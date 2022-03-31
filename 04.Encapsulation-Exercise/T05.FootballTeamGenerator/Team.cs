using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T05.FootballTeamGenerator
{
    public class Team
    {
        private List<Player> players;
        private string name;

        public Team(string name)
        {
            Name = name;
            players = new List<Player>();
        }

        public string Name
        {
            get => name;

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("A name should not be empty.");
                }

                name = value;
            }
        }

        public int Rating => players.Any() ? (int)Math.Round(players.Average(x => x.Stats)) : 0;

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }

        public bool RemovePlayer(string playerName)
        {
            return players.Remove(players.FirstOrDefault(x => x.Name == playerName));
        }
    }
}
