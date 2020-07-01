using Poker.Domain.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Engine.Events
{
    public class PlayerHandDealtEvent : EventArgs
    {
        public Player Player { get; set; }
    }
}
