namespace Webhook
{
    using System;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Nest;
    using Webhook.Eventing;
    using Webhook.Models;
    using Webhook.Modules;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddLogging(builder => builder.AddConsole().AddDebug());

            //services.AddSingleton<SavePlexHookInElasticSearchModule>();

            services.AddSingleton(this.Configuration.GetSection("Plex").Get<PlexConfig>());
            services.AddSingleton(this.Configuration.GetSection("ElasticSearch").Get<ElasticSearchConfig>());
            services.AddSingleton(provider =>
            {
                var esConfig = provider.GetRequiredService<ElasticSearchConfig>();

                var node = new Uri(esConfig.Uri);
                var settings = new ConnectionSettings(node);
                return new ElasticClient(settings);
            });

            services.AddMvc()
                    .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePlexApiKey();
            //app.UseSavePlexHooksInElasticSearch();

            app.UseRouting();
            app.UseEndpoints(routes =>
            {
                routes.MapControllers();
            });
        }
    }
}