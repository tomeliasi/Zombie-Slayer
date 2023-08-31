using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    abstract public class ZombieAbstract : Entity
    {

        protected Player playerInstance;
        protected Random randNum = new Random();
        protected Size clientSize;

        protected Bitmap imageRight;
        protected Bitmap imageLeft;
        protected Bitmap imageUp;
        protected Bitmap imageDown;

        private Direction currentDirection = Direction.Left;

        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        public override void move(Size ClientSize)
        {
            if (playerInstance != null)
            {
                if (Left > playerInstance.Left)
                {
                    Left -= speed;
                    SetDirectionImage(Direction.Left);
                }

                if (Left < playerInstance.Left)
                {
                    Left += speed;
                    SetDirectionImage(Direction.Right);
                }

                if (Top > playerInstance.Top)
                {
                    Top -= speed;
                    SetDirectionImage(Direction.Up);
                }

                if (Top < playerInstance.Top)
                {
                    Top += speed;
                    SetDirectionImage(Direction.Down);
                }
            }

        }
        private void SetDirectionImage(Direction direction)
        {
            currentDirection = direction;

            switch (direction)
            {
                case Direction.Left:
                    Image = imageLeft;
                    break;
                case Direction.Right:
                    Image = imageRight;
                    break;
                case Direction.Up:
                    Image = imageUp;
                    break;
                case Direction.Down:
                    Image = imageDown;
                    break;
                default:
                    break;
            }
        }

        public void initZombie()
        {
            Left = randNum.Next(0, 900);
            Top = randNum.Next(0, 600);
            //Image = Properties.Resources.zombieGDown;
            up = false;
            down = false;
            left = false;
            right = false;
            facing = "down";
            //health = Constants.ZombieInitialHealth;
            //speed = Constants.ZombieSpeed;
        }
    }
}
