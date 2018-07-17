namespace PokerTracker
{
    partial class frmSessions
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
            this.gridSessions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridSessions)).BeginInit();
            this.SuspendLayout();
            // 
            // gridSessions
            // 
            this.gridSessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSessions.Location = new System.Drawing.Point(4, 4);
            this.gridSessions.Name = "gridSessions";
            this.gridSessions.Size = new System.Drawing.Size(617, 360);
            this.gridSessions.TabIndex = 0;
            // 
            // frmSessions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 370);
            this.Controls.Add(this.gridSessions);
            this.Name = "frmSessions";
            this.Text = "frmSessions";
            ((System.ComponentModel.ISupportInitialize)(this.gridSessions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView gridSessions;
    }
}