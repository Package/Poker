using Poker.Domain.Pot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poker.Engine.Events
{
    public class PotCompleteEvent : EventArgs
    {
        public PotResult Result { get; set; }
    }
}
