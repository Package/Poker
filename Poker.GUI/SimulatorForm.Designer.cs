namespace Poker.GUI
{
    partial class SimulatorForm
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
            this.playerOneCardOne = new System.Windows.Forms.ComboBox();
            this.playerOneCardTwo = new System.Windows.Forms.ComboBox();
            this.playerTwoCardOne = new System.Windows.Forms.ComboBox();
            this.playerTwoCardTwo = new System.Windows.Forms.ComboBox();
            this.btnRunSim = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.numericHands = new System.Windows.Forms.NumericUpDown();
            this.lblPlayerTwoWins = new System.Windows.Forms.Label();
            this.lblPlayerOneWins = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHands)).BeginInit();
            this.SuspendLayout();
            // 
            // playerOneCardOne
            // 
            this.playerOneCardOne.FormattingEnabled = true;
            this.playerOneCardOne.Location = new System.Drawing.Point(48, 56);
            this.playerOneCardOne.Name = "playerOneCardOne";
            this.playerOneCardOne.Size = new System.Drawing.Size(121, 23);
            this.playerOneCardOne.TabIndex = 3;
            // 
            // playerOneCardTwo
            // 
            this.playerOneCardTwo.FormattingEnabled = true;
            this.playerOneCardTwo.Location = new System.Drawing.Point(48, 85);
            this.playerOneCardTwo.Name = "playerOneCardTwo";
            this.playerOneCardTwo.Size = new System.Drawing.Size(121, 23);
            this.playerOneCardTwo.TabIndex = 4;
            // 
            // playerTwoCardOne
            // 
            this.playerTwoCardOne.FormattingEnabled = true;
            this.playerTwoCardOne.Location = new System.Drawing.Point(239, 56);
            this.playerTwoCardOne.Name = "playerTwoCardOne";
            this.playerTwoCardOne.Size = new System.Drawing.Size(121, 23);
            this.playerTwoCardOne.TabIndex = 3;
            // 
            // playerTwoCardTwo
            // 
            this.playerTwoCardTwo.FormattingEnabled = true;
            this.playerTwoCardTwo.Location = new System.Drawing.Point(239, 85);
            this.playerTwoCardTwo.Name = "playerTwoCardTwo";
            this.playerTwoCardTwo.Size = new System.Drawing.Size(121, 23);
            this.playerTwoCardTwo.TabIndex = 4;
            // 
            // btnRunSim
            // 
            this.btnRunSim.Location = new System.Drawing.Point(255, 249);
            this.btnRunSim.Name = "btnRunSim";
            this.btnRunSim.Size = new System.Drawing.Size(105, 23);
            this.btnRunSim.TabIndex = 5;
            this.btnRunSim.Text = "Run Simulation";
            this.btnRunSim.UseVisualStyleBackColor = true;
            this.btnRunSim.Click += new System.EventHandler(this.btnRunSim_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Player One";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(239, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Player Two";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numericHands);
            this.groupBox1.Controls.Add(this.lblPlayerTwoWins);
            this.groupBox1.Controls.Add(this.lblPlayerOneWins);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.playerOneCardOne);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.playerOneCardTwo);
            this.groupBox1.Controls.Add(this.btnRunSim);
            this.groupBox1.Controls.Add(this.playerTwoCardOne);
            this.groupBox1.Controls.Add(this.playerTwoCardTwo);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(423, 288);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Simulator";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(36, 251);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "Hands";
            // 
            // numericHands
            // 
            this.numericHands.Location = new System.Drawing.Point(85, 249);
            this.numericHands.Name = "numericHands";
            this.numericHands.Size = new System.Drawing.Size(93, 23);
            this.numericHands.TabIndex = 9;
            // 
            // lblPlayerTwoWins
            // 
            this.lblPlayerTwoWins.AutoSize = true;
            this.lblPlayerTwoWins.Location = new System.Drawing.Point(239, 111);
            this.lblPlayerTwoWins.Name = "lblPlayerTwoWins";
            this.lblPlayerTwoWins.Size = new System.Drawing.Size(0, 15);
            this.lblPlayerTwoWins.TabIndex = 8;
            // 
            // lblPlayerOneWins
            // 
            this.lblPlayerOneWins.AutoSize = true;
            this.lblPlayerOneWins.Location = new System.Drawing.Point(48, 111);
            this.lblPlayerOneWins.Name = "lblPlayerOneWins";
            this.lblPlayerOneWins.Size = new System.Drawing.Size(0, 15);
            this.lblPlayerOneWins.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(-36, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "label6";
            // 
            // SimulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 312);
            this.Controls.Add(this.groupBox1);
            this.Name = "SimulatorForm";
            this.Text = "Hand Simulator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericHands)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox t;
        private System.Windows.Forms.TextBox x;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label playt;
        private System.Windows.Forms.ComboBox playerOneCardOne;
        private System.Windows.Forms.ComboBox playerOneCardTwo;
        private System.Windows.Forms.ComboBox playerTwoCardOne;
        private System.Windows.Forms.ComboBox playerTwoCardTwo;
        private System.Windows.Forms.Button btnRunSim;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numericHands;
        private System.Windows.Forms.Label yer;
        private System.Windows.Forms.Label lblPlayerOneWins;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblPlayerTwoWins;
        private System.Windows.Forms.Label l;
    }
}

