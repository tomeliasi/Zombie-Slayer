using System;
using System.Drawing;

namespace Zombie_Slayer
{
    abstract public class ZombieAbstract : Entity
    {

        protected Player playerInstance = Globals.player;
        protected Random randNum = new Random();
        protected int demmage;

        public abstract void initZombie(Point initialPosition);
        public abstract void getDamaged(int damage);

    }
}
