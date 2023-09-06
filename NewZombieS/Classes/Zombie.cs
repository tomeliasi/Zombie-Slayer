using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public class Zombie : ZombieAbstract
    {
        public Random randNum = new Random();

        public Zombie(Point initialPosition)
        {
            speed = Constants.ZombieSpeed;
            Tag = "zombie";
            Size = new Size(Constants.ZombieSizeWidth, Constants.ZombieSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;

            initZombie(initialPosition);
        }

        public int getHealth() { return health; }
        public override void initZombie(Point initialPosition)
        {
            if (initialPosition == Point.Empty)
            {
                Left = randNum.Next(0, clientSize.Width - Width);
                Top = randNum.Next(0, clientSize.Height - Height);
            }
            else
            {
                Left = initialPosition.X;
                Top = initialPosition.Y;
            }
            Image = Properties.Resources.zombieGDown;
            health = Constants.ZombieInitialHealth;
            BackColor = Color.Transparent;
        }

        public override void move()
        {
            if (playerInstance != null)
            {
                if (Left > playerInstance.Left)
                {
                    Left -= speed;
                    Image = Properties.Resources.zombieGLeft;
                }

                if (Left < playerInstance.Left)
                {
                    Left += speed;
                    Image = Properties.Resources.zombieGRight;
                }

                if (Top > playerInstance.Top)
                {
                    Top -= speed;
                    Image = Properties.Resources.zombieGUp;
                }

                if (Top < playerInstance.Top)
                {
                    Top += speed;
                    Image = Properties.Resources.zombieGDown;
                }
            }
        }

        public override void getDamaged(int damage)
        {
            health -= damage;
        }

    }
}
