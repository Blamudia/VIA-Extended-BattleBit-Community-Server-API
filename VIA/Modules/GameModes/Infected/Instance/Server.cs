using BattleBitAPI.Server;
using BBR.Community.API;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;

namespace CommunityServerAPI.VIA.Modules.GameModes.Infected.Instance
{
    //TODO: resolve inside a service layer which is callable via either itself eg; chat or endpoint in order to switch gamemode/server for example
    public class Server : IHostedService, IDisposable
    {
        private readonly ServerListener<MyPlayer, MyGameServer> _listener;
        private readonly ILogger<Server> _logger;
        private readonly Configuration _configuration;

        private bool _disposed;

        public Server(ILogger<Server> logger, IConfiguration configuration)
        {
            _listener = new ServerListener<MyPlayer, MyGameServer>();
            _logger = logger;
            _configuration = configuration.Get<Configuration>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (!_listener.IsListening)
                _listener.Start(
                    IPAddress.Parse(_configuration.BattleBit.IP),
                    _configuration.BattleBit.Port
                );

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping gamemode");
            _listener.Stop();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;
        }
    }
}