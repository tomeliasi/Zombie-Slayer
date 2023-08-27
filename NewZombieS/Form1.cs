using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewZombieS
{
    public partial class Form1 : Form
    {
        private Player player = new Player();
        bool up, down, right, left, gameOver;
        string facing = "up";
        int playerHealth = 100;
        bool isAmmoVisable = false;
        bool isHealthkitVisable = false;
        int ammo = 10;
        int zombieSpeed = 3;
        Random randNum = new Random();
        int score = 0;

        List<PictureBox> zombiesList = new List<PictureBox>();

        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }


        private void mainTimerEvent(object sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                healthBar.Value = playerHealth;
            }
            else
            {
                gameOver = true;
                handleGameOver();
            }


            ammoCount.Text = "Ammo: " + player.ammo;
            Kills.Text = "Kills: " + player.score;


            player.move(ClientSize);

            foreach (Control entity in this.Controls)
            {
                if (entity is PictureBox && (string)entity.Tag == "ammo")
                {
                    if (player.Bounds.IntersectsWith(entity.Bounds))
                    {
                        this.Controls.Remove(entity);
                        entity.Dispose();
                        ammo += 10;
                        isAmmoVisable = false;
                    }
                }
                if (entity is PictureBox && (string)entity.Tag == "healthkit")
                {
                    if (player.Bounds.IntersectsWith(entity.Bounds))
                    {
                        this.Controls.Remove(entity);
                        entity.Dispose();
                        playerHealth += 30;
                        isHealthkitVisable = false;
                    }
                }
                if (entity is PictureBox && (string)entity.Tag == "zombie")
                {
                    if (player.Bounds.IntersectsWith(entity.Bounds))
                        playerHealth -= 2;

                    if (entity.Left > player.Left)
                    {
                        entity.Left -= zombieSpeed;
                        ((PictureBox)entity).Image = Properties.Resources.zombieGLeft;
                    }

                    if (entity.Left < player.Left)
                    {
                        entity.Left += zombieSpeed;
                        ((PictureBox)entity).Image = Properties.Resources.zombieGRight;
                    }
                    if (entity.Top > player.Top)
                    {
                        entity.Top -= zombieSpeed;
                        ((PictureBox)entity).Image = Properties.Resources.zombieGUp;
                    }
                    if (entity.Top < player.Top)
                    {
                        entity.Top += zombieSpeed;
                        ((PictureBox)entity).Image = Properties.Resources.zombieGDown;
                    }
                }
                foreach (Control entity2 in this.Controls)
                {
                    if (entity2 is PictureBox && (string)entity2.Tag == "bullet" && entity is PictureBox && (string)entity.Tag == "zombie")
                    {
                        if (entity.Bounds.IntersectsWith(entity2.Bounds))
                        {
                            player.score++;
                            this.Controls.Remove(entity2);
                            ((PictureBox)entity2).Dispose();
                            this.Controls.Remove(entity);
                            ((PictureBox)entity).Dispose();
                            zombiesList.Remove((PictureBox)entity);
                            MakeZombies();
                        }
                    }
                }
            }

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine(sender);
            player.playerKeyIsDown(e, gameOver);
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            player.playerKeyIsUp(e);
        }

        private void MakeZombies()
        {
            PictureBox zombie = new PictureBox();
            zombie.Tag = "zombie";
            zombie.Image = Properties.Resources.zombieGDown;
            zombie.Left = randNum.Next(0, this.ClientSize.Width - zombie.Width);
            zombie.Top = randNum.Next(0, this.ClientSize.Height - zombie.Height);
            zombie.Size = new Size(100, 100);
            zombie.SizeMode = PictureBoxSizeMode.StretchImage;
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
            player.BringToFront();
        }

        private void MakeHealthKit()
        {
            PictureBox Healthkit = new PictureBox();
            Healthkit.Tag = "healthkit";
            Healthkit.Image = Properties.Resources.first_aid_kit;
            Healthkit.Left = randNum.Next(0, this.ClientSize.Width - Healthkit.Width);
            Healthkit.Top = randNum.Next(0, this.ClientSize.Height - Healthkit.Height);
            Healthkit.Size = new Size(70, 60);
            Healthkit.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(Healthkit);
            Healthkit.BringToFront();
            player.BringToFront();
            isHealthkitVisable = true;
        }

        private void RestartGame()
        {
            this.Controls.Add(player);
            gameOver = false;

            player.Image = Properties.Resources.hero_up;

            foreach (PictureBox zombie in zombiesList)
            {
                this.Controls.Remove(zombie);
            }
            zombiesList.Clear();

            for (int i = 0; i < 3; i++)
                MakeZombies();
            up = false;
            down = false;
            left = false;
            right = false;
            playerHealth = 100;
            score = 0;
            ammo = 10;
            GameTimer.Start();

        }

        private void healthBar_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Kills_TextChanged(object sender, EventArgs e)
        {

        }

        private void Kills_Click_1(object sender, EventArgs e)
        {

        }

        private void handleGameOver()
        {
            player.Image = Properties.Resources.skull;
            player.Size = new Size(99, 100);
            GameTimer.Stop();

            renderGameoverSection();
        }

        private void renderGameoverSection()
        {

            /////////////////game over image button/////////////////

            PictureBox Gameover = new PictureBox();
            Gameover.Image = Properties.Resources.game_over;
            Gameover.SizeMode = PictureBoxSizeMode.StretchImage;
            Gameover.Size = new Size(300, 300);
            Gameover.Location = new Point((this.Width - Gameover.Width) / 2, (this.Height - Gameover.Height) / 2);
            this.Controls.Add(Gameover);
            Gameover.BringToFront();


            /////////////////reset button/////////////////

            PictureBox Reset = new PictureBox();
            Reset.Image = Properties.Resources.reset;
            Reset.SizeMode = PictureBoxSizeMode.StretchImage;
            Reset.Size = new Size(100, 100);
            Reset.Location = new Point((this.Width - Gameover.Width) / 2 + 100, (this.Height - Gameover.Height) / 2 + 300);
            this.Controls.Add(Reset);
            Reset.BringToFront();
        }

        private void player_Click(object sender, EventArgs e)
        {

        }

        private void kills_Click(object sender, EventArgs e)
        {

        }
    }
}
