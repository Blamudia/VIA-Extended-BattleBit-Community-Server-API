using BattleBitAPI.Server;
using Microsoft.Extensions.Hosting;

namespace BBR.Community.API.Modules.GameModes.Infected.Instance
{
    public class Server : IHostedService
    {
        private static ServerListener<MyPlayer, MyGameServer> _listener = new ServerListener<MyPlayer, MyGameServer>();

        //todo, don't do it this way lmao.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_listener.Start(29294);

            //lets break shit. not in the mood to safely shut it down yet.
            Thread.Sleep(-1);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_listener.Stop();
            return Task.CompletedTask;
        }
    }
}
