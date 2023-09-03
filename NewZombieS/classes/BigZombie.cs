using System;
using System.Drawing;
using System.Windows.Forms;
namespace Zombie_Slayer
{
    public class BigZombie : ZombieAbstract
    {
        public new Random randNum = new Random();
        private new readonly Player playerInstance;
        private int width = 150;
        private int height = 150;

        public int getDemmage() { return demmage; }
        public int getSpeed() { return speed; }
        public int getHealth() { return health; }
        public int getWidth() { return width; }

        public BigZombie(Player player, Size clientSize)
        {
            playerInstance = player;

            Tag = "bigZombie";

            Size = new Size(width, height);
            SizeMode = PictureBoxSizeMode.StretchImage;

            speed = Constants.BigZombieSpeed;
            initZombie();
            demmage = Constants.BigZombieDammage;

            BackColor = Color.Transparent;

        }
        public void initZombie(Size clientSize)
        {
            Left = randNum.Next(0, clientSize.Width - Width);
            Top = randNum.Next(0, clientSize.Height - Height);
            Image = Properties.Resources.bigZombieDown;
            health = 2;
            speed = 1;
        }


        public override void move(Size ClientSize)
        {
            if (playerInstance != null)
            {
                if (Left > playerInstance.Left)
                {
                    Left -= speed;
                    Image = Properties.Resources.bigZombieLeft;
                }

                if (Left < playerInstance.Left)
                {
                    Left += speed;
                    Image = Properties.Resources.bigZombieRight;
                }

                if (Top > playerInstance.Top)
                {
                    Top -= speed;
                    Image = Properties.Resources.bigZombieUp;
                }

                if (Top < playerInstance.Top)
                {
                    Top += speed;
                    Image = Properties.Resources.bigZombieDown;
                }
            }
        }

        public void getDamaged(int damage)
        {
            health -= damage;

            speed+=2;
            health--;
            demmage--;

            Width -= 25;
            Height -= 25;
        }
    }
}

