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

        public void makeAmmo()
        {
            Tag = "ammo";
            Image = Properties.Resources.ammunition;
            Left = randNum.Next(0, mainForm.ClientSize.Width - Width);
            Top = randNum.Next(0, mainForm.ClientSize.Height - Height);
            Size = new Size(70, 60);
            SizeMode = PictureBoxSizeMode.StretchImage;
            mainForm.Controls.Add(this);
            BringToFront();
            playerInstance.BringToFront();
            playerInstance.setIsAmmoVisible(true);
        }

    }
}