using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Domain.Pot
{
    public enum Street
    {
        Flop, Turn, River
    }

    public static class StreetExtensions
    {
        /// <summary>
        /// Returns the number of cards that should be dealt for a given street.
        /// </summary>
        /// <param name="street"></param>
        /// <returns></returns>
        public static int NumberOfCards(this Street street) => street switch
        {
            Street.Flop => 3,
            Street.Turn => 1,
            Street.River => 1,
            _ => 1
        };
    }
}
