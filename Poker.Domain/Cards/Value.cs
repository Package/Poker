using System;

namespace Poker.Domain
{
    public enum Value
    {
        None, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace
    }

    public static class ValueMethods
    {
        public static int Difference(this Value currentValue, Value nextValue)
        {
            // Edge case which can treat an Ace as low.
            if (nextValue == Value.Two && currentValue == Value.Ace)
                return 1;

            return nextValue - currentValue;
        }

        /// <summary>
        /// Returns a one character string representation of the value.
        /// E.g. AsString(Value.Six) => "6"
        /// E.g. AsString(Value.Ace) => "A"
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string AsString(this Value value) => value switch
        {
            Value.Ace => "A",
            Value.King => "K",
            Value.Queen => "Q",
            Value.Jack => "J",
            Value.Ten => "T",
            Value.Nine => "9",
            Value.Eight => "8",
            Value.Seven => "7",
            Value.Six => "6",
            Value.Five => "5",
            Value.Four => "4",
            Value.Three => "3",
            Value.Two => "2",
            _ => throw new ArgumentException($"Unknown value {value}")
        };
    }
}
