using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Domain;
using Poker.Domain.Players;
using Poker.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Tests
{
    [TestClass]
    public class Test_Simulation
    {
        [TestMethod]
        public void CanSimulateHands()
        {
            // Simulate 5 hands of AA vs 65s
            var simulator = new HandSimulator { NumberOfHands = 10 };
            simulator.Players = new List<Player>
            {
                new ComputerPlayer { Hand = Hand.FromString("AD AH"), Name = "Computer 1" },
                new ComputerPlayer { Hand = Hand.FromString("6C 5C"), Name = "Comptuer 2" },
            };

            simulator.Simulate();
            var results = simulator.GetPlayerWinsAsPercentage();

            Assert.AreEqual(simulator.Players.Count, results.Keys.Count);
            Assert.AreEqual(simulator.NumberOfHands, simulator.WinMap.Values.Sum());
        }

    }
}
