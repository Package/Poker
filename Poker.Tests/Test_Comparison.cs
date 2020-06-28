using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Domain;
using Poker.Domain.Hands;
using Poker.Evaluator.Evaluation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Tests
{
    [TestClass]
    public class Test_Comparison
    {
        const int PlayerOne = 1;
        const int PlayerTwo = -1;
        const int Draw = 0;

        [TestMethod]
        public void CanCompareTiedHands()
        {
            // Same hand, players draw with Ace High Card
            var handOne = Hand.FromString("5D 8C 9S JS AC");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.HighCard, resultOne.Strength);

            var handTwo = Hand.FromString("5D 8C 9S JS AC");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.HighCard, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(Draw, compareResult);

            // Same hand, players draw with Two Pair
            handOne = Hand.FromString("5D 8C 9S 9S 8C");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.TwoPair, resultOne.Strength);

            handTwo = Hand.FromString("5D 8C 9S 9S 8C");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.TwoPair, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(Draw, compareResult);

            // Same hand, players draw with Flush
            handOne = Hand.FromString("5D 8D 9D 9D 8D");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Flush, resultOne.Strength);

            handTwo = Hand.FromString("5D 8D 9D 9D 8D");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Flush, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(Draw, compareResult);
        }

        [TestMethod]
        public void CanCompareHighCard()
        {
            // Player one wins with: High Card Ace -> High Card Queen
            var handOne = Hand.FromString("5D 8C 9S JS AC");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.HighCard, resultOne.Strength);

            var handTwo = Hand.FromString("2C 5C 7D 8S QH");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.HighCard, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerOne, compareResult);
        }

        [TestMethod]
        public void CanComparePair()
        {
            // Player two wins with: Pair Eights -> Pair Fives
            var handOne = Hand.FromString("5H 5C 6S 7S KD");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Pair, resultOne.Strength);

            var handTwo = Hand.FromString("2C 3S 8S 8D TD");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Pair, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);

            // Player one wins with: Pair Queen (9 kicker) -> Pair Queen (7 kicker)
            handOne = Hand.FromString("4D 6S 9H QH QC");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Pair, resultOne.Strength);

            handTwo = Hand.FromString("3D 6D 7H QD QS");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Pair, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerOne, compareResult);
        }

        [TestMethod]
        public void CanCompareTwoPairs()
        {
            // Player One win with Two Pair: 9s + 8s
            var handOne = Hand.FromString("5D 8C 9S 9S 8C AS 3H");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.TwoPair, resultOne.Strength);

            var handTwo = Hand.FromString("5D 8C 9S 4S 8C 2D 4D");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.TwoPair, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerOne, compareResult);

            // Player Two win with Two Pair: As + 4s
            handOne = Hand.FromString("5D 8C 9S 9S 8C AS 3H");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.TwoPair, resultOne.Strength);

            handTwo = Hand.FromString("AD AC 9S 4S 8C 2D 4D");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.TwoPair, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);
        }

        [TestMethod]
        public void CanCompareThreeOfAKind()
        {
            // Player two wins with: Three of Kind Ace (Ten Kicker)
            var handOne = Hand.FromString("2D 9C AS AH AC");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.ThreeOfAKind, resultOne.Strength);

            var handTwo = Hand.FromString("2D TC AS AH AC");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.ThreeOfAKind, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);

            // Player two wins with: Three of Kind Queen
            handOne = Hand.FromString("4D 4S 9H QH 4C");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.ThreeOfAKind, resultOne.Strength);

            handTwo = Hand.FromString("3D QS QH QD 4S");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.ThreeOfAKind, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);
        }

        [TestMethod]
        public void CanCompareStraight()
        {
            // Write tests...
        }

        [TestMethod]
        public void CanCompareFlush()
        {
            // Write tests...
        }

        [TestMethod]
        public void CanCompareFullHouse()
        {
            // Write tests...
        }

        [TestMethod]
        public void CanCompareStraightFlush()
        {
            // Write tests...
        }

        [TestMethod]
        public void CanCompareRoyalFlush()
        {
            // Write tests...
        }
    }
}
