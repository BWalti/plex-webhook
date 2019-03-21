namespace Webhook.Eventing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    public class EventAggregator : IEventAggregator
    {
        private readonly List<Handler> handlers = new List<Handler>();

        /// <inheritdoc />
        public virtual void Subscribe(object subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            lock (this.handlers)
            {
                if (this.handlers.Any(x => x.Matches(subscriber)))
                {
                    return;
                }

                this.handlers.Add(new Handler(subscriber));
            }
        }

        /// <inheritdoc />
        public virtual void Unsubscribe(object subscriber)
        {
            if (subscriber == null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            lock (this.handlers)
            {
                var handlersFound = this.handlers.FirstOrDefault(x => x.Matches(subscriber));

                if (handlersFound != null)
                {
                    this.handlers.Remove(handlersFound);
                }
            }
        }

        public virtual async Task PublishAsync(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Handler[] handlersToNotify;

            lock (this.handlers)
            {
                handlersToNotify = this.handlers.ToArray();
            }

            var messageType = message.GetType();

            var tasks = handlersToNotify.Select(h => h.Handle(messageType, message));

            await Task.WhenAll(tasks);

            foreach (var handler in handlersToNotify.Where(x => !x.IsDead))
            {
                if (handler.Reference.Target is ComponentBase component)
                {
                    var myMethod = component.GetType().GetMethod("StateHasChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                    myMethod.Invoke(component, null);
                }
            }

            var deadHandlers = handlersToNotify.Where(h => h.IsDead).ToList();

            if (deadHandlers.Any())
            {
                lock (this.handlers)
                {
                    foreach (var item in deadHandlers)
                    {
                        this.handlers.Remove(item);
                    }
                }
            }
        }

        private class Handler
        {
            private readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

            public Handler(object handler)
            {
                this.Reference = new WeakReference(handler);

                var interfaces = handler.GetType().GetTypeInfo().ImplementedInterfaces
                    .Where(x => x.GetTypeInfo().IsGenericType && x.GetGenericTypeDefinition() == typeof(IHandle<>));

                foreach (var handleInterface in interfaces)
                {
                    var type = handleInterface.GetTypeInfo().GenericTypeArguments[0];
                    var method = handleInterface.GetRuntimeMethod("HandleAsync", new[] { type });

                    if (method != null)
                    {
                        this.supportedHandlers[type] = method;
                    }
                }
            }

            public bool IsDead => this.Reference.Target == null;

            public WeakReference Reference { get; }

            public bool Matches(object instance)
            {
                return this.Reference.Target == instance;
            }

            public Task Handle(Type messageType, object message)
            {
                var target = this.Reference.Target;

                if (target == null)
                {
                    return Task.FromResult(false);
                }

                var tasks = this.supportedHandlers
                    .Where(handler => handler.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()))
                    .Select(pair => pair.Value.Invoke(target, new[] { message }))
                    .Select(result => (Task)result)
                    .ToList();

                return Task.WhenAll(tasks);
            }

            //public bool Handles(Type messageType)
            //{
            //    return this.supportedHandlers.Any(pair => pair.Key.GetTypeInfo().IsAssignableFrom(messageType.GetTypeInfo()));
            //}
        }
    }
}