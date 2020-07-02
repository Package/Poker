using Poker.Domain;
using Poker.Domain.Hands;
using Poker.Domain.Players;
using Poker.Evaluator.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Poker.Engine
{
    public class HandSimulator
    {
        /// <summary>
        /// The deck which will be used to simulate the hands.
        /// </summary>
        public Deck Deck { get; private set; }

        /// <summary>
        /// The players involved in this simulation
        /// </summary>
        public ICollection<Player> Players { get; set; }

        /// <summary>
        /// The number of hands to play in this simulation.
        /// </summary>
        public long NumberOfHands { get; set; }

        /// <summary>
        /// Maps the number of times each player wins in the simulation.
        /// </summary>
        public Dictionary<Player, int> WinMap { get; private set; }

        /// <summary>
        /// Creates the deck that will be used to simulate the hands.
        /// </summary>
        private void InitialiseDeck()
        {
            if (Deck == null)
            {
                Deck = new Deck();
            }
            else
            {
                Deck.Create();
            }

            Deck.RemovePlayerHoleCards(Players);
        }

        public void Simulate()
        {
            if (Players == null)
            {
                throw new InvalidOperationException("Cannot simulate hands without any players.");
            }

            WinMap = new Dictionary<Player, int>();

            for (var handNumber = 1; handNumber <= NumberOfHands; handNumber++)
            {
                InitialiseDeck();

                // Take the community cards
                var cards = Deck.Take(5).ToArray();

                var resultsMap = new Dictionary<Player, HandResult>();
                foreach (var player in Players)
                {
                    // Give community cards to each player
                    player.Hand.AddCards(cards);
                    var cardsInHand = player.Hand.Cards.Count;

                    // Evaluate their hand
                    resultsMap[player] = HandEvaluation.Evaluate(player.Hand);

                    // Remove community cards again after
                    player.Hand.Cards.RemoveAll(c => cards.Contains(c));
                    cardsInHand = player.Hand.Cards.Count;
                }

                // Sort results by best hand
                List<KeyValuePair<Player, HandResult>> resultsList = resultsMap.ToList();
                resultsList.Sort((h1, h2) => h2.Value.CompareTo(h1.Value));
                var bestResult = resultsList.FirstOrDefault();

                var winningString = $"{bestResult.Key.Name} wins with a {bestResult.Value}";

                // Update map containing results
                if (WinMap.ContainsKey(bestResult.Key))
                    WinMap[bestResult.Key] += 1;
                else
                    WinMap[bestResult.Key] = 1;
            }
        }

        /// <summary>
        /// Calculates the number of times each player won a hand in the simulation.
        /// </summary>
        /// <returns></returns>
        public Dictionary<Player, double> GetPlayerWinsAsPercentage()
        {
            var percentages = new Dictionary<Player, double>();

            foreach (var player in Players)
            {
                if (WinMap.ContainsKey(player))
                {
                    // Number of Wins / Number of Hands
                    percentages[player] = WinMap[player] * 1.0 / NumberOfHands * 1.0;
                }
                else
                {
                    // Never won a hand, win percent is 0%
                    percentages[player] = 0d;
                }
            }

            return percentages;
        }

        /// <summary>
        /// Creates the players that will be involved in the simulation.
        /// </summary>
        /// <param name="numberOfPlayers"></param>
        private void CreatePlayers(int numberOfPlayers)
        {
            if (Players == null)
            {
                Players = new List<Player>();

                for (var player = 1; player <= numberOfPlayers; player++)
                {
                    Players.Add(new ComputerPlayer { Name = $"Player {player}" });
                }
            }
        }
    }
}
