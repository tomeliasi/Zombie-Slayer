using System;
using System.Drawing;

namespace Zombie_Slayer
{
    abstract public class ZombieAbstract : Entity
    {

        protected Player playerInstance = Globals.player;
        protected Random randNum = new Random();
        protected Size clientSize = Globals.clientSize;
        protected int demmage;


        public void initZombie()
        {
            Left = randNum.Next(0, 600);
            Top = randNum.Next(0, 600);
            up = false;
            down = false;
            left = false;
            right = false;
            facing = "down";
        }
    }
}
