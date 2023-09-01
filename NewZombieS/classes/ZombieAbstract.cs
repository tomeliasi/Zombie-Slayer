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
