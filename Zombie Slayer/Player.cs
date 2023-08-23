using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Slayer
{
    public class Player : Entity
    {
        public override void Move()
        {
            if (left && Left > 4)
                Left -= speed;
            if (right && Left + Width < this.ClientSize.Width)
                Left += speed;

            if (up && Top > 65)
                Top -= speed;
            if (down && Top + Height < this.ClientSize.Height)
                Top += speed;

        }
    }

}
