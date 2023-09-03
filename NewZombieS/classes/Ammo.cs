using System;
using System.Windows.Forms;
using System.Drawing;


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
