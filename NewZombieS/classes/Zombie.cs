using Zombie_Slayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using Zombie_Slayer;

namespace Zombie_Slayer
{
    public class Zombie : ZombieAbstract
    {
        public Random randNum = new Random();
        
        public Zombie(Player player, Size clientSize)
        {
            playerInstance = player;

            Tag = "zombie";
            Size = new Size(Constants.ZombieSizeWidth, Constants.ZombieSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            initZombie();
        }
        public void initZombie(Size clientSize)
        {
            Image = Properties.Resources.zombieGDown;
            health = Constants.ZombieInitialHealth;
            speed = Constants.ZombieSpeed;
        }


        public override void move(Size ClientSize)
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
    }
}
