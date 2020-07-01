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

            BackgroundWorker = new BackgroundWorker();
            BackgroundWorker.DoWork += BackgroundWorker_DoWork;

            HandEngine = new HandEngine();
            HandEngine.PlayerHandDealt += HandEngine_PlayerHandDealt;
            HandEngine.CommunityCardsDealt += HandEngine_CommunityCardsDealt;
            HandEngine.PotComplete += HandEngine_PotComplete;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            HandEngine.Run();
        }

        private void HandEngine_PotComplete(object sender, PotCompleteEvent e)
        {
            txtComplete.Invoke(new Action(delegate { 
                txtComplete.AppendText($"Pot #{e.Result.Pot.Id} completed." + Environment.NewLine); 
            }));
        }

        private void HandEngine_CommunityCardsDealt(object sender, CommunityCardsDealtEvent e)
        {
            txtCommunity.Invoke(new Action(delegate { 
                txtCommunity.AppendText($"Cards dealt for {e.Street}" + Environment.NewLine); 
            }));
        }

        private void HandEngine_PlayerHandDealt(object sender, PlayerHandDealtEvent e)
        {
            txtHole.Invoke(new Action(delegate { 
                txtHole.AppendText($"Cards dealt to {e.Player.Name}" + Environment.NewLine); 
            }));
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            BackgroundWorker.RunWorkerAsync();
        }
    }
}
