using Poker.Domain.Hands;
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
        /// Creates a new Hand from a given collection of hole cards.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns></returns>
        public static Hand FromCards(ICollection<Card> cards)
        {
            return new Hand { Cards = cards };
        }

        /// <summary>
        /// Adds a Card into this Hand.
        /// </summary>
        /// <param name="card"></param>
        public void AddCards(params Card[] cards)
        {
            if (Cards == null)
            {
                Cards = new List<Card>();
            }

            foreach (var card in cards)
            {
                Cards.Add(card);
            }
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
