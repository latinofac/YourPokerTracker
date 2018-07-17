namespace PokerTracker
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridPlayers = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.btnSessions = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnClearDB = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnHands = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnResults = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridPlayers)).BeginInit();
            this.SuspendLayout();
            // 
            // gridPlayers
            // 
            this.gridPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gridPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridPlayers.Location = new System.Drawing.Point(12, 44);
            this.gridPlayers.Name = "gridPlayers";
            this.gridPlayers.Size = new System.Drawing.Size(687, 528);
            this.gridPlayers.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Find Player:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(81, 10);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(185, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(105, 150);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(499, 23);
            this.prgBar.TabIndex = 3;
            // 
            // btnSessions
            // 
            this.btnSessions.Location = new System.Drawing.Point(705, 87);
            this.btnSessions.Name = "btnSessions";
            this.btnSessions.Size = new System.Drawing.Size(126, 126);
            this.btnSessions.TabIndex = 4;
            this.btnSessions.UseVisualStyleBackColor = true;
            this.btnSessions.Click += new System.EventHandler(this.btnSessions_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(861, 268);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(126, 23);
            this.btnTest.TabIndex = 5;
            this.btnTest.Text = "Test HV";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Visible = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnClearDB
            // 
            this.btnClearDB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClearDB.Location = new System.Drawing.Point(861, 446);
            this.btnClearDB.Name = "btnClearDB";
            this.btnClearDB.Size = new System.Drawing.Size(126, 126);
            this.btnClearDB.TabIndex = 6;
            this.btnClearDB.UseVisualStyleBackColor = true;
            this.btnClearDB.Click += new System.EventHandler(this.btnClearDB_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(705, 268);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(126, 126);
            this.btnLoad.TabIndex = 7;
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnHands
            // 
            this.btnHands.Location = new System.Drawing.Point(861, 87);
            this.btnHands.Name = "btnHands";
            this.btnHands.Size = new System.Drawing.Size(126, 126);
            this.btnHands.TabIndex = 8;
            this.btnHands.UseVisualStyleBackColor = true;
            this.btnHands.Click += new System.EventHandler(this.btnHands_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnConfig.Location = new System.Drawing.Point(705, 446);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(126, 126);
            this.btnConfig.TabIndex = 9;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnResults
            // 
            this.btnResults.Location = new System.Drawing.Point(1017, 87);
            this.btnResults.Name = "btnResults";
            this.btnResults.Size = new System.Drawing.Size(126, 126);
            this.btnResults.TabIndex = 10;
            this.btnResults.UseVisualStyleBackColor = true;
            this.btnResults.Click += new System.EventHandler(this.btnResults_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(735, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Sessions";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(902, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Hands";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1052, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "Results";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(712, 421);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Configurations";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(712, 239);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Load Hands";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(889, 421);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Clear DB";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1208, 57);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 20);
            this.label8.TabIndex = 17;
            this.label8.Text = "Exit";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1171, 87);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(126, 126);
            this.btnExit.TabIndex = 18;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 584);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnResults);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnHands);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnClearDB);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSessions);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridPlayers);
            this.Name = "frmMain";
            this.Text = "Welcome to Your Tracker!";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gridPlayers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gridPlayers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Button btnSessions;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnClearDB;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnHands;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnExit;
    }
}