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
    }
}
