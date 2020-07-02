using Poker.Domain;
using Poker.Domain.Players;
using Poker.Engine;
using Poker.Engine.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Poker.GUI
{
    public partial class GameForm : Form
    {
        public HandEngine HandEngine { get; set; }
        public BackgroundWorker BackgroundWorker { get; private set; }

        public GameForm()
        {
            InitializeComponent();
            PopulateComboItems();

            // Increase size on the hand selector. 
            numericHands.Maximum = int.MaxValue;

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;

            //HandEngine = new HandEngine();
            //HandEngine.PlayerHandDealt += HandEngine_PlayerHandDealt;
            //HandEngine.CommunityCardsDealt += HandEngine_CommunityCardsDealt;
            //HandEngine.PotComplete += HandEngine_PotComplete;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var cardOne = $"{(string)playerOneCardOne.SelectedItem} {(string)playerOneCardTwo.SelectedItem}";
            var cardTwo = $"{(string)playerTwoCardOne.SelectedItem} {(string)playerTwoCardTwo.SelectedItem}";

            var simulator = new HandSimulator { NumberOfHands = (int)numericHands.Value };
            var playerOne = new ComputerPlayer { Hand = Hand.FromString(cardOne), Name = "Player 1" };
            var playerTwo = new ComputerPlayer { Hand = Hand.FromString(cardTwo), Name = "Player 2" };
            simulator.Players = new List<Player> { playerOne, playerTwo };
            simulator.Simulate();

            var resultPercentages = simulator.GetPlayerWinsAsPercentage();
            lblPlayerOneWins.Text = $"{(resultPercentages[playerOne] * 100)}%";
            lblPlayerTwoWins.Text = $"{(resultPercentages[playerTwo] * 100)}%";
        }

        //private void HandEngine_PotComplete(object sender, PotCompleteEvent e)
        //{
        //    txtComplete.Invoke(new Action(delegate { 
        //        txtComplete.AppendText($"Pot #{e.Result.Pot.Id} completed." + Environment.NewLine); 
        //    }));
        //}

        //private void HandEngine_CommunityCardsDealt(object sender, CommunityCardsDealtEvent e)
        //{
        //    txtCommunity.Invoke(new Action(delegate { 
        //        txtCommunity.AppendText($"Cards dealt for {e.Street}" + Environment.NewLine); 
        //    }));
        //}

        //private void HandEngine_PlayerHandDealt(object sender, PlayerHandDealtEvent e)
        //{
        //    txtHole.Invoke(new Action(delegate { 
        //        txtHole.AppendText($"Cards dealt to {e.Player.Name}" + Environment.NewLine); 
        //    }));
        //}

        private void btnPlay_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Populate the combo boxes with all the cards in the deck.
        /// </summary>
        private void PopulateComboItems()
        {
            var deck = new Deck();
            // Deck randomises the cards by default. Order them so they are easier to select
            var orderedCards = deck.Cards.OrderBy(c => c.Suit).ThenBy(c => c.Value).ToList();
            foreach (var card in orderedCards)
            {
                var cardAsString = $"{card.Value.AsString()}{card.Suit.AsString()}";
                playerOneCardOne.Items.Add(cardAsString);
                playerOneCardTwo.Items.Add(cardAsString);

                playerTwoCardOne.Items.Add(cardAsString);
                playerTwoCardTwo.Items.Add(cardAsString);
            }
        }

        /// <summary>
        /// Runs the hand simulator.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRunSim_Click(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync();
        }
    }
}
