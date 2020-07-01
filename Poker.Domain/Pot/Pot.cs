using Poker.Domain.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Domain.Pot
{
    public class Pot
    {
        public Guid Id { get; set; }

        public IList<Player> Players { get; set; }

        public decimal Total { get; set; }

        public Pot()
        {
            Create();
        }

        /// <summary>
        /// Creates a new Pot, setting and initialising all default values.
        /// </summary>
        public void Create()
        {
            Id = Guid.NewGuid();

            if (Players == null)
            {
                Players = new List<Player>();

                for (var playerCount = 1; playerCount <= 6; playerCount++)
                {
                    Players.Add(new HumanPlayer
                    {
                        Balance = 1_000m,
                        Name = $"Player #{playerCount}"
                    });
                }
            }
        }
    }
}
