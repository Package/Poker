using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Domain;
using Poker.Evaluator.Evaluation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Tests
{
    [TestClass]
    public class Test_FiveCardEvaluator
    {
        //[TestMethod]
        //public void CanEvaluateBestFiveCardsForPair()
        //{
        //    // Best five are: AS AD KD 8H 6H
        //    var hand = Hand.FromString("AS KD 6H 5S 4C AD 8H");
        //    var strength = HandEvaluation.Strength(hand);

        //    var result = BestFiveCardEvaluator.Evaluate(hand, strength);
        //    Assert.AreEqual(3, result.Kickers.Count);
        //    Assert.AreEqual(2, result.Core.Count);
        //}
    }
}
