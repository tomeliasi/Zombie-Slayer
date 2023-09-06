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
            int spawnEdge = randNum.Next(1, 5);

            int x = 0, y = 0;

            switch (spawnEdge)
            {
                case 1: // Top edge
                    x = randNum.Next(0, screenWidth);
                    y = 0;
                    break;
                case 2: // Right edge
                    x = screenWidth;
                    y = randNum.Next(0, screenHeight);
                    break;
                case 3: // Bottom edge
                    x = randNum.Next(0, screenWidth);
                    y = screenHeight;
                    break;
                case 4: // Left edge
                    x = 0;
                    y = randNum.Next(0, screenHeight);
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
