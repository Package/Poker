namespace Poker.Domain
{
    public enum Suit
    {
        Spades, Clubs, Hearts, Diamonds
    }

    // Example of an applying an extension method to an enum.

    public static class SuitMethods
    {
        public static string ToString(this Suit suit)
        {
            return suit switch
            {
                _ => "Hello World"
            };
        }

    }
}
