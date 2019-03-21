namespace Webhook
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Events;

    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                         .MinimumLevel.Debug()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .CreateLogger();

            Log.Information("Starting the API...");

            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                        .AddJsonFile("appsettings.json", false)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", true)
                        .AddEnvironmentVariables()
                        .AddCommandLine(args);
                })
                .UseStartup<Startup>()
                .UseSerilog()
                .Build()
                .Run();
        }

    }
}