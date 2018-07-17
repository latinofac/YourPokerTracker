namespace PokerTracker
{
    partial class frmResults
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
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.cboPlayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFilterHands = new System.Windows.Forms.Button();
            this.txtFilteredHands = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Location = new System.Drawing.Point(0, 36);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(463, 245);
            this.dgvResults.TabIndex = 0;
            // 
            // cboPlayer
            // 
            this.cboPlayer.FormattingEnabled = true;
            this.cboPlayer.Location = new System.Drawing.Point(90, 6);
            this.cboPlayer.Name = "cboPlayer";
            this.cboPlayer.Size = new System.Drawing.Size(235, 21);
            this.cboPlayer.TabIndex = 10;
            this.cboPlayer.SelectedIndexChanged += new System.EventHandler(this.cboPlayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Select Player:";
            // 
            // btnFilterHands
            // 
            this.btnFilterHands.Location = new System.Drawing.Point(371, 6);
            this.btnFilterHands.Name = "btnFilterHands";
            this.btnFilterHands.Size = new System.Drawing.Size(85, 23);
            this.btnFilterHands.TabIndex = 11;
            this.btnFilterHands.Text = "Filter Hands";
            this.btnFilterHands.UseVisualStyleBackColor = true;
            this.btnFilterHands.Click += new System.EventHandler(this.btnFilterHands_Click);
            // 
            // txtFilteredHands
            // 
            this.txtFilteredHands.Location = new System.Drawing.Point(334, 8);
            this.txtFilteredHands.Name = "txtFilteredHands";
            this.txtFilteredHands.ReadOnly = true;
            this.txtFilteredHands.Size = new System.Drawing.Size(31, 20);
            this.txtFilteredHands.TabIndex = 12;
            this.txtFilteredHands.Visible = false;
            // 
            // frmResults
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 281);
            this.Controls.Add(this.txtFilteredHands);
            this.Controls.Add(this.btnFilterHands);
            this.Controls.Add(this.cboPlayer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvResults);
            this.Name = "frmResults";
            this.Text = "Results!";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dgvResults;
        public System.Windows.Forms.ComboBox cboPlayer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFilterHands;
        public System.Windows.Forms.TextBox txtFilteredHands;
    }
}