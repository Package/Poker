using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Domain.Hands
{
    public static class HandExtensions
    {
        /// <summary>
        /// Returns the X number of cards from this hand which are considered the "best".
        /// Best is simply determined by the card's value.
        /// </summary>
        /// <param name="cardsInHand"></param>
        /// <param name="amountToTake"></param>
        /// <returns></returns>
        public static IList<Card> TakeBest(this IEnumerable<Card> cardsInHand, int amountToTake = 5)
        {
            return cardsInHand.OrderByDescending(card => card.Value).Take(amountToTake).ToList();
        }
    }
}
