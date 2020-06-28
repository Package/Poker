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
        /// From the hand provided, performs a full evaluation on it to determine:
        /// 
        /// 1) The hand strength
        /// 2) The core cards
        /// 3) Any kicker cards
        /// 
        /// For example, a hand of [AS KD 6C 5C 4D AH 8H] would return a HandResult of
        /// Strength: HandStrength.Pair
        /// Core: [AS, AH]
        /// Kickers: [KD, 8H, 6C]
        /// 
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        public static HandResult Evaluate(Hand hand)
        {
            var values = BuildValueMap(hand);

            /*
             * It's intentional that we're checking Four of a Kind and Full House before the Royal and Straight Flush,
             * as it's not possible to have either a Royal or Straight Flush if you do have Four of a Kind or a Full House.
             * 
             * Four of a Kind and Full House both do beat a regular flush however so we need to check for these first, as the 
             * logic for checking for Royal/Straight is tied in with a regular Flush.
             */

            // Four of a Kind
            var fourOfAKindResult = OfAKindResult(hand, values, 4);
            if (fourOfAKindResult != null)
            {
                return fourOfAKindResult;
            }

            // Full House
            var pairsResult = PairResult(hand, values);
            var threeOfAKindResult = OfAKindResult(hand, values, 3);
            if (threeOfAKindResult != null && pairsResult != null)
            {
                return FullHouseResult(threeOfAKindResult, pairsResult);
            }

            // Flush, Straight, Straight Flush, Royal Flush
            var flushCards = GetFlushCards(hand);
            var straightCards = GetStraightCards(hand);
            if (flushCards != null)
            {
                return FlushResult(flushCards, straightCards, hand);
            }
            if (straightCards != null)
            {
                return StraightResult(straightCards, hand);
            }

            // Three of a Kind
            if (threeOfAKindResult != null)
            {
                return threeOfAKindResult;
            }

            // Two Pair, One Pair
            if (pairsResult != null)
            {
                return pairsResult;
            }

            // High Card
            return HighCardResult(hand);
        }

        /// <summary>
        /// Handles building the HandResult for when the hand contains nothing but a High Card.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static HandResult HighCardResult(Hand hand)
        {
            var highestCard = hand.Cards.TakeBest(1);
            var kickers = hand.Cards.Except(highestCard).TakeBest(4);

            return new HandResult
            {
                Hand = hand,
                Strength = HandStrength.HighCard,
                Core = highestCard,
                Kickers = kickers
            };
        }

        /// <summary>
        /// Handles building the HandResult for when a hand contains pairs.
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private static HandResult PairResult(Hand hand, Dictionary<Value, int> values)
        {
            var pairedValues = new List<Value>();

            foreach (KeyValuePair<Value, int> item in values)
            {
                // Has exactly two of a value (paired)
                if (item.Value == 2)
                {
                    pairedValues.Add(item.Key);
                }
            }

            if (pairedValues.Count == 0)
            {
                // Did not have any pairs, so return nothing.
                return null;
            }
            else if (pairedValues.Count > 2)
            {
                // Limit to the best pairs, as it's possible to have more than 2
                pairedValues = pairedValues.OrderByDescending(p => p).Take(2).ToList();
            }

            var core = hand.Cards.Where(c => pairedValues.Contains(c.Value)).OrderByDescending(c => c.Value).ToList();
            var kickersToTake = pairedValues.Count switch
            {
                1 => 3, // One Pair contains two cards, we need another 3 to make the best five card hand
                2 => 1, // Two Pair contains four cards, we only need 1 more to make the best five card hand
                _ => throw new Exception($"Unsupported number of pairs: {pairedValues.Count}")
            };
            var kickers = hand.Cards.Except(core).TakeBest(kickersToTake);
            var strength = pairedValues.Count switch
            {
                1 => HandStrength.Pair,
                2 => HandStrength.TwoPair,
                _ => HandStrength.None
            };

            var result = new HandResult
            {
                Hand = hand,
                Strength = strength,
                Core = core,
                Kickers = kickers
            };

            return result;
        }

        /// <summary>
        /// Handles building the HandResult for when a hand contains X of a Kind.
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="values"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        private static HandResult OfAKindResult(Hand hand, Dictionary<Value, int> values, int amount)
        {
            Value bestValue = Value.None;

            foreach (KeyValuePair<Value, int> item in values)
            {
                // Has X of a kind.
                if (item.Value == amount)
                {
                    // It's possible with 7 cards to actually have two lots of 3 of a kind, so we need the take the highest.
                    if (item.Key > bestValue)
                    {
                        bestValue = item.Key;
                    }
                }
            }

            // Did not have any X of a kind, so return nothing.
            if (bestValue == Value.None)
            {
                return null;
            }

            var core = hand.Cards.Where(c => c.Value == bestValue).ToList();
            var kickers = hand.Cards.Except(core).TakeBest(5 - amount);
            var strength = amount switch
            {
                3 => HandStrength.ThreeOfAKind,
                4 => HandStrength.FourOfAKind,
                _ => HandStrength.None
            };

            var result = new HandResult {
                Hand = hand,
                Strength = strength,
                Core = core,
                Kickers = kickers
            };

            return result;
        }

        /// <summary>
        /// Handles building the HandResult for when a hand contains a straight.
        /// </summary>
        /// <param name="straightCards"></param>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static HandResult StraightResult(IEnumerable<Card> straightCards, Hand hand)
        {
            return new HandResult
            {
                Strength = HandStrength.Straight,
                Core = straightCards.TakeBest(),
                Hand = hand
            };
        }

        /// <summary>
        /// Handles building the HandResult for when a hand contains a flush.
        /// Note that this returns the best flush that is possible, it could be a Royal, Straight or regular Flush.
        /// </summary>
        /// <param name="flushCards"></param>
        /// <param name="straightCards"></param>
        /// <returns></returns>
        private static HandResult FlushResult(IEnumerable<Card> flushCards, IEnumerable<Card> straightCards, Hand hand)
        {
            var result = new HandResult { Hand = hand };

            // No straight on the board - impossible to have anything better than a flush
            if (straightCards == null)
            {
                result.Strength = HandStrength.Flush;
                result.Core = flushCards.TakeBest();
                return result;
            }

            var straightFlushCards = new List<Card>();
            var flushSuit = flushCards.First().Suit;

            /*
             * Check for a Royal Flush.
             * 
             * Straight cards in range 10 to Ace need to be in same suit for Royal Flush
             */
            var values = (Value[])Enum.GetValues(typeof(Value));
            var tenToAce = values[^5..^0]; // Ten to Ace range
            foreach (var value in tenToAce)
            {
                var card = straightCards.FirstOrDefault(c => c.Value == value && c.Suit == flushSuit);
                if (card != null)
                {
                    straightFlushCards.Add(card);
                }
            }
            if (straightFlushCards.Count == 5)
            {
                // Found all five in the straight cards, this is a Royal Flush
                result.Strength = HandStrength.RoyalFlush;
                result.Core = straightFlushCards;
                return result;
            }

            /*
             * Check for a Straight Flush.
             * 
             * Five or more cards in the straight need to be of the same suit for the Straight Flush.
             */
            straightFlushCards.Clear();
            foreach (var card in straightCards)
            {
                if (flushCards.Contains(card))
                {
                    straightFlushCards.Add(card);
                }
                else
                {
                    // This card isn't, but we've already seen enough to know it's a straight flush
                    if (straightFlushCards.Count >= 5)
                    {
                        result.Strength = HandStrength.StraightFlush;
                        result.Core = straightFlushCards.TakeBest();
                        return result;
                    }
                    else
                    {
                        straightFlushCards.Clear();
                    }
                }
            }

            if (straightFlushCards.Count >= 5)
            {
                // If there's 5 or more cards matching in both the straight and the flush then it's a Straight Flush.
                result.Strength = HandStrength.StraightFlush;
                result.Core = straightFlushCards.TakeBest();
            }
            else
            {
                // Otherwise, it's just a regular Flush.
                result.Strength = HandStrength.Flush;
                result.Core = flushCards.TakeBest();
            }

            return result;
        }


        /// <summary>
        /// Handles building the HandResult for when a hand contains a full house. That is a combination of 
        /// both a three of a kind and a pair.
        /// </summary>
        /// <param name="threeOfAKind"></param>
        /// <param name="pairs"></param>
        /// <returns></returns>
        private static HandResult FullHouseResult(HandResult threeOfAKind, HandResult pairs)
        {
            var combinedCore = new List<Card>();
            combinedCore.AddRange(threeOfAKind.Core);

            if (pairs.Strength == HandStrength.TwoPair)
            {
                // If the pairs are a two pair, then just take the highest of those.
                // E.g. imagine a seven card hand as "3S 3C 3D AH AD 5S 5H"
                // This should take the Aces as they are a better pair than the fives.
                var maxPair = pairs.Core.Max(c => c.Value);
                combinedCore.AddRange(pairs.Core.Where(c => c.Value == maxPair));
            }
            else
            {
                combinedCore.AddRange(pairs.Core);
            }

            return new HandResult { Hand = threeOfAKind.Hand, Strength = HandStrength.FullHouse, Core = combinedCore };
        }


        /// <summary>
        /// Returns all the cards which are making this hand a flush (if any).
        /// If the hand does not contain a flush, then null is returned.
        /// </summary>
        /// <param name="hand"></param>
        /// <returns></returns>
        private static IEnumerable<Card> GetFlushCards(Hand hand)
        {
            var suitMap = BuildSuitMap(hand);

            var containsFlush = suitMap.Values.Max() >= 5;
            if (!containsFlush)
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
            var straightCards = new List<Card>
            {
                lastCard
            };

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
