using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Domain
{
    public class Hand
    {
        public ICollection<Card> Cards { get; private set; }

        /// <summary>
        /// Creates a Hand from the provided string.
        /// E.g. "AD 5H 3C 4C TS" would return Hand which contains 5 Cards.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Hand FromString(string str)
        {
            var cards = str.Split(" ").Select(s => Card.FromString(s)).ToList();
            if (cards == null || !cards.Any())
            {
                throw new ArgumentException($"Unable to create hand from input: {str}");
            }

            return new Hand { 
                Cards = cards
            };
        }

        /// <summary>
        /// Adds a Card into this Hand.
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(Card card)
        {
            if (Cards == null)
            {
                Cards = new List<Card>();
            }

            Cards.Add(card);
        }

        /// <summary>
        /// Resets the cards in this hand.
        /// </summary>
        public void Reset()
        {
            Cards?.Clear();
        }
    }
}
