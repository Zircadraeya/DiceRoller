using DiceRoller.Shared;
using System.Collections.Generic;
using System.Linq;

namespace DiceRoller.Server
{
    public interface IPlayerStore
    {
        void Add(PlayerModel player);
        List<PlayerModel> GetPlayers();
        void Remove(string connectionId);
    }
    public class PlayerStore : IPlayerStore
    {
        private readonly List<PlayerModel> _players = new List<PlayerModel>();

        public void Add(PlayerModel player)
        {
            if (player != null)
            {
                lock (_players)
                {
                    _players.Add(player);
                }
            }
        }

        public List<PlayerModel> GetPlayers()
        {
            return _players;
        }

        public void Remove(string connectionId)
        {
            if (!string.IsNullOrEmpty(connectionId))
            {
                lock (_players)
                {
                    var player = _players.FirstOrDefault(p => p.Id == connectionId);
                    if (player != null)
                    {
                        _players.Remove(player);
                    }
                }
            }
        }
    }
}
