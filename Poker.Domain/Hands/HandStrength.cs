using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Domain.Hands
{
    public enum HandStrength
    {
        None, HighCard, Pair, TwoPair, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush, RoyalFlush
    }
}
