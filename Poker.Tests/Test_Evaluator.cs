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
    public class Test_Evaluator
    {
        [TestMethod]
        public void CanEvaluateHighCard()
        {
            // High Card
            var hand = Hand.FromString("2S 5H 8C 9C AS KD");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.HighCard);
        }

        [TestMethod]
        public void CanEvaluatePairs()
        {
            // Two Pair
            var hand = Hand.FromString("5H 5S TC TD KH 3D 4D");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.TwoPair);

            // One Pair
            hand = Hand.FromString("QH TD 3S QC AH 2D 6S");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.Pair);

            // Three Pair (should still return two pairs tho)
            hand = Hand.FromString("5H 5S TC TD KH 6D KS");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.TwoPair);
        }

        [TestMethod]
        public void CanEvaluateStraight()
        {
            // Standard straight
            var hand = Hand.FromString("5H 6D 7D QS 8C 9S AS");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Straight, strength);

            // 7 card straight
            hand = Hand.FromString("5H 6D 7D TC 8C 9S 4D");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Straight, strength);

            // Low straight
            hand = Hand.FromString("AH 2D 4D 5C TC 9S 3D");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Straight, strength);

            // Low 6 card straight
            hand = Hand.FromString("AH 2D 4D 5C 6C 9S 3D");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Straight, strength);

            // 7 card high straight
            hand = Hand.FromString("9H TD QD KC JC AS 8D");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Straight, strength);

            // Nut straight
            hand = Hand.FromString("JH QD TS AS 6C KS 3D");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Straight, strength);
        }

        [TestMethod]
        public void CanEvaluateStraightFlush()
        {
            // High straight flush
            var hand = Hand.FromString("9D TD JD QD 6S KD 4C");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.StraightFlush, strength);

            // Low straight flush
            hand = Hand.FromString("AS 2S 4S 3S 8S 5S");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.StraightFlush, strength);

            // Mid straight flush
            hand = Hand.FromString("6H 8H 7H 9H 5H 3S AS");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.StraightFlush, strength);

            // Straight & Flush but not a straight flush - should return just flush
            hand = Hand.FromString("JH 8H 7H 9H 5H 6S AS");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Flush, strength);
        }

        [TestMethod]
        public void CanEvaluateRoyalFlush()
        {
            // Royal flush
            var hand = Hand.FromString("TC JC QC KC AC 6S 3D");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.RoyalFlush, strength);

            // 7 card but still royal flush
            hand = Hand.FromString("TC JC QC KC AC 9C 8C");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.RoyalFlush, strength);

            // Not quite a royal flush - this should just return flush
            hand = Hand.FromString("TC JC QC KC AS 6S 3C");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(HandStrength.Flush, strength);
        }

        [TestMethod]
        public void CanEvaluateFlush()
        {
            // A flush
            var hand = Hand.FromString("6S 7S KS 2S JS AD");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.Flush);

            // Not a flush
            hand = Hand.FromString("6C 7S KS 2S JS AD");
            strength = HandEvaluation.Strength(hand);
            Assert.AreNotEqual(strength, HandStrength.Flush);
        }

        [TestMethod]
        public void CanEvaluateFullHouse()
        {
            // Full House
            var hand = Hand.FromString("4H 4S TH 4D KH KD 7H");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.FullHouse);
        }

        [TestMethod]
        public void CanEvaluateOfAKind()
        {
            // Three of a Kind
            var hand = Hand.FromString("AD 2S TH 4D AS KD AH");
            var strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.ThreeOfAKind);

            // Four of a Kind
            hand = Hand.FromString("JD JS JH 4D AS JD AH");
            strength = HandEvaluation.Strength(hand);
            Assert.AreEqual(strength, HandStrength.FourOfAKind);
        }
    }
}
