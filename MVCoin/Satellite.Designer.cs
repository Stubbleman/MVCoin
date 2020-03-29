namespace MVCoin
{
    partial class Satellite
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
            this.SuspendLayout();
            // 
            // Satellite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 175);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = new System.Drawing.Point(10, 10);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Satellite";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Satellite";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Satellite_Load);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Satellite_MouseDoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Satellite_MouseDown);
            this.MouseEnter += new System.EventHandler(this.Satellite_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.Satellite_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Satellite_MouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}