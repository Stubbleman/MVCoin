﻿namespace MVCoin
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(280, 248);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Satellite";
            this.Text = "Satellite";
            this.Load += new System.EventHandler(this.Satellite_Load);
            this.Shown += new System.EventHandler(this.Satellite_Shown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}