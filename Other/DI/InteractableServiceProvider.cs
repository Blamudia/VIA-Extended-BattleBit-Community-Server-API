using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BBR.Community.API.Other.DI
{
    public sealed class InteractableServiceProvider
    {
        public static readonly IHostBuilder Builder = createDefaultBuilder();

        public static Configuration Configuration { get; private set; } = new();

        private static IHostBuilder createDefaultBuilder()
            => Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(app =>
                {
                    var configBuilder = app.AddJsonFile("appsettings.json");
                    Configuration = ConfigurationBinder.Get<Configuration>(configBuilder.Build()) ?? new Configuration();
                    var x = 5;
                })
                .ConfigureServices(services =>
                {
                    services.AddLogging();
                });
    }
}
