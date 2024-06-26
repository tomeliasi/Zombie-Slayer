﻿namespace Zombie_Slayer
{
    class Bullet
    {
        public string direction = string.Empty;
        public int bulletLeft;
        public int bulletTop;
        private int speed = 20;
        private PictureBox bullet = new PictureBox(); 
        private System.Windows.Forms.Timer bulletTimer = new System.Windows.Forms.Timer();

        public void makeBullet(Form form)
        {
            bullet.BackColor = Color.Transparent;
            bullet.Image = Properties.Resources.bulletup;
            bullet.SizeMode = PictureBoxSizeMode.StretchImage;
            bullet.Size = new Size(20, 20);
            bullet.Tag = "bullet";
            bullet.Left = bulletLeft;
            bullet.Top = bulletTop;
            bullet.BringToFront();

            form.Controls.Add(bullet);

            bulletTimer.Interval = speed;
            bulletTimer.Tick += new EventHandler(BulletTimerEvent);
            bulletTimer.Start();
        }

        private void BulletTimerEvent(object sender,EventArgs e)
        {
            if (direction == "left")
            {
                bullet.Left -= speed;
                bullet.Image = Properties.Resources.bulletleft;
            }

            if (direction == "right")
            {
                bullet.Left += speed;
                bullet.Image = Properties.Resources.bulletright;
            }

            if (direction == "up")
            {
                bullet.Top -= speed;
                bullet.Image = Properties.Resources.bulletup;
            }
                

            if(direction == "down")
            {
                bullet.Top += speed;
                bullet.Image = Properties.Resources.bulletdown;
            }
                

            if(bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
            {
                bulletTimer.Stop();
                bulletTimer.Dispose();
                bullet.Dispose();
                bulletTimer = null;
                bullet = null;
            }
        }
    }
}
