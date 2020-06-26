using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Domain;
using System;

namespace Poker.Tests
{
    [TestClass]
    public class Test_Card
    {
        [TestMethod]
        public void CanBuildCardFromString()
        {
            var c1 = Card.FromString("AD");
            Assert.AreEqual(c1.Suit, Suit.Diamonds);
            Assert.AreEqual(c1.Value, Value.AceHigh);

            var c2 = Card.FromString("TC");
            Assert.AreEqual(c2.Suit, Suit.Clubs);
            Assert.AreEqual(c2.Value, Value.Ten);

            var c3 = Card.FromString("4H");
            Assert.AreEqual(c3.Suit, Suit.Hearts);
            Assert.AreEqual(c3.Value, Value.Four);

            var c4 = Card.FromString("KS");
            Assert.AreEqual(c4.Suit, Suit.Spades);
            Assert.AreEqual(c4.Value, Value.King);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsErrorWithInvalidCard()
        {
            // String length too long to be a valid card - this should trigger an exception
            var c = Card.FromString("10D");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsErrorWithInvalidSuit()
        {
            // No such suit with "V" - this should trigger an exception
            var c = Card.FromString("KV");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowsErrorWithInvalidValue()
        {
            // No such value with "G" - this should trigger an exception
            var c = Card.FromString("GD");
        }
    }
}
