using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace CommunityServerAPI.VIA.Modules.GameModes.Infected.Instance
{
    //TODO, cleanup contents. actually integrate in api
    class MyGameServer : GameServer<MyPlayer>
    {
        public static new TGameServer CreateInstance<TGameServer>(Internal @internal) where TGameServer : MyGameServer
        {
            TGameServer gameServer = (TGameServer)Activator.CreateInstance(typeof(TGameServer));
            gameServer.mInternal = @internal;
            return gameServer;
        }

        public override async Task OnPlayerJoiningToServer(ulong steamId, PlayerJoiningArguments args)
        {
            await Console.Out.WriteLineAsync($"Player joining");
        }

        public override async Task OnPlayerConnected(MyPlayer player)
        {
            await Console.Out.WriteLineAsync("Connected: " + player);
        }

        public override async Task OnPlayerSpawned(MyPlayer player)
        {
            player.SetRunningSpeedMultiplier(2f);
            player.SetJumpMultiplier(2f);
            player.SetFallDamageMultiplier(0f);
            player.SetReceiveDamageMultiplier(0.1f);
            player.SetGiveDamageMultiplier(4f);

            await Task.CompletedTask;
        }

        public override async Task OnConnected()
        {
            await Console.Out.WriteLineAsync("Current state: " + RoundSettings.State);

        }
        public override async Task OnGameStateChanged(GameState oldState, GameState newState)
        {
            await Console.Out.WriteLineAsync("State changed to -> " + newState);
        }
    }

}
