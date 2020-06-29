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
        public void CanOrderListOfHandsCorrectly()
        {
            var hands = new List<Hand>()
            {
                Hand.FromString("3S 3C 3D 6H 4C AD AH"), // Full House 3s, Aces
                Hand.FromString("3S 6C 6D 6H 4C AD AH"), // Full House 6s, Aces
                Hand.FromString("3S AC AD 6H 4C AD AH"), // Four of kind
                Hand.FromString("3S 4C 5D 6H 7C 8D AH"), // Straight
                Hand.FromString("3S 3C 3D 6H 4C AD AH"), // Two Pair
                Hand.FromString("3S 5C AD 6H 4C KD 9H"), // High Card
            };

            var handResults = new List<HandResult>();
            foreach (var h in hands)
            {
                handResults.Add(HandEvaluation.Evaluate(h));
            }
            handResults.Sort((h1, h2) => h2.CompareTo(h1));
            Assert.AreEqual(hands.Count, handResults.Count);
        }

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
            var handOne = Hand.FromString("2D 3C 5S 4H TC 6D 3C");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Straight, resultOne.Strength);

            var handTwo = Hand.FromString("5S 4H TC 6D 3C 7D AH");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Straight, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);

            handOne = Hand.FromString("TD 6C JS QS TH KC AH");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Straight, resultOne.Strength);

            handTwo = Hand.FromString("7H 4H 8C 2S 9S TC JD");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Straight, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerOne, compareResult);

            // Edge case with low ace straight
            handOne = Hand.FromString("AH 2D 3C 4H 5S TS KD");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Straight, resultOne.Strength);

            handTwo = Hand.FromString("AH 2D 3C 4H 5S TS 6D");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Straight, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);
        }

        [TestMethod]
        public void CanCompareFlush()
        {
            var handOne = Hand.FromString("2C 3C 5S 4H TC 6C 3C");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Flush, resultOne.Strength);

            var handTwo = Hand.FromString("5S 4H TH 6H 3C 7H AH");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Flush, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);

            handOne = Hand.FromString("TD 6D JS QD TD KD AH");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.Flush, resultOne.Strength);

            handTwo = Hand.FromString("7S 4S 8S 2S 9S TC JS");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.Flush, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerOne, compareResult);
        }

        [TestMethod]
        public void CanCompareFullHouse()
        {
            var handOne = Hand.FromString("2D 3C 3S 3H TC TH AC");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.FullHouse, resultOne.Strength);

            var handTwo = Hand.FromString("3S 3C 3D 6H 4C AD AH");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.FullHouse, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);

            handOne = Hand.FromString("TD TH JS JD JH KC AH");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.FullHouse, resultOne.Strength);

            handTwo = Hand.FromString("7S 7D 7H 9H 9S TC JS");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.FullHouse, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerOne, compareResult);
        }

        [TestMethod]
        public void CanCompareStraightFlush()
        {
            var handOne = Hand.FromString("2C 3C 4C 5C TC TH AC");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.StraightFlush, resultOne.Strength);

            var handTwo = Hand.FromString("2C 3C 4C 5C 6C TH AC");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.StraightFlush, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);

            handOne = Hand.FromString("TD 9D 8D 7D 7H 6D AH");
            resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.StraightFlush, resultOne.Strength);

            handTwo = Hand.FromString("QD JD TD 9D 8D 6H 7D");
            resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.StraightFlush, resultTwo.Strength);

            compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(PlayerTwo, compareResult);
        }

        [TestMethod]
        public void CanCompareRoyalFlush()
        {
            var handOne = Hand.FromString("TC JC QC AC KC 5H 5C");
            var resultOne = HandEvaluation.Evaluate(handOne);
            Assert.AreEqual(HandStrength.RoyalFlush, resultOne.Strength);

            var handTwo = Hand.FromString("AH KH QH JH TH 5H 6C");
            var resultTwo = HandEvaluation.Evaluate(handTwo);
            Assert.AreEqual(HandStrength.RoyalFlush, resultTwo.Strength);

            var compareResult = resultOne.CompareTo(resultTwo);
            Assert.AreEqual(Draw, compareResult);
        }
    }
}
