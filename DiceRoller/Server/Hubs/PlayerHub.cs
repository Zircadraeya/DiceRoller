using DiceRoller.Shared;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace DiceRoller.Server.Hubs
{
    public class PlayerHub : Hub
    {
        private static IPlayerStore PlayerStore;

        public PlayerHub(IPlayerStore playerStore)
        {
            PlayerStore = playerStore;
        }

        public async Task Join(string playerName, string characterName)
        {
            var player = new PlayerModel
            {
                Id = Context.ConnectionId,
                PlayerName = playerName,
                CharacterName = characterName
            };
            PlayerStore.Add(player);
            await Clients.All.SendAsync("PlayersChanged", PlayerStore.GetPlayers());
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = Context.ConnectionId;
            PlayerStore.Remove(id);
            Clients.All.SendAsync("PlayersChanged", PlayerStore.GetPlayers());
            return base.OnDisconnectedAsync(exception);
        }
    }
}
