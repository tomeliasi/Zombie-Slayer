using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace Zombie_Slayer
{
    public partial class Form1 : Form
    {
        private GameState gameState = new GameState();
        private bool gameOver;
        private SoundPlayer gameOverSound;
        private SoundPlayer MainSound;
        private bool isPause = false;

        private CollisionHandler collisionHandler;
        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeGame();
        }

        private void InitializeGame()

        {
            Globals.player = new Player();
            Globals.zombiesList = new List<ZombieAbstract>();
            Globals.bigZombiesList = new List<BigZombie>();

            Globals.player = new Player();
            Globals.ammo = new Ammo(Globals.player, this);
            Globals.healthKit = new HealthKit(Globals.player, this);

            Globals.clientSize = ClientSize;
            // Load sounds
            gameOverSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "GameOverSound.wav"));
            MainSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "MainSound.wav"));
            collisionHandler = new CollisionHandler(Globals.player, this, Globals.zombiesList, Globals.bigZombiesList);

            restartGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(Globals.player.Image, Globals.player.Location);
        }

        private void mainTimerEvent(object sender, EventArgs e)
        {
            if (Globals.player.getHealth() > 1)
            {
                if (Globals.player.getHealth() > 100)
                {
                    Globals.player.setHealth(-100);
                }
                healthBar.Value = Globals.player.getHealth();
                this.Invalidate(healthBar.Bounds);

            }
            else
            {
                gameOver = true;
                handleGameOver();
                gameOverSound.Play();
            }
            ammoCount.Text = "Ammo: " + Globals.player.getAmmo();
            Kills.Text = "kills: " + Globals.player.getScore();

            Globals.player.move(ClientSize);
            this.Invalidate(Globals.player.Bounds);
            collisionHandler.HandleCollisions();

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            Globals.player.playerKeyIsDown(e, gameOver);
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            Globals.player.playerKeyIsUp(e, Globals.ammo, Globals.healthKit);
        }


        private void restartGame()
        {
            string[] tagsToRemove = { "gameOver", "reset", "ammoCount", "gameOver", "healthkit", "ammo", "bigZombie", "zombie" };

            this.Controls.Add(Globals.player);
            gameOver = false;
            isPause = true;

            GameTimer.Start();

            Globals.player.Image = Properties.Resources.hero_up;

            foreach (string tag in tagsToRemove)
            {
                removeObjectByTag(tag);
            }

            Globals.zombiesList.Clear();

            for (int i = 0; i < 2; i++)
                Utilities.MakeZombie(Globals.player, this, Globals.zombiesList, Globals.bigZombiesList);

            Globals.player.initPlayer();
            MainSound.Play();
            Globals.player.setIsAmmoVisible(false);
            Globals.player.setIsHealthkitVisable(false);
        }

        private void handleGameOver()
        {
            Globals.player.Image = Properties.Resources.skull;
            Globals.player.Size = new Size(99, 100);
            GameTimer.Stop();
            MainSound.Dispose();
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
            gameOver.BackColor = Color.Transparent;
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
            reset.BackColor = Color.Transparent;
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


        private void pause_Click(object sender, EventArgs e)
        {
            if (isPause)
            {
                GameTimer.Stop();
                MainSound.Stop();
                pause.Image = Properties.Resources.PlayGame;
                isPause = !isPause;
                load.Visible = true;
                save.Visible = true;
            }
            else
            {
                MainSound.Play();
                GameTimer.Start();
                pause.Image = Properties.Resources.PauseGame;
                isPause = !isPause;
                load.Visible = false;
                save.Visible = false;
            }
        }

        private void SaveGameState()
        {
            try
            {
                using (FileStream fs = new FileStream("gamestate.dat", FileMode.Create))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    gameState.PlayerHealth = Globals.player.getHealth();
                    gameState.PlayerScore = Globals.player.getScore();
                    gameState.PlayerAmmo = Globals.player.getAmmo();
                    formatter.Serialize(fs, gameState);
                }
                MessageBox.Show("Game saved successfully.", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving game: " + ex.Message, "Save Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadGameState()
        {
            try
            {
                if (File.Exists("gamestate.dat"))
                {
                    using (FileStream fs = new FileStream("gamestate.dat", FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        gameState = (GameState)formatter.Deserialize(fs);
                        Globals.player.setHealth(-Globals.player.getHealth());
                        Globals.player.setAmmo(-Globals.player.getAmmo());
                        Globals.player.setScore(-Globals.player.getScore());
                        Globals.player.setHealth(gameState.PlayerHealth);
                        Globals.player.setScore(gameState.PlayerScore);
                        Globals.player.setAmmo(gameState.PlayerAmmo);
                        Globals.player.setIsAmmoVisible(false);
                        Globals.player.setIsHealthkitVisable(false);

                    }
                    MessageBox.Show("Game loaded successfully.", "Load Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No saved game found.", "Load Game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading game : " + ex.Message, "Load Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            SaveGameState();
        }

        private void load_Click(object sender, EventArgs e)
        {
            LoadGameState();
        }

    }
}
