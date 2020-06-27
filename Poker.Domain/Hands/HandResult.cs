using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Domain.Hands
{
    public class HandResult
    {
        public Hand Hand { get; set; }
        public HandStrength Strength { get; set; }
        public ICollection<Card> Core { get; set; }
        public ICollection<Card> Kickers { get; set; }
    }
}
