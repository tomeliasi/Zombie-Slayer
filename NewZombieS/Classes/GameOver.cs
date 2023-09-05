using Zombie_Slayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using Zombie_Slayer;

namespace Zombie_Slayer
{
    public class GameOver : PictureBox
    {
        private Action restartGameHandler;
        public GameOver(Action restartGameHandler)
        {
            this.restartGameHandler = restartGameHandler;
        }
        public void makeGameOver()
        {
            /////////////////game over image button/////////////////

            PictureBox gameOver = new PictureBox();
            gameOver.Tag = "gameOver";
            gameOver.Image = Properties.Resources.game_over;
            gameOver.SizeMode = PictureBoxSizeMode.StretchImage;
            gameOver.Size = new Size(300, 300);
            gameOver.BackColor = Color.Transparent;
            gameOver.Location = new Point((Globals.clientSize.Width - gameOver.Width) / 2, (Globals.clientSize.Height - gameOver.Height) / 2 - 50);
            Globals.addElementToForm.Invoke(gameOver);
            gameOver.BringToFront();


            /////////////////reset button/////////////////

            PictureBox reset = new PictureBox();
            reset.Tag = "reset";
            reset.Image = Properties.Resources.reset;
            reset.SizeMode = PictureBoxSizeMode.StretchImage;
            reset.Size = new Size(100, 100);
            reset.Location = new Point((Globals.clientSize.Width - gameOver.Width) / 2 + 100, (Globals.clientSize.Height - gameOver.Height) / 2 + 250);
            Globals.addElementToForm.Invoke(reset);
            reset.BackColor = Color.Transparent;
            reset.BringToFront();
            reset.Click += handleClickOnReset;

        }

        private void handleClickOnReset(object sender, EventArgs e)
        {
            restartGameHandler?.Invoke();
        }
    }
}
