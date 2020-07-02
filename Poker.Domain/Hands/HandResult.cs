using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Poker.Domain.Hands
{
    public class HandResult : IComparable<HandResult>
    {
        public Hand Hand { get; set; }
        public HandStrength Strength { get; set; }
        public IList<Card> Core { get; set; }
        public IList<Card> Kickers { get; set; }

        /// <summary>
        /// Compares two HandResult objects to determine which one is higher ranked.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(HandResult other)
        {
            // Firstly, check on the hand strength to see if there is a clear winner.
            // E.g. a Flush beats a 3 of a Kind and there's no need to check any further.
            if (Strength > other.Strength)
                return 1;
            else if (Strength < other.Strength)
                return -1;
            else
            {
                // Compare all the core cards and see if we can determine a best hand.
                // E.g. a Pair of Kings would beat a Pair of Tens etc.
                for (var coreIndex = 0; coreIndex < Core.Count; coreIndex++)
                {
                    var comparison = Core[coreIndex].CompareTo(other.Core[coreIndex]);
                    if (comparison == 0)
                        continue;

                    return comparison;
                }

                // Got here then all core cards compare equally, e.g. both got the same pair.
                // Now try compare on the kickers.
                if (Kickers != null)
                {
                    for (var kickerIndex = 0; kickerIndex < Kickers.Count; kickerIndex++)
                    {
                        var comparison = Kickers[kickerIndex].CompareTo(other.Kickers[kickerIndex]);
                        if (comparison == 0)
                            continue;

                        return comparison;
                    }
                }
            }

            // Got here then there truly was no way to separate these hands.
            return 0;
        }

        /// <summary>
        /// Builds a string representation of the winning cards.
        /// E.g. FullHouse (Jack of Spades, Jack of Diamonds, Jack of Hearts, Eight of Clubs, Eight of Diamonds)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var str = Strength.ToString() + " (";
            
            for (var coreIndex = 0; coreIndex < Core.Count; coreIndex++)
            {
                str += Core[coreIndex].ToString();
                if (coreIndex < Core.Count - 1)
                    str += ", ";
            }

            str += "" + ")";

            return str;
        }
    }
}
