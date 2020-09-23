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
    public partial class SimulatorForm : Form
    {
        public BackgroundWorker BackgroundWorker { get; private set; }

        public SimulatorForm()
        {
            InitializeComponent();
            PopulateComboItems();

            // Increase size on the hand selector. 
            numericHands.Maximum = int.MaxValue;
            numericHands.Value = 50; // By Default

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;
        }

        /// <summary>
        /// Performs the work to simulate the hands in a background thread.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            lblPlayerOneWins.Text = string.Empty;
            lblPlayerTwoWins.Text = string.Empty;

            var cardOne = $"{(string)playerOneCardOne.SelectedItem} {(string)playerOneCardTwo.SelectedItem}";
            var cardTwo = $"{(string)playerTwoCardOne.SelectedItem} {(string)playerTwoCardTwo.SelectedItem}";

            var simulator = new HandSimulator { NumberOfHands = (int)numericHands.Value };
            var playerOne = new ComputerPlayer { Hand = Hand.FromString(cardOne), Name = "Player 1" };
            var playerTwo = new ComputerPlayer { Hand = Hand.FromString(cardTwo), Name = "Player 2" };
            simulator.Players = new List<Player> { playerOne, playerTwo };
            simulator.Simulate();

            var resultPercentages = simulator.GetPlayerWinsAsPercentage();
            lblPlayerOneWins.Text = $"Wins ({simulator.WinMap[playerOne]}): {simulator.FormatWinPercentage(resultPercentages[playerOne])}%";
            lblPlayerTwoWins.Text = $"Wins ({simulator.WinMap[playerTwo]}): {simulator.FormatWinPercentage(resultPercentages[playerTwo])}%";
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
