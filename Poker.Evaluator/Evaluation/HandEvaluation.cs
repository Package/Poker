using Poker.Domain;
using Poker.Domain.Hands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Poker.Evaluator.Evaluation
{
    public sealed class HandEvaluation
    {
        /// <summary>
        /// From the hand provided, calculates the strength of it.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static HandStrength Strength(Hand hand)
        {
            var values = BuildValueMap(hand);

            var flush = GetFlushCards(hand);
            var straight = GetStraightCards(hand);
            var pairs = Pairs(values);
            var trips = OfAKind(values, 3);
            var quads = OfAKind(values, 4);

            if (quads > 0) 
                return HandStrength.FourOfAKind;

            if (trips > 0 && pairs > 0) 
                return HandStrength.FullHouse;

            if (flush != null) 
                return BestFlush(flush, straight);

            if (straight != null) 
                return HandStrength.Straight;

            if (trips > 0) 
                return HandStrength.ThreeOfAKind;

            if (pairs >= 2) 
                return HandStrength.TwoPair;

            if (pairs == 1) 
                return HandStrength.Pair;

            return HandStrength.HighCard;
        }

        /// <summary>
        /// Returns the number of pairs in the hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static int Pairs(Dictionary<Value, int> values) => values.Values.Count(v => v == 2);

        /// <summary>
        /// Returns how many values of the same kind are in the hand.
        /// E.g. OfAKind(values, 3) would return how many three of a kinds are in the hand values.
        /// E.g. OfAKind(values, 4) would return how many four of a kinds are in the hand values.
        /// </summary>
        /// <param name="values"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static int OfAKind(Dictionary<Value, int> values, int amount) => values.Values.Count(v => v == amount);

        /// <summary>
        /// Returns the best flush that is possible. Checks for Royal and Straight flushes.
        /// </summary>
        /// <param name="flushCards"></param>
        /// <param name="straightCards"></param>
        /// <returns></returns>
        private static HandStrength BestFlush(IEnumerable<Card> flushCards, IEnumerable<Card> straightCards)
        {
            // No straight on the board - impossible to have anything better than a flush
            if (straightCards == null)
                return HandStrength.Flush;

            var counter = 0;
            var flushSuit = flushCards.First().Suit;

            /*
             * Check for royal flush.
             * Straight cards in range 10 to Ace need to be in same suit for royal flush
             */ 
            var values = (Value[]) Enum.GetValues(typeof(Value));
            var tenToAce = values[^5..^0]; // Ten to Ace range
            foreach (var value in tenToAce)
            {
                var card = straightCards.FirstOrDefault(c => c.Value == value && c.Suit == flushSuit);
                if (card != null)
                    counter += 1;
            }
            if (counter == 5)
            {
                // Found all five in the straight cards, this is a royal flush
                return HandStrength.RoyalFlush;
            }

            /*
             * Check for straight flush.
             * Five or more cards in the straight need to be of the same suit for the straight flush
             */
            counter = 0;
            foreach (var card in straightCards)
            {
                if (flushCards.Contains(card))
                {
                    counter += 1;
                }
                else
                {
                    // This card isn't, but we've already seen enough to know it's a straight flush
                    if (counter >= 5)
                        return HandStrength.StraightFlush;
                    else
                        counter = 0;
                }
            }

            return counter >= 5 ? HandStrength.StraightFlush : HandStrength.Flush;
        }

        /// <summary>
        /// Determines whether the hand is a flush.
        /// That is, five or more cards are of the same suit.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static bool IsFlush(Dictionary<Suit, int> suits) => suits.Values.Max() >= 5;

        /// <summary>
        /// Returns all the cards which are making this hand a flush (if any).
        /// If the hand does not contain a flush, then null is returned.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static IEnumerable<Card> GetFlushCards(Hand hand)
        {
            var suitMap = BuildSuitMap(hand);
            if (!IsFlush(suitMap))
                return null;

            var flushSuit = suitMap.FirstOrDefault(s => s.Value >= 5).Key;
            return hand.Cards.Where(c => c.Suit == flushSuit);
        }

        /// <summary>
        /// Returns the cards in the provided hand which make a straight.
        /// Note that this will return all the cards in the straight, even if there is more than 5.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static IEnumerable<Card> GetStraightCards(Hand hand)
        {
            var orderedCards = hand.Cards.OrderBy(c => c.Value).ToList();
            
            // If there's an ace in the hand, put this to the beginning to count for 
            // low straight (Ace - 5)
            var ace = hand.Cards.FirstOrDefault(c => c.Value == Value.Ace);
            if (ace != null)
            {
                orderedCards.Insert(0, ace);
            }

            var lastCard = orderedCards.FirstOrDefault();
            var straightCards = new List<Card>();
            straightCards.Add(lastCard);

            for (var index = 1; index < orderedCards.Count; index++)
            {
                var currentCard = orderedCards[index];
                var valueDifference = lastCard.Value.Difference(currentCard.Value);

                if (valueDifference == 1)
                {
                    // Difference of exactly 1 means the cards are one value apart, e.g Five -> Six
                    straightCards.Add(currentCard);
                }
                else if (valueDifference == 0)
                {
                    // Difference of exactly 0 means the card is the same as the previously seen card, e.g. Jack -> Jack
                    // In this case, we don't need to do anything...

                    // Todo: we may need to add this to the set of straight cards anyway to account for straight flush situations.
                }
                else
                {
                    // Difference of anything else means the card is a lot further up in the chain e.g Seven -> Ten.
                    // If not already a straight then this needs to reset as these cards cannot make a straight
                    if (straightCards.Count >= 5)
                        return straightCards;
                    else
                    {
                        straightCards.Clear();
                        straightCards.Add(currentCard);
                    }
                }

                lastCard = currentCard;
            }

            return straightCards.Count >= 5 ? straightCards : null;
        }

        /// <summary>
        /// Builds a map of the values in the hand. How many times each value appears in the hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static Dictionary<Value, int> BuildValueMap(Hand hand)
        {
            var valueMap = new Dictionary<Value, int>();
            foreach (var card in hand.Cards)
            {
                if (valueMap.ContainsKey(card.Value))
                {
                    valueMap[card.Value] += 1;
                }
                else
                {
                    valueMap[card.Value] = 1;
                }
            }

            return valueMap;
        }

        /// <summary>
        /// Builds a map of each suit in the hand. How many times each suit appears in the hand.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static Dictionary<Suit, int> BuildSuitMap(Hand hand)
        {
            var suitMap = new Dictionary<Suit, int>();
            foreach (var card in hand.Cards)
            {
                if (suitMap.ContainsKey(card.Suit))
                {
                    suitMap[card.Suit] += 1;
                }
                else
                {
                    suitMap[card.Suit] = 1;
                }
            }

            return suitMap;
        }
    }
}
