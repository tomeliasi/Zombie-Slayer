using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            ammoCount = new Label();
            kills = new Label();
            health = new Label();
            healthBar = new ProgressBar();
            GameTimer = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // ammoCount
            // 
            ammoCount.AutoSize = true;
            ammoCount.BackColor = Color.Transparent;
            ammoCount.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            ammoCount.ForeColor = Color.White;
            ammoCount.Location = new Point(14, 12);
            ammoCount.Name = "ammoCount";
            ammoCount.Size = new Size(144, 41);
            ammoCount.TabIndex = 0;
            ammoCount.Text = "Ammo: 0";
            ammoCount.TextAlign = ContentAlignment.TopRight;
            // 
            // kills
            // 
            kills.AutoSize = true;
            kills.BackColor = Color.Transparent;
            kills.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            kills.ForeColor = Color.White;
            kills.Location = new Point(304, 12);
            kills.Name = "kills";
            kills.Size = new Size(110, 41);
            kills.TabIndex = 1;
            kills.Text = "Kiils: 0";
            kills.TextAlign = ContentAlignment.TopRight;
            // 
            // health
            // 
            health.AutoSize = true;
            health.BackColor = Color.Transparent;
            health.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            health.ForeColor = Color.White;
            health.Location = new Point(579, 12);
            health.Name = "health";
            health.Size = new Size(112, 41);
            health.TabIndex = 2;
            health.Text = "Health";
            health.TextAlign = ContentAlignment.TopRight;
            // 
            // healthBar
            // 
            healthBar.Location = new Point(688, 22);
            healthBar.Margin = new Padding(3, 4, 3, 4);
            healthBar.Name = "healthBar";
            healthBar.Size = new Size(222, 31);
            healthBar.TabIndex = 3;
            healthBar.Value = 100;
            // 
            // GameTimer
            // 
            GameTimer.Enabled = true;
            GameTimer.Interval = 20;
            GameTimer.Tick += mainTimerEvent;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(922, 653);
            Controls.Add(healthBar);
            Controls.Add(health);
            Controls.Add(kills);
            Controls.Add(ammoCount);
            Name = "Form1";
            Text = "Zombie Slayer";
            KeyDown += keyIsDown;
            KeyUp += keyIsUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label ammoCount;
        private Label kills;
        private Label health;
        private ProgressBar healthBar;
        private System.Windows.Forms.Timer GameTimer;
    }
}