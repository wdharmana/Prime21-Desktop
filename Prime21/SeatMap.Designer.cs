namespace Prime21
{
    partial class SeatMap
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
            this.panelScreen = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelScreen
            // 
            this.panelScreen.BackColor = System.Drawing.Color.Black;
            this.panelScreen.Location = new System.Drawing.Point(12, 12);
            this.panelScreen.Name = "panelScreen";
            this.panelScreen.Size = new System.Drawing.Size(859, 28);
            this.panelScreen.TabIndex = 0;
            // 
            // SeatMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 480);
            this.Controls.Add(this.panelScreen);
            this.Name = "SeatMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Seat Map";
            this.Load += new System.EventHandler(this.SeatMap_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelScreen;
    }
}