using Poker.Domain;
using Poker.Domain.Pot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Engine.Events
{
    public class CommunityCardsDealtEvent : EventArgs
    {
        public Card[] Cards { get; set; }
        public Street Street { get; set; }
    }
}
