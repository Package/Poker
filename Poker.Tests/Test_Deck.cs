using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Tests
{
    [TestClass]
    public class Test_Deck
    {
        [TestMethod]
        public void CanCreateDeck()
        {
            var deck = new Deck();
            Assert.AreEqual(deck.Cards.Count, 52);

            var jackOfDiamonds = deck.Cards.FirstOrDefault(c => c.Suit == Suit.Diamonds && c.Value == Value.Jack);
            Assert.IsNotNull(jackOfDiamonds);

            var threeOfClubs = deck.Cards.FirstOrDefault(c => c.Suit == Suit.Clubs && c.Value == Value.Three);
            Assert.IsNotNull(threeOfClubs);

            var allSpades = deck.Cards.Where(c => c.Suit == Suit.Spades);
            Assert.AreEqual(allSpades.Count(), 13);
        }

        [TestMethod]
        public void CanTakeCards()
        {
            var deck = new Deck();
            var twoCards = deck.Take(2);
            Assert.AreEqual(twoCards.Count, 2);
            Assert.AreEqual(deck.Cards.Count, 50); // 50 left after taking 2
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ThrowsErrorIfTakingTooManyCards()
        {
            var deck = new Deck();
            var cards = deck.Take(100); // More than exist in deck
        }

        [TestMethod]
        public void CanResetDeck()
        {
            var deck = new Deck();
            var cards = deck.Take(10);
            Assert.AreEqual(cards.Count, 10);
            Assert.AreEqual(deck.Cards.Count, 42); // 42 left after taking 10

            deck.Create(); // Resets deck
            Assert.AreEqual(deck.Cards.Count, 52); // back to a new deck
        }
    }
}
