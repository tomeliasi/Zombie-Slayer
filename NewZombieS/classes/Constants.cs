using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zombie_Slayer
{
    public static class Constants
    {

        ///////////////////////////Form///////////////////////////

        public const int FormWidth = 1111;
        public const int FormHeight = 700;



        ///////////////////////////Player///////////////////////////

        // size
        public const int PlayerSizeWidth = 100;
        public const int PlayerSizeHeight = 100;

        // location
        public const int PlayerLocationWidth = 300;
        public const int PlayerLocationHeight = 300;

        public const int PlayerInitialHealth = 100;
        public const int PlayerInitialAmmo = 10;
        public const int PlayerInitialScore = 0;
        public const int PlayerSpeed = 20;




        ///////////////////////////Zombie///////////////////////////

        //size
        public const int ZombieSizeWidth = 100;
        public const int ZombieSizeHeight = 100;

        public const int ZombieInitialHealth = 1;
        public const int ZombieSpeed = 3;
        public const int ZombieDammage = 1;



        ///////////////////////////Bullet///////////////////////////

        // size
        public const int BulletSizeWidth = 20;
        public const int BulletSizeHeight = 20;



        ///////////////////////////Ammo///////////////////////////

        // size
        public const int AmmoSizeWidth = 70;
        public const int AmmoSizeHeight = 60;



        ///////////////////////////HealthKit///////////////////////////

        // size
        public const int HealthKitSizeWidth = 70;
        public const int HealthKitSizeHeight = 60;


    }
}
