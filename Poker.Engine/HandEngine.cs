using Poker.Domain;
using Poker.Domain.Hands;
using Poker.Domain.Players;
using Poker.Domain.Pot;
using Poker.Engine.Events;
using Poker.Evaluator.Evaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Engine
{
    public class HandEngine
    {
        public Pot Pot { get; set; }
        public Deck Deck { get; set; }

        /// <summary>
        /// Event fired when the engine deals a player a hand.
        /// </summary>
        public event EventHandler<PlayerHandDealtEvent> PlayerHandDealt;

        /// <summary>
        /// Event fired when the engine deals the community cards.
        /// </summary>
        public event EventHandler<CommunityCardsDealtEvent> CommunityCardsDealt;

        /// <summary>
        /// Event fired when the pot has been completed.
        /// </summary>
        public event EventHandler<PotCompleteEvent> PotComplete;

        /// <summary>
        /// Runs the engine which plays out a hand.
        /// </summary>
        /// <returns></returns>
        public void Run()
        {
            Pot = new Pot();

            /*
             * Pre-flop
             */
            DealHoleCards();
            RoundOfBetting();

            /*
             * Post-flop
             */
            DealCommunityCards();
            RoundOfBetting();

            /*
             * Turn
             */
            DealCommunityCards();
            RoundOfBetting();

            /*
             * River
             */
            DealCommunityCards();
            RoundOfBetting();

            /*
             * Showdown
             */
            Showdown();
        }

        /// <summary>
        /// Deals down the two hole cards to each player in the pot.
        /// </summary>
        private void DealHoleCards()
        {
            if (Deck == null)
            {
                Deck = new Deck();
            }
            else
            {
                Deck.Create();
            }

            // Deal two cards to each player
            foreach (var player in Pot.Players)
            {
                player.Hand = Hand.FromCards(Deck.Take(2));
                PlayerHandDealt?.Invoke(this, new PlayerHandDealtEvent { Player = player });
            }
        }

        /// <summary>
        /// Each player remaining in the hand can make their action, e.g. call, fold, raise etc.
        /// </summary>
        private void RoundOfBetting()
        {
            Console.WriteLine("Simulating checks, calls, raises, folds, etc..");
            Pot.Total += 1000;
        }

        /// <summary>
        /// Deals the community cards to each player's hand.
        /// Three cards should be dealt for the flop.
        /// One card should be dealt for the turn.
        /// One card should be dealt for the river.
        /// </summary>
        /// <param name="amount"></param>
        private void DealCommunityCards()
        {
            var street = NextStreet;
            var communityCards = Deck.Take(street.NumberOfCards()).ToArray();

            foreach (var player in Pot.Players)
            {
                player.Hand.AddCards(communityCards);
            }

            CommunityCardsDealt?.Invoke(this, new CommunityCardsDealtEvent
            {
                Cards = communityCards,
                Street = street
            });
        }

        /// <summary>
        /// Works out how many cards we should deal based on how many cards a player currently has in their hand.
        /// When a player has only 2 cards in their hand, we deal 3 cards out for the flop.
        /// When a player has 5 cards in their hand, we deal 1 card for the turn.
        /// When a player has 6 cards in their hand, we deal 1 card for the river.
        /// </summary>
        private Street NextStreet => Pot.Players.First().Hand.Cards.Count switch
        {
            2 => Street.Flop,
            5 => Street.Turn,
            6 => Street.River,
            _ => throw new Exception("Unknown number of cards in hand.")
        };

        /// <summary>
        /// Works out which hand won the pot and moves the chips into that player's balance.
        /// </summary>
        private void Showdown()
        {
            // Evaluate each players hand to determine its strength etc.
            var results = new List<HandResult>();
            foreach (var player in Pot.Players)
            {
                results.Add(HandEvaluation.Evaluate(player.Hand));
            }

            // Sorts the hands by their strength. First item in the results list now is the best hand.
            results.Sort((h1, h2) => h2.CompareTo(h1));


            // Writes out hand summary and gives the winning player the pot total.
            var bestHand = results.FirstOrDefault();
            foreach (var player in Pot.Players)
            {

                /*
                 * Todo: need to be able to support chopped pots where there is no clear winning hand.
                 */
                if (player.Hand == bestHand.Hand)
                {
                    player.Balance += Pot.Total;

                    var result = new PotResult {
                        Pot = Pot,
                        Winners = new List<Player> { player },
                        HandResults = results
                    };
                    PotComplete?.Invoke(this, new PotCompleteEvent { Result = result });

                    return;
                }
            }
        }
    }
}
