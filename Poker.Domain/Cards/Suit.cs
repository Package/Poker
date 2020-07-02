using System;

namespace Poker.Domain
{
    public enum Suit
    {
        Spades, Clubs, Hearts, Diamonds
    }

    public static class SuitMethods
    {
        /// <summary>
        /// Returns a one character string representation of this suit.
        /// E.g. AsString(Suit.Diamonds) => "D"
        /// </summary>
        /// <param name="suit"></param>
        /// <returns></returns>
        public static string AsString(this Suit suit) =>
            suit switch
            {
                Suit.Diamonds => "D",
                Suit.Hearts => "H",
                Suit.Clubs => "C",
                Suit.Spades => "S",
                _ => throw new ArgumentException($"Unknown suit {suit}")
            };
    }
}
