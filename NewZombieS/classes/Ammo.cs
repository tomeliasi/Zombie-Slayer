using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Zombie_Slayer;

namespace Zombie_Slayer
{
    public class Ammo : PictureBox
    {
        private Form mainForm;
        private Player playerInstance;
        private Random randNum = new Random();
        public Ammo(Player player, Form form)
        {
            playerInstance = player;
            mainForm = form;
        }

        public void MakeAmmo()
        {
            Tag = "ammo";
            Image = Properties.Resources.ammunition;
            Left = randNum.Next(0, mainForm.ClientSize.Width - Width);
            Top = randNum.Next(0, mainForm.ClientSize.Height - Height);
            Size = new Size(Constants.AmmoSizeWidth, Constants.AmmoSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            mainForm.Controls.Add(this);
            BringToFront();
            playerInstance.setIsAmmoVisible(true);
        }

    }
}
