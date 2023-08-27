using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NewZombieS
{
    public class Player : Entity
    {
        public int ammo = 0;
        public bool isAmmoVisible = false;
        public bool isHealthkitVisable = false;
        public int score = 0;

        public int getAmmo() { return ammo; }
        public int getScore() { return score; }
        public bool getIsAmmoVisible() { return isAmmoVisible; }
        public int getHealth() { return health; }
        public int getSpeed() { return speed; }
        public void setHeath(int health) { this.health += health; }
        public void setScore(int score) { this.score = score; }
        public void setAmmo(int ammo) { this.ammo = ammo; }
        public void setIsAmmoVisible(bool value) { isAmmoVisible = value; }
        public Player()
        {
            Tag = "player";
            Size = new Size(100, 100);
            SizeMode = PictureBoxSizeMode.StretchImage;
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

        public void playerKeyIsUp(KeyEventArgs e)
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
            /*
            if (ammo < 1 && !isAmmoVisable)
            {
                MakeAmmo();
            }
            if (playerHealth < 30 && !isHealthkitVisable)
            {
                MakeHealthKit();
            }
            */
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
