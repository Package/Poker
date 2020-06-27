using Poker.Domain;
using Poker.Domain.Hands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Evaluator.Evaluation
{
    public class BestFiveCardEvaluator
    {

        /// <summary>
        /// Given a provided hand, evaluate the best five cards from the hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="strength"></param>
        public static HandResult Evaluate(Hand hand, HandStrength strength)
        {
            var result = new HandResult { Hand = hand, Strength = strength };

            if (hand.Cards.Count < 7)
            {
                throw new ArgumentException("Cannot evaluate hand with less than 7 cards.");
            }

            return result;
        }
    }
}
