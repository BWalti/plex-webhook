namespace Webhook.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Webhook.Eventing;
    using Webhook.Messages;
    using Webhook.Models;
    using Webhook.Models.PlexWebhook;

    [Route("api/[controller]")]
    [ApiController]
    public class PlexController : ControllerBase
    {
        private readonly IEventAggregator eventAggregator;
        private readonly PlexConfig config;

        private static readonly List<PlexWebHook> Hooks;

        static PlexController()
        {
            Hooks = new List<PlexWebHook>();
        }

        public PlexController(IEventAggregator eventAggregator, PlexConfig config)
        {
            this.eventAggregator = eventAggregator;
            this.config = config;
        }

        [HttpPost]
        public async Task<ActionResult<PlexWebHook>> Post(PlexWebHook hook, [FromQuery]string authKey)
        {
            if (authKey != this.config.AuthToken)
            {
                return this.BadRequest();
            }

            Hooks.Add(hook);

            await this.eventAggregator.PublishAsync(new PlexWebHookReceived(hook));

            return this.CreatedAtAction(nameof(this.Get), new { id = Hooks.Count - 1 }, hook);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlexWebHook>>> Get()
        {
            await Task.Delay(0);
            return this.Ok(Hooks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PlexWebHook>> Get(int id)
        {
            await Task.Delay(0);
            return this.Ok(Hooks[id]);
        }
    }
}