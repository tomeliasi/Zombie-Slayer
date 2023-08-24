namespace Zombie_Slayer
{
    public class HealthKit : PictureBox
    {
        private Form mainForm;
        private Player playerInstance;
        private Random randNum = new Random();
        public HealthKit(Player player, Form form)
        {
            playerInstance = player;
            mainForm = form;
        }

        public void makeHealthKit()
        {
            playerInstance.setIsHealthkitVisable(true);
            Tag = "healthkit";
            Image = Properties.Resources.first_aid_kit;
            Left = randNum.Next(0, mainForm.ClientSize.Width - Width);
            Top = randNum.Next(0, mainForm.ClientSize.Height - Height);
            Size = new Size(70, 60);
            SizeMode = PictureBoxSizeMode.StretchImage;
            mainForm.Controls.Add(this);
            BringToFront();
            playerInstance.BringToFront();
        }
    }
}
