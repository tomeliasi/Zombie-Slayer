using System;
using System.Windows.Forms;
using System.Drawing;


namespace Zombie_Slayer
{
    public class Ammo : PictureBox
    {
        private Random randNum = new Random();

        public void MakeAmmo()
        {
            Tag = "ammo";
            Image = Properties.Resources.ammunition;
            Left = randNum.Next(0, Globals.clientSize.Width - Width);
            Top = randNum.Next(50, Globals.clientSize.Height - Height);
            Size = new Size(Constants.AmmoSizeWidth, Constants.AmmoSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Globals.form.Controls.Add(this);
            BringToFront();
            Globals.player.setIsAmmoVisible(true);
            BackColor = Color.Transparent;
        }
    }
}
