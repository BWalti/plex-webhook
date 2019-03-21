namespace Webhook.Modules
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Webhook.Eventing;
    using Webhook.Models;

    public static class PlexApplicationBuilderExtensions
    {
        public static void UseSavePlexHooksInElasticSearch(this IApplicationBuilder app)
        {
            var eventAggregator = app.ApplicationServices.GetService<IEventAggregator>();
            var esIntegration = app.ApplicationServices.GetService<SavePlexHookInElasticSearchModule>();

            eventAggregator.Subscribe(esIntegration);
        }

        public static void UsePlexApiKey(this IApplicationBuilder app)
        {
            var plexConfig = app.ApplicationServices.GetService<PlexConfig>();
            if (string.IsNullOrEmpty(plexConfig.AuthToken))
            {
                // no auth key defined, generate one!
                Log.Warning("Generating Auth key as none is defined...");

                var partA = Guid.NewGuid().ToString("N");
                var partB = Guid.NewGuid().ToString("N");

                var key = $"{partA}{partB}";

                Log.Warning($"Generated Key: {key}");
                plexConfig.AuthToken = key;
            }
        }
    }
}