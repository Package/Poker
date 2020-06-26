using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Domain.Hands
{
    public enum HandStrength
    {
        HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush
    }
}
