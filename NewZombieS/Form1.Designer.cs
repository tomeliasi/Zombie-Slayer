﻿
namespace Zombie_Slayer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.healthBar = new System.Windows.Forms.ProgressBar();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.ammoCount = new System.Windows.Forms.Label();
            this.Kills = new System.Windows.Forms.Label();
            this.health = new System.Windows.Forms.Label();
            this.pause = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pause)).BeginInit();
            this.SuspendLayout();
            // 
            // healthBar
            // 
            this.healthBar.Location = new System.Drawing.Point(501, 15);
            this.healthBar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.healthBar.Name = "healthBar";
            this.healthBar.Size = new System.Drawing.Size(245, 49);
            this.healthBar.TabIndex = 1;
            this.healthBar.Value = 100;
            // 
            // GameTimer
            // 
            this.GameTimer.Enabled = true;
            this.GameTimer.Interval = 20;
            this.GameTimer.Tick += new System.EventHandler(this.mainTimerEvent);
            // 
            // ammoCount
            // 
            this.ammoCount.AutoSize = true;
            this.ammoCount.BackColor = System.Drawing.Color.Transparent;
            this.ammoCount.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.ammoCount.ForeColor = System.Drawing.Color.White;
            this.ammoCount.Location = new System.Drawing.Point(14, 15);
            this.ammoCount.Name = "ammoCount";
            this.ammoCount.Size = new System.Drawing.Size(144, 41);
            this.ammoCount.TabIndex = 5;
            this.ammoCount.Text = "Ammo: 0";
            this.ammoCount.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Kills
            // 
            this.Kills.AutoSize = true;
            this.Kills.BackColor = System.Drawing.Color.Transparent;
            this.Kills.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.Kills.ForeColor = System.Drawing.Color.White;
            this.Kills.Location = new System.Drawing.Point(212, 15);
            this.Kills.Name = "Kills";
            this.Kills.Size = new System.Drawing.Size(110, 41);
            this.Kills.TabIndex = 1;
            this.Kills.Text = "Kills: 0";
            this.Kills.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // health
            // 
            this.health.AutoSize = true;
            this.health.BackColor = System.Drawing.Color.Transparent;
            this.health.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.health.ForeColor = System.Drawing.Color.White;
            this.health.Location = new System.Drawing.Point(383, 15);
            this.health.Name = "health";
            this.health.Size = new System.Drawing.Size(112, 41);
            this.health.TabIndex = 2;
            this.health.Text = "Health";
            this.health.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pause
            // 
            this.pause.Image = global::Zombie_Slayer.Properties.Resources.pause;
            this.pause.Location = new System.Drawing.Point(810, 12);
            this.pause.Name = "pause";
            this.pause.Size = new System.Drawing.Size(84, 74);
            this.pause.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pause.TabIndex = 6;
            this.pause.TabStop = false;
            this.pause.Tag = "pause";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(922, 653);
            this.Controls.Add(this.pause);
            this.Controls.Add(this.health);
            this.Controls.Add(this.Kills);
            this.Controls.Add(this.ammoCount);
            this.Controls.Add(this.healthBar);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Zombie Slayer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyIsUp);
            ((System.ComponentModel.ISupportInitialize)(this.pause)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar healthBar;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label ammoCount;
        private System.Windows.Forms.Label Kills;
        private System.Windows.Forms.Label health;
        private System.Windows.Forms.PictureBox pause;
    }
}

