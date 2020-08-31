using DiceRoller.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiceRoller.Server.Hubs
{
    public class DiceHub : Hub
    {
        public async Task RollDice(PlayerModel player, string request, int mod)
        {
            var number = int.Parse(request.Split('d')[0]);
            var sides = int.Parse(request.Split('d')[1]);
            var results = new List<int>();
            for (int i = 0; i < number; i++)
            {
                results.Add(ThreadLocalRandom.Instance.Next(1, sides + 1));
            }
            var roll = new Roll
            {
                Request = request,
                Result = results,
                Modifier = mod
            };
            await Clients.All.SendAsync("DiceRolled", player, roll);
        }
    }
}
