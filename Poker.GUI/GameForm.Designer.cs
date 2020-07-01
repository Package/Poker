namespace Poker.GUI
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtHole = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCommunity = new System.Windows.Forms.TextBox();
            this.txtComplete = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPlay = new System.Windows.Forms.Button();
            this.player1card2 = new System.Windows.Forms.Label();
            this.player1card1 = new System.Windows.Forms.Label();
            this.player2card1 = new System.Windows.Forms.Label();
            this.player2card2 = new System.Windows.Forms.Label();
            this.player3card1 = new System.Windows.Forms.Label();
            this.player3card2 = new System.Windows.Forms.Label();
            this.player4card1 = new System.Windows.Forms.Label();
            this.player4card2 = new System.Windows.Forms.Label();
            this.commCard1 = new System.Windows.Forms.Label();
            this.commCard2 = new System.Windows.Forms.Label();
            this.commCard3 = new System.Windows.Forms.Label();
            this.commCard4 = new System.Windows.Forms.Label();
            this.commCard5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtHole
            // 
            this.txtHole.Location = new System.Drawing.Point(21, 40);
            this.txtHole.Multiline = true;
            this.txtHole.Name = "txtHole";
            this.txtHole.Size = new System.Drawing.Size(253, 143);
            this.txtHole.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hole Cards";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Community Dealt";
            // 
            // txtCommunity
            // 
            this.txtCommunity.Location = new System.Drawing.Point(298, 40);
            this.txtCommunity.Multiline = true;
            this.txtCommunity.Name = "txtCommunity";
            this.txtCommunity.Size = new System.Drawing.Size(253, 143);
            this.txtCommunity.TabIndex = 0;
            // 
            // txtComplete
            // 
            this.txtComplete.Location = new System.Drawing.Point(575, 40);
            this.txtComplete.Multiline = true;
            this.txtComplete.Name = "txtComplete";
            this.txtComplete.Size = new System.Drawing.Size(253, 143);
            this.txtComplete.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(575, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Hand Complete";
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(21, 200);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(75, 23);
            this.btnPlay.TabIndex = 2;
            this.btnPlay.Text = "Play Hand";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // player1card2
            // 
            this.player1card2.AutoSize = true;
            this.player1card2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player1card2.ForeColor = System.Drawing.Color.DarkRed;
            this.player1card2.Location = new System.Drawing.Point(393, 535);
            this.player1card2.Name = "player1card2";
            this.player1card2.Size = new System.Drawing.Size(85, 15);
            this.player1card2.TabIndex = 3;
            this.player1card2.Text = "J of Diamonds";
            // 
            // player1card1
            // 
            this.player1card1.AutoSize = true;
            this.player1card1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player1card1.Location = new System.Drawing.Point(393, 520);
            this.player1card1.Name = "player1card1";
            this.player1card1.Size = new System.Drawing.Size(61, 15);
            this.player1card1.TabIndex = 4;
            this.player1card1.Text = "5 of Clubs";
            // 
            // player2card1
            // 
            this.player2card1.AutoSize = true;
            this.player2card1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player2card1.Location = new System.Drawing.Point(59, 376);
            this.player2card1.Name = "player2card1";
            this.player2card1.Size = new System.Drawing.Size(61, 15);
            this.player2card1.TabIndex = 4;
            this.player2card1.Text = "5 of Clubs";
            // 
            // player2card2
            // 
            this.player2card2.AutoSize = true;
            this.player2card2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player2card2.ForeColor = System.Drawing.Color.DarkRed;
            this.player2card2.Location = new System.Drawing.Point(59, 391);
            this.player2card2.Name = "player2card2";
            this.player2card2.Size = new System.Drawing.Size(85, 15);
            this.player2card2.TabIndex = 3;
            this.player2card2.Text = "J of Diamonds";
            // 
            // player3card1
            // 
            this.player3card1.AutoSize = true;
            this.player3card1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player3card1.ForeColor = System.Drawing.Color.Green;
            this.player3card1.Location = new System.Drawing.Point(393, 247);
            this.player3card1.Name = "player3card1";
            this.player3card1.Size = new System.Drawing.Size(61, 15);
            this.player3card1.TabIndex = 4;
            this.player3card1.Text = "5 of Clubs";
            // 
            // player3card2
            // 
            this.player3card2.AutoSize = true;
            this.player3card2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player3card2.ForeColor = System.Drawing.Color.DarkRed;
            this.player3card2.Location = new System.Drawing.Point(393, 262);
            this.player3card2.Name = "player3card2";
            this.player3card2.Size = new System.Drawing.Size(85, 15);
            this.player3card2.TabIndex = 3;
            this.player3card2.Text = "J of Diamonds";
            // 
            // player4card1
            // 
            this.player4card1.AutoSize = true;
            this.player4card1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player4card1.Location = new System.Drawing.Point(743, 376);
            this.player4card1.Name = "player4card1";
            this.player4card1.Size = new System.Drawing.Size(61, 15);
            this.player4card1.TabIndex = 4;
            this.player4card1.Text = "5 of Clubs";
            // 
            // player4card2
            // 
            this.player4card2.AutoSize = true;
            this.player4card2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.player4card2.ForeColor = System.Drawing.Color.DarkRed;
            this.player4card2.Location = new System.Drawing.Point(743, 391);
            this.player4card2.Name = "player4card2";
            this.player4card2.Size = new System.Drawing.Size(85, 15);
            this.player4card2.TabIndex = 3;
            this.player4card2.Text = "J of Diamonds";
            // 
            // commCard1
            // 
            this.commCard1.AutoSize = true;
            this.commCard1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.commCard1.ForeColor = System.Drawing.Color.Blue;
            this.commCard1.Location = new System.Drawing.Point(393, 349);
            this.commCard1.Name = "commCard1";
            this.commCard1.Size = new System.Drawing.Size(69, 15);
            this.commCard1.TabIndex = 4;
            this.commCard1.Text = "5 of Hearts";
            // 
            // commCard2
            // 
            this.commCard2.AutoSize = true;
            this.commCard2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.commCard2.ForeColor = System.Drawing.Color.DarkRed;
            this.commCard2.Location = new System.Drawing.Point(393, 364);
            this.commCard2.Name = "commCard2";
            this.commCard2.Size = new System.Drawing.Size(85, 15);
            this.commCard2.TabIndex = 3;
            this.commCard2.Text = "J of Diamonds";
            // 
            // commCard3
            // 
            this.commCard3.AutoSize = true;
            this.commCard3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.commCard3.Location = new System.Drawing.Point(393, 379);
            this.commCard3.Name = "commCard3";
            this.commCard3.Size = new System.Drawing.Size(61, 15);
            this.commCard3.TabIndex = 4;
            this.commCard3.Text = "5 of Clubs";
            // 
            // commCard4
            // 
            this.commCard4.AutoSize = true;
            this.commCard4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.commCard4.ForeColor = System.Drawing.Color.DarkRed;
            this.commCard4.Location = new System.Drawing.Point(393, 394);
            this.commCard4.Name = "commCard4";
            this.commCard4.Size = new System.Drawing.Size(85, 15);
            this.commCard4.TabIndex = 3;
            this.commCard4.Text = "J of Diamonds";
            // 
            // commCard5
            // 
            this.commCard5.AutoSize = true;
            this.commCard5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.commCard5.ForeColor = System.Drawing.Color.DarkRed;
            this.commCard5.Location = new System.Drawing.Point(393, 409);
            this.commCard5.Name = "commCard5";
            this.commCard5.Size = new System.Drawing.Size(85, 15);
            this.commCard5.TabIndex = 3;
            this.commCard5.Text = "J of Diamonds";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 597);
            this.Controls.Add(this.commCard5);
            this.Controls.Add(this.commCard4);
            this.Controls.Add(this.commCard3);
            this.Controls.Add(this.commCard2);
            this.Controls.Add(this.commCard1);
            this.Controls.Add(this.player4card2);
            this.Controls.Add(this.player4card1);
            this.Controls.Add(this.player3card2);
            this.Controls.Add(this.player3card1);
            this.Controls.Add(this.player2card2);
            this.Controls.Add(this.player2card1);
            this.Controls.Add(this.player1card1);
            this.Controls.Add(this.player1card2);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtComplete);
            this.Controls.Add(this.txtCommunity);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHole);
            this.Name = "GameForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox t;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox txtComplete;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtHole;
        private System.Windows.Forms.TextBox txtCommunity;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.Label player1card2;
        private System.Windows.Forms.Label player1card1;
        private System.Windows.Forms.Label playt;
        private System.Windows.Forms.Label player2card1;
        private System.Windows.Forms.Label player2card2;
        private System.Windows.Forms.Label player3card1;
        private System.Windows.Forms.Label player3card2;
        private System.Windows.Forms.Label player4card1;
        private System.Windows.Forms.Label player4card2;
        private System.Windows.Forms.Label commCard1;
        private System.Windows.Forms.Label commCard2;
        private System.Windows.Forms.Label commCard3;
        private System.Windows.Forms.Label commCard4;
        private System.Windows.Forms.Label commCard5;
    }
}

