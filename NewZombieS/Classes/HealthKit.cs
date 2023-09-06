using Zombie_Slayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using Zombie_Slayer;
using System.Media;
using System.IO;

namespace Zombie_Slayer
{
    public class HealthKit : PictureBox
    {
        private Random randNum = new Random();
        public void makeHealthKit()
        {
            Globals.player.setIsHealthkitVisable(true);
            Tag = "healthkit";
            Image = Properties.Resources.first_aid_kit;
            Left = randNum.Next(0, Globals.clientSize.Width - Width);
            Top = randNum.Next(50, Globals.clientSize.Height - Height);
            Size = new Size(Constants.HealthKitSizeWidth, Constants.HealthKitSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Globals.form.Controls.Add(this);
            BringToFront();
            Globals.player.BringToFront();
            BackColor = Color.Transparent;

        }
    }
}
