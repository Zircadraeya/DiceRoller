using DiceRoller.Shared;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiceRoller.Server.Hubs
{
    public class DiceHub : Hub
    {
        public async Task RollDice(PlayerModel player, string request)
        {
            var regex = new System.Text.RegularExpressions.Regex(@"(?<diceCount>[0-9]+)d(?<diceValue>[0-9]+)(?<diceMod>([\+\-][0-9]+))?");
            var match = regex.Match(request);
            var number = int.Parse(match.Groups["diceCount"].Value);
            var sides = int.Parse(match.Groups["diceValue"].Value);
            var mod = match.Groups["diceMod"].Success ? int.Parse(match.Groups["diceMod"].Value) : 0;
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
