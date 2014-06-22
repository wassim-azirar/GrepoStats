using System.Collections.Generic;
using GrepoStats.Model;

namespace GrepoStats.Utils
{
    public static class DefaultDataLoader
    {
        public static IList<Alliance> GetAlliancesDefaultData()
        {
            return new List<Alliance>();
        }

        public static IList<Island> GetIslandsDefaultData()
        {
            return new List<Island>();
        }

        public static IList<Player> GetPlayersDefaultData()
        {
            return new List<Player>
            {
                new Player
                {
                    Id = 1,
                    AllianceId = 25,
                    Name = "Stéphane",
                    Points = 3900,
                    Rank = 34,
                    Towns = 3
                },

                new Player
                {
                    Id = 2,
                    AllianceId = 31,
                    Name = "Jean Jack",
                    Points = 1890,
                    Rank = 47,
                    Towns = 1
                },

                new Player
                {
                    Id = 3,
                    AllianceId = 10,
                    Name = "Pierre-Marie",
                    Points = 12001,
                    Rank = 2,
                    Towns = 10
                }
            };
        }

        public static IList<Town> GetTownsDefaultData()
        {
            return new List<Town>();
        }
    }
}
