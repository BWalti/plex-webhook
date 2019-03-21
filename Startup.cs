namespace Webhook
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Nest;

    public class Startup
    {
        private readonly ElasticClient client;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

            var node = new Uri("http://192.168.0.20:32423/");
            var settings = new ConnectionSettings(node);
            this.client = new ElasticClient(settings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this.client);

            services.AddLogging(builder => builder.AddConsole().AddDebug());

            services.AddMvc()
                    .AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting(routes =>
            {
                routes.MapDefaultControllerRoute();
            });
        }
    }
}