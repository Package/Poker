using Poker.Domain.Hands;
using Poker.Domain.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Domain.Pot
{
    public class PotResult
    {
        public Pot Pot { get; set; }
        public ICollection<Player> Winners { get; set; }
        public ICollection<HandResult> HandResults { get; set; }
    }
}
