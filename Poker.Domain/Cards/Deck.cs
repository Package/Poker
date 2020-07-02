using Poker.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker.Domain
{
    public class Deck
    {
        public ICollection<Card> Cards { get; private set; }

        public Deck()
        {
            Create();
        }

        /// <summary>
        /// Creates a new deck. Adds in all the 52 standard playing cards.
        /// </summary>
        public void Create()
        {
            InitialiseOrClear();

            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Value value in Enum.GetValues(typeof(Value)))
                {
                    if (value == Value.None)
                        continue;

                    Cards.Add(new Card { Value = value, Suit = suit });
                }
            }

            Shuffle();
        }

        /// <summary>
        /// Shuffles the deck of cards.
        /// </summary>
        public void Shuffle()
        {
            Cards = Cards.OrderBy(c => Guid.NewGuid()).ToList();
        }

        /// <summary>
        /// Removes the provided hole cards from the deck.
        /// </summary>
        /// <param name="players"></param>
        public void RemovePlayerHoleCards(ICollection<Player> players)
        {
            foreach (var player in players)
            {
                foreach (var card in player.Hand.Cards)
                {
                    Cards.Remove(card);
                }
            }
        }

        /// <summary>
        /// Takes the specified number of cards from the deck. The cards are subsequently removed.
        /// This assumes that the you have been previously called Shuffle() to randomise the card order in the collection.
        /// </summary>
        /// <param name="numberOfCards"></param>
        /// <returns></returns>
        public List<Card> Take(int numberOfCards)
        {
            if (numberOfCards < 1 || numberOfCards >= Cards.Count)
            {
                throw new IndexOutOfRangeException($"Cannot take {numberOfCards} cards.");
            }

            var cardsToTake = Cards.Take(numberOfCards);
            foreach (var card in cardsToTake)
            {
                Cards.Remove(card);
            }

            return cardsToTake.ToList();
        }

        /// <summary>
        /// Initialises the deck if the cards are currently null.
        /// Otherwise, clears the existing cards ready to build a new deck.
        /// </summary>
        private void InitialiseOrClear()
        {
            if (Cards == null)
            {
                Cards = new List<Card>();
            }
            else
            {
                Cards.Clear();
            }
        }
    }
}
