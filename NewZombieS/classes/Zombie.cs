﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public class Zombie : ZombieAbstract
    {
        public Random randNum = new Random();
        private Player playerInstance;

        public Zombie(Player player, Size clientSize) : this(player, clientSize, Point.Empty)
        {
        }

        public Zombie(Player player, Size clientSize, Point initialPosition)
        {
            playerInstance = player;
            speed = Constants.ZombieSpeed;
            Tag = "zombie";
            Size = new Size(Constants.ZombieSizeWidth, Constants.ZombieSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            InitZombie(clientSize, initialPosition);
        }

        private void InitZombie(Size clientSize, Point initialPosition)
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
