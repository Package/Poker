using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Poker.Domain
{
    public class Card : IComparable<Card>
    {
        public Value Value { get; set; }

        public Suit Suit { get; set; }

        /// <summary>
        /// Builds a card from the provided string. 
        /// 
        /// E.g: "KS" would return a Card as King Spades
        /// E.g: "9D" would return a Card as Nine Diamonds
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public static Card FromString(string card)
        {
            if (card.Length != 2)
            {
                throw new ArgumentException($"Card must be length of two. Encountered {card} which is length {card.Length}");
            }

            var value = card[0] switch {
                'A' => Value.Ace,
                'K' => Value.King,
                'Q' => Value.Queen,
                'J' => Value.Jack,
                'T' => Value.Ten,
                '9' => Value.Nine,
                '8' => Value.Eight,
                '7' => Value.Seven,
                '6' => Value.Six,
                '5' => Value.Five,
                '4' => Value.Four,
                '3' => Value.Three,
                '2' => Value.Two,
                _ => throw new ArgumentException($"Unknown value in provided card: {card}")
            };

            var suit = card[1] switch {
                'C' => Suit.Clubs,
                'H' => Suit.Hearts,
                'D' => Suit.Diamonds,
                'S' => Suit.Spades,
                _ => throw new ArgumentException($"Unknown suit in provided card: {card}")
            };

            return new Card { Value = value, Suit = suit };
        }

        /// <summary>
        /// Compares two Card objects and returns which one is ranked higher.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Card other)
        {
            if (Value > other.Value)
                return 1;
            else if (Value < other.Value)
                return -1;
            return 0;
        }
    }
}

