using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public abstract class Entity : PictureBox
    {
        protected bool up, down, right, left;
        protected string facing = string.Empty;
        protected int speed;
        protected int health;

        public abstract void move(Size clientSize);
    }
}
