using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Poker.Domain.Players
{
    public abstract class Player
    {
        public string Name { get; set; }
        public Hand Hand { get; set; }
        public decimal Balance { get; set; }
    }
}
