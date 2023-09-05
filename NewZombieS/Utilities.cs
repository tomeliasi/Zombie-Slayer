using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public static class Utilities
    {
        public static void MakeZombie(Player player, Form form, List<ZombieAbstract> zombiesList, List<BigZombie> bigZombiesList)
        {
            zombiesList.Clear();

            int screenWidth = form.ClientSize.Width;
            int screenHeight = form.ClientSize.Height;

            Random randNum = new Random();
            int spawnArea = randNum.Next(1, 5);

            int x = 0, y = 0;

            switch (spawnArea)
            {
                case 1: // Top-left corner
                    x = randNum.Next(0, screenWidth / 2);
                    y = randNum.Next(0, screenHeight / 2);
                    break;
                case 2: // Top-right corner
                    x = randNum.Next(screenWidth / 2, screenWidth);
                    y = randNum.Next(0, screenHeight / 2);
                    break;
                case 3: // Bottom-left corner
                    x = randNum.Next(0, screenWidth / 2);
                    y = randNum.Next(screenHeight / 2, screenHeight);
                    break;
                case 4: // Bottom-right corner
                    x = randNum.Next(screenWidth / 2, screenWidth);
                    y = randNum.Next(screenHeight / 2, screenHeight);
                    break;
            }

            ZombieAbstract zombie = new Zombie(new Point(x, y));
            ZombieAbstract zombieBig = new BigZombie(new Point(x, y));

            zombiesList.Add(zombie);
            zombiesList.Add(zombieBig);

            int randomIndex = randNum.Next(0, zombiesList.Count);
            form.Controls.Add(zombiesList[randomIndex]);
        }
    }
}
