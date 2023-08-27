using Zombie_Slayer;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Zombie_Slayer
{
    public class Player : Entity
    {
        private int ammo = 0;
        private bool isAmmoVisible = false;
        private bool isHealthkitVisable = false;
        private int score = 0;

        public int getAmmo() { return ammo; }
        public int getScore() { return score; }
        public bool getIsAmmoVisible() { return isAmmoVisible; }
        public bool getIsHealthkitVisable() { return isHealthkitVisable; }
        public int getHealth() { return health; }
        public int getSpeed() { return speed; }
        public void setHeath(int health) { this.health += health; }

        public void setMaxHealth() { this.health = 100; }
        public void setScore(int score) { this.score += score; }
        public void setAmmo(int ammo) { this.ammo += ammo; }
        public void setIsAmmoVisible(bool value) { isAmmoVisible = value; }
        public void setIsHealthkitVisable(bool value) { isHealthkitVisable = value; }

        public Player()
        {
            Tag = "player";
            Size = new Size(100, 100);
            SizeMode = PictureBoxSizeMode.StretchImage;
            initPlayer();
        }

        public void initPlayer()
        {
            Image = Properties.Resources.hero_up;
            Location = new Point(300, 300);
            up = false;
            down = false;
            left = false;
            right = false;
            isAmmoVisible = false;
            facing = "up";
            health = 100;
            speed = 20;
            ammo = 10;
            score = 0;
        }
        public override void move(Size clientSize)
        {
            if (left && Left > 4)
                Left -= speed;

            if (right && Left + Width < clientSize.Width)
                Left += speed;

            if (up && Top > 65)
                Top -= speed;

            if (down && Top + Height < clientSize.Height)
                Top += speed;
        }

        public void playerKeyIsDown(KeyEventArgs e, bool gameOver)
        {
            if (gameOver == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    left = true;
                    facing = "left";
                    Image = Properties.Resources.hero_left;
                }

                if (e.KeyCode == Keys.Right)
                {
                    right = true;
                    facing = "right";
                    Image = Properties.Resources.hero_right;

                }

                if (e.KeyCode == Keys.Up)
                {
                    up = true;
                    facing = "up";
                    Image = Properties.Resources.hero_up;

                }

                if (e.KeyCode == Keys.Down)
                {
                    down = true;
                    facing = "down";
                    Image = Properties.Resources.hero__down;

                }
            }
        }

        public void playerKeyIsUp(KeyEventArgs e, Ammo newAmmo, HealthKit healthKit)
        {
            if (e.KeyCode == Keys.Left)
                left = false;

            if (e.KeyCode == Keys.Right)
                right = false;

            if (e.KeyCode == Keys.Up)
                up = false;

            if (e.KeyCode == Keys.Down)
                down = false;

            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                Shoot();
            }

            if (ammo < 1 && !this.isAmmoVisible)
            {
                newAmmo.MakeAmmo();
            }


            if (health < 30 && !isHealthkitVisable)
            {
                healthKit.makeHealthKit();
            }


        }
        public void Shoot()
        {
            if (ammo > 0)
            {
                ammo--;
                ShootBullet(facing);
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = Left + (Width / 2) - 12;
            shootBullet.bulletTop = Top + (Height / 2) - 10;
            shootBullet.makeBullet(this.FindForm());
        }
    }

}
