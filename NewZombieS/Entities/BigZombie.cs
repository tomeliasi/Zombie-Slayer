using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public class BigZombie : ZombieAbstract
    {
        public int getDemmage() { return demmage; }
        public int getSpeed() { return speed; }
        public int getHealth() { return health; }
        public int getWidth() { return Width; } // Use the inherited Width property

        public BigZombie(Point initialPosition)
        {
            Tag = "bigZombie";
            Size = new Size(Constants.BigZombieSizeWidth, Constants.BigZombieSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;

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

            Image = Properties.Resources.bigZombieDown;
            health = Constants.BigZombieHealth;
            speed = Constants.BigZombieSpeed;
            demmage = Constants.BigZombieDammage;
            BackColor = Color.Transparent;
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
            speed += 2;
            health--;
            demmage--;

            Width -= 25;
            Height -= 25;
        }
    }
}
