using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    internal class Dragon : MonsterAbstract
    {
        public new Random randNum = new Random();
        private new readonly Player playerInstance;
        private int width = Constants.dragonSizeWidth;
        private int height = Constants.dragonSizeHeight;
        public HealthBar healthBar { get; private set; }

        public int getDemmage() { return demmage; }
        public int getSpeed() { return speed; }
        public int getHealth() { return health; }
        public int getWidth() { return width; }

        public Dragon(Player player, Size clientSize)
        {
            playerInstance = player;

            Tag = "dragon";

            Size = new Size(width, height);
            SizeMode = PictureBoxSizeMode.StretchImage;

            speed = Constants.dragonSpeed;
            demmage = Constants.dragonDammage;

            health = Constants.dragonHealth;
            BackColor = Color.Transparent;

            healthBar = new HealthBar(Constants.dragonHealth);
            healthBar.Tag = "healthBar"; 
            healthBar.Size = new Size(Constants.dragonHealth * 2, 10);
            healthBar.BackColor = Color.Red; 
            healthBar.UpdateHealth(health);
            healthBar.Visible = false; 

        }


        public override void move(Size ClientSize)
        {
            if (playerInstance != null)
            {
                if (Left > playerInstance.Left)
                {
                    Left -= speed;
                    Image = Properties.Resources.dragonLeft;
                }

                if (Left < playerInstance.Left)
                {
                    Left += speed;
                    Image = Properties.Resources.dragonRight;
                }

                if (Top > playerInstance.Top)
                {
                    Top -= speed;
                    Image = Properties.Resources.dragonUp;
                }

                if (Top < playerInstance.Top)
                {
                    Top += speed;
                    Image = Properties.Resources.dragonDown;
                }
            }

        }

        public void getDamaged(int damage)
        {
            health -= damage;
            healthBar.UpdateHealth(health); // Update the health bar when the dragon takes damage
        }

    }
}
    

