using BBR.Community.API;
using CommunityServerAPI.VIA.Modules.GameModes.Infected.Instance;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CommunityServerAPI.VIA.Other.DI
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
                    Configuration = configBuilder.Build().Get<Configuration>() ?? new Configuration();


                })
                .ConfigureServices(services =>
                {
                    services.AddLogging();
                    services.AddHostedService<Server>();
                });
    }
}
