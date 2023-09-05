using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public static class Utilities
    {
        public static void MakeZombie()
        {
            Globals.zombiesList.Clear();

            int screenWidth = Globals.clientSize.Width;
            int screenHeight = Globals.clientSize.Height;

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

            Globals.zombiesList.Add(zombie);
            Globals.zombiesList.Add(zombieBig);

            int randomIndex = randNum.Next(0, Globals.zombiesList.Count);
            Globals.addElementToForm.Invoke(Globals.zombiesList[randomIndex]);
        }

        public static void removeObjectByTag(string tag)
        {
            foreach (Control control in Globals.form.Controls)
            {
                if (control is PictureBox && ((string)control.Tag == tag))
                {
                    Globals.removeElementToForm.Invoke(control);
                    control.Dispose();
                }
            }
        }
    }
}
