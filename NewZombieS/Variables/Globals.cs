using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public class Globals
    {
        public static Form1 form;

        public static Player player;
        public static List<ZombieAbstract> zombiesList;
        public static List<BigZombie> bigZombiesList;

        public static Size clientSize;

        public static Ammo ammo;
        public static HealthKit healthKit;

        public static Action<Control> addElementToForm;
        public static Action<Control> removeElementToForm;
    }

}
