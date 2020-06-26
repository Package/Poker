using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Poker.Tests
{
    [TestClass]
    public class Test_Hand
    {
        [TestMethod]
        public void CanAddCardToHand()
        {
            var h = new Hand();
            var c1 = Card.FromString("5D");
            h.AddCard(c1);

            Assert.AreEqual(h.Cards.Count, 1);
        }

        [TestMethod]
        public void CanResetHand()
        {
            var h = new Hand();

            var c1 = Card.FromString("5D");
            h.AddCard(c1);
            Assert.AreEqual(h.Cards.Count, 1);

            h.Reset();
            Assert.AreEqual(h.Cards.Count, 0);
        }

        [TestMethod]
        public void CanBuildHandFromString()
        {
            var h1 = Hand.FromString("AD 5H 3C 4C TS");
            Assert.IsNotNull(h1.Cards);
            Assert.AreEqual(h1.Cards.Count, 5);

            var c1 = h1.Cards.FirstOrDefault();
            Assert.AreEqual(c1.Suit, Suit.Diamonds);
            Assert.AreEqual(c1.Value, Value.AceHigh);

            var h2 = Hand.FromString("AD 5H");
            Assert.IsNotNull(h2.Cards);
            Assert.AreEqual(h2.Cards.Count, 2);

            var h3 = Hand.FromString("AD");
            Assert.IsNotNull(h3.Cards);
            Assert.AreEqual(h3.Cards.Count, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsErrorWithInvalidString()
        {
            var h1 = Hand.FromString("AD5H");
        }
    }
}
