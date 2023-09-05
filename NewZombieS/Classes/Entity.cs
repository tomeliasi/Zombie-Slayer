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

        protected Size clientSize = Globals.clientSize;
        public abstract void move();
    }
}
