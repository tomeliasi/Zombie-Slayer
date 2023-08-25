using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public partial class Form1 : Form
    {
        private Player player = new Player();
        private Ammo ammo;
        private HealthKit healthKit;
        private List<Zombie> zombiesList = new List<Zombie>();

        bool gameOver;
        Random randNum = new Random();

        public Form1()
        {
            InitializeComponent();
            restartGame();

            ammo = new Ammo(player, this);
            healthKit = new HealthKit(player, this);
        }

        private void mainTimerEvent(object sender, EventArgs e)
        {
            if (player.getHealth() > 1)
            {
                healthBar.Value = player.getHealth();
            }
            else
            {
                gameOver = true;
                handleGameOver();
            }

            ammoCount.Text = "Ammo: " + player.getAmmo();
            kills.Text = "kills: " + player.getScore();


            player.move(ClientSize);

            collisions();
        }

        private void collisions()
        {

            foreach (Control entity in this.Controls)
            {
                if (entity is Ammo)
                {
                    Ammo ammoEntity = (Ammo)entity;

                    if (player.Bounds.IntersectsWith(ammoEntity.Bounds))
                    {
                        this.Controls.Remove(ammoEntity);
                        ammoEntity.Dispose();
                        player.setAmmo(10);
                        player.setIsAmmoVisible(false);
                    }
                }
                if (entity is HealthKit)
                {
                    HealthKit halthKitEntity = (HealthKit)entity;
                    if (player.Bounds.IntersectsWith(halthKitEntity.Bounds))
                    {
                        this.Controls.Remove(halthKitEntity);
                        halthKitEntity.Dispose();
                        if (player.getHealth() <= 70)
                            player.setHeath(30);
                        else
                            player.setMaxHealth();
                        player.setIsHealthkitVisable(false);
                    }
                }
                if (entity is Zombie)
                {
                    Zombie zombieEntity = (Zombie)entity;
                    zombieEntity.move(ClientSize);

                    if (player.Bounds.IntersectsWith(entity.Bounds))
                        player.setHeath(-2);

                }
                foreach (Control entity2 in this.Controls)
                {
                    if (entity2 is PictureBox && (string)entity2.Tag == "bullet" && entity is Zombie)
                    {
                        Zombie zombieEntity = (Zombie)entity;

                        if (zombieEntity.Bounds.IntersectsWith(entity2.Bounds))
                        {
                            player.setScore(1);
                            this.Controls.Remove(entity2);
                            ((PictureBox)entity2).Dispose();
                            this.Controls.Remove(zombieEntity);
                            zombieEntity.Dispose();
                            zombiesList.Remove(zombieEntity);
                            makeZombie();
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
            player.playerKeyIsUp(e, ammo, healthKit);
        }

        private void makeZombie()
        {
            Zombie zombie = new Zombie(player, this.ClientSize);
            zombiesList.Add(zombie);
            this.Controls.Add(zombie);
        }

        private void restartGame()
        {
            this.Controls.Add(player);
            gameOver = false;

            GameTimer.Start();

            player.Image = Properties.Resources.hero_up;

            removeObjectByTag("gameOver");
            removeObjectByTag("reset");
            removeObjectByTag("healthkit");
            removeObjectByTag("ammo");


            foreach (PictureBox zombie in zombiesList)
            {
                this.Controls.Remove(zombie);
            }
            zombiesList.Clear();

            for (int i = 0; i < 3; i++)
                makeZombie();
            player.initPlayer();

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

            PictureBox gameOver = new PictureBox();
            gameOver.Tag = "gameOver";
            gameOver.Image = Properties.Resources.game_over;
            gameOver.SizeMode = PictureBoxSizeMode.StretchImage;
            gameOver.Size = new Size(300, 300);
            gameOver.Location = new Point((this.Width - gameOver.Width) / 2, (this.Height - gameOver.Height) / 2 - 50);
            this.Controls.Add(gameOver);
            gameOver.BringToFront();


            /////////////////reset button/////////////////

            PictureBox reset = new PictureBox();
            reset.Tag = "reset";
            reset.Image = Properties.Resources.reset;
            reset.SizeMode = PictureBoxSizeMode.StretchImage;
            reset.Size = new Size(100, 100);
            reset.Location = new Point((this.Width - gameOver.Width) / 2 + 100, (this.Height - gameOver.Height) / 2 + 250);
            this.Controls.Add(reset);
            reset.BringToFront();

            reset.Click += handleClickOnReset;
        }

        private void handleClickOnReset(object sender, EventArgs e)
        {
            restartGame();
        }


        private void removeObjectByTag(string tag)
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && ((string)control.Tag == tag))
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
