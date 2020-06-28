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
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.HighCard, result.Strength);
            Assert.AreEqual(1, result.Core.Count);
            Assert.AreEqual(4, result.Kickers.Count);
        }

        [TestMethod]
        public void CanEvaluatePairs()
        {
            // Two Pair
            var hand = Hand.FromString("5H 5S TC TD KH 3D 4D");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.TwoPair, result.Strength);
            Assert.AreEqual(4, result.Core.Count);
            Assert.AreEqual(1, result.Kickers.Count);

            // One Pair
            hand = Hand.FromString("QH TD 3S QC AH 2D 6S");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Pair, result.Strength);
            Assert.AreEqual(2, result.Core.Count);
            Assert.AreEqual(3, result.Kickers.Count);

            // Three Pair (should still return two pairs tho)
            hand = Hand.FromString("5H 5S TC TD KH 6D KS");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.TwoPair, result.Strength);
            Assert.AreEqual(4, result.Core.Count);
            Assert.AreEqual(1, result.Kickers.Count);
        }

        [TestMethod]
        public void CanEvaluateStraight()
        {
            // Standard straight
            var hand = Hand.FromString("5H 6D 7D QS 8C 9S AS");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Straight, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // 7 card straight
            hand = Hand.FromString("5H 6D 7D TC 8C 9S 4D");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Straight, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Low straight
            hand = Hand.FromString("AH 2D 4D 5C TC 9S 3D");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Straight, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Low 6 card straight
            hand = Hand.FromString("AH 2D 4D 5C 6C 9S 3D");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Straight, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // 7 card high straight
            hand = Hand.FromString("9H TD QD KC JC AS 8D");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Straight, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Nut straight
            hand = Hand.FromString("JH QD TS AS 6C KS 3D");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Straight, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);
        }

        [TestMethod]
        public void CanEvaluateStraightFlush()
        {
            // High straight flush
            var hand = Hand.FromString("9D TD JD QD 6S KD 4C");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.StraightFlush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Low straight flush
            hand = Hand.FromString("AS 2S 4S 3S 8S 5S");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.StraightFlush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Mid straight flush
            hand = Hand.FromString("6H 8H 7H 9H 5H 3S AS");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.StraightFlush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Straight & Flush but not a straight flush - should return just flush
            hand = Hand.FromString("JH 8H 7H 9H 5H 6S AS");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Flush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);
        }

        [TestMethod]
        public void CanEvaluateRoyalFlush()
        {
            // Royal flush
            var hand = Hand.FromString("TC JC QC KC AC 6S 3D");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.RoyalFlush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // 7 card but still royal flush
            hand = Hand.FromString("TC JC QC KC AC 9C 8C");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.RoyalFlush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Not quite a royal flush - this should just return flush
            hand = Hand.FromString("TC JC QC KC AS 6C 3C");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Flush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);
        }

        [TestMethod]
        public void CanEvaluateFlush()
        {
            // A flush
            var hand = Hand.FromString("6S 7S KS 2S JS AD");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.Flush, result.Strength);
            Assert.IsNull(result.Kickers);
            Assert.AreEqual(5, result.Core.Count);

            // Not a flush
            hand = Hand.FromString("6C 7S KS 2S JS AD");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreNotEqual(HandStrength.Flush, result.Strength);
        }

        [TestMethod]
        public void CanEvaluateFullHouse()
        {
            // Full House
            var hand = Hand.FromString("4H 4S TH 4D KH KD 7H");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.FullHouse, result.Strength);
            Assert.AreEqual(5, result.Core.Count);
            Assert.IsNull(result.Kickers);

            // Two full houses, 3s over Aces, 3s over 5s.
            // Should select 3s over Aces.
            hand = Hand.FromString("3S 3C 3D AH AD 5S 5H");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.FullHouse, result.Strength);
            Assert.AreEqual(5, result.Core.Count);
            Assert.IsNull(result.Kickers);
        }

        [TestMethod]
        public void CanEvaluateOfAKind()
        {
            // Three of a Kind
            var hand = Hand.FromString("AD 2S TH 4D AS KD AH");
            var result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.ThreeOfAKind, result.Strength);
            Assert.AreEqual(3, result.Core.Count);
            Assert.AreEqual(2, result.Kickers.Count);

            // Four of a Kind
            hand = Hand.FromString("JD JS JH 4D AS JD AH");
            result = HandEvaluation.Evaluate(hand);
            Assert.AreEqual(HandStrength.FourOfAKind, result.Strength);
            Assert.AreEqual(4, result.Core.Count);
            Assert.AreEqual(1, result.Kickers.Count);
        }
    }
}
