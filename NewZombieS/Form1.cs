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
        private Player player = new Player();
        private Ammo ammo;
        private HealthKit healthKit;
        private List<ZombieAbstract> zombiesList = new List<ZombieAbstract>();
        private List<BigZombie> bigZombiesList = new List<BigZombie>();
        private GameState gameState = new GameState();
        private Random randNum = new Random();
        private bool gameOver;
        private SoundPlayer gameOverSound;
        private SoundPlayer MainSound;
        private bool isPause = false;

        // Enums for object tags
        private enum ObjectTags { Player, Ammo, HealthKit, Zombie, BigZombie }

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            InitializeGame();
        }

        private void InitializeGame()
        {
            // Initialize player, ammo, healthKit, and other game elements
            player = new Player();
            ammo = new Ammo(player, this);
            healthKit = new HealthKit(player, this);

            // Load sounds
            gameOverSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "GameOverSound.wav"));
            MainSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "MainSound.wav"));

            restartGame();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(player.Image, player.Location);
        }

        private void mainTimerEvent(object sender, EventArgs e)
        {
            if (player.getHealth() > 1)
            {
                if (player.getHealth() > 100)
                {
                    player.setHealth(-100);
                }
                healthBar.Value = player.getHealth();
                this.Invalidate(healthBar.Bounds);

            }
            else
            {
                gameOver = true;
                handleGameOver();
                gameOverSound.Play();
            }
            ammoCount.Text = "Ammo: " + player.getAmmo();
            Kills.Text = "kills: " + player.getScore();

            player.move(ClientSize);
            this.Invalidate(player.Bounds);
            collisions();

        }

        private void collisions()
        {
            foreach (Control entity in this.Controls)
            {
                switch (entity)
                {
                    case Ammo ammoEntity when player.Bounds.IntersectsWith(ammoEntity.Bounds):
                        HandleAmmoCollision(ammoEntity);
                        break;

                    case HealthKit healthKitEntity when player.Bounds.IntersectsWith(healthKitEntity.Bounds):
                        HandleHealthKitCollision(healthKitEntity);
                        break;

                    case Zombie zombieEntity:
                        HandleZombieCollision(zombieEntity);
                        break;

                    case BigZombie bigZombieEntity:
                        HandleBigZombieCollision(bigZombieEntity);
                        break;

                    case PictureBox bulletEntity when (string)bulletEntity.Tag == "bullet":
                        foreach (Control enemyEntity in this.Controls)
                        {
                            switch (enemyEntity)
                            {
                                case Zombie zombieEnemy when zombieEnemy.Bounds.IntersectsWith(bulletEntity.Bounds):
                                    HandleZombieBulletCollision(zombieEnemy, bulletEntity);
                                    break;

                                case BigZombie bigZombieEnemy when bigZombieEnemy.Bounds.IntersectsWith(bulletEntity.Bounds):
                                    HandleBigZombieBulletCollision(bigZombieEnemy, bulletEntity);
                                    break;
                            }
                        }
                        break;
                }
            }
        }

        private void HandleAmmoCollision(Ammo ammoEntity)
        {
            this.Controls.Remove(ammoEntity);
            ammoEntity.Dispose();

            player.setAmmo(10);
            player.setIsAmmoVisible(false);
            ammo = null;
            ammo = new Ammo(player, this);
        }

        private void HandleHealthKitCollision(HealthKit healthKitEntity)
        {
            this.Controls.Remove(healthKitEntity);
            healthKitEntity.Dispose();

            if (player.getHealth() <= 70)
                player.setHealth(30);
            else
                player.setMaxHealth();

            player.setIsHealthkitVisable(false);
            healthKit = null;
            healthKit = new HealthKit(player, this);
        }

        private void HandleZombieCollision(Zombie zombieEntity)
        {
            zombieEntity.move(ClientSize);
            this.Invalidate(zombieEntity.Bounds);

            if (player.Bounds.IntersectsWith(zombieEntity.Bounds))
                player.setHealth(-Constants.ZombieDammage);
        }

        private void HandleBigZombieCollision(BigZombie bigZombieEntity)
        {
            bigZombieEntity.move(ClientSize);
            this.Invalidate(bigZombieEntity.Bounds);

            if (player.Bounds.IntersectsWith(bigZombieEntity.Bounds))
                player.setHealth(-bigZombieEntity.getDemmage());
        }

        private void HandleZombieBulletCollision(Zombie zombieEntity, PictureBox bulletEntity)
        {
            player.setScore(1);
            this.Controls.Remove(bulletEntity);
            bulletEntity.Dispose();
            this.Controls.Remove(zombieEntity);
            zombieEntity.Dispose();
            zombiesList.Remove(zombieEntity);
            makeZombie();
        }

        private void HandleBigZombieBulletCollision(BigZombie bigZombieEntity, PictureBox bulletEntity)
        {
            this.Controls.Remove(bulletEntity);
            bulletEntity.Dispose();

            bigZombieEntity.getDamaged(1);

            if (bigZombieEntity.Width < 100)
            {
                player.setScore(1);
                bigZombiesList.Remove(bigZombieEntity);
                bigZombieEntity.Dispose();
                makeZombie();
            }
        }



        private void keyIsDown(object sender, KeyEventArgs e)
        {
            player.playerKeyIsDown(e, gameOver);
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            player.playerKeyIsUp(e, ammo, healthKit);
        }

        private void makeZombie()
        {
            zombiesList.Clear();

            int screenWidth = this.ClientSize.Width;
            int screenHeight = this.ClientSize.Height;

            int spawnArea = randNum.Next(1, 5); 

            int x = 0, y = 0;

            switch (spawnArea)
            {
                case 1: // Top-left corner
                    x = randNum.Next(0, screenWidth / 2);
                    y = randNum.Next(0, screenHeight / 2);
                    break;
                case 2: // Top-right corner
                    x = randNum.Next(screenWidth / 2, screenWidth);
                    y = randNum.Next(0, screenHeight / 2);
                    break;
                case 3: // Bottom-left corner
                    x = randNum.Next(0, screenWidth / 2);
                    y = randNum.Next(screenHeight / 2, screenHeight);
                    break;
                case 4: // Bottom-right corner
                    x = randNum.Next(screenWidth / 2, screenWidth);
                    y = randNum.Next(screenHeight / 2, screenHeight);
                    break;
            }

            ZombieAbstract zombie = new Zombie(player, this.ClientSize, new Point(x, y));
            ZombieAbstract zombieBig = new BigZombie(player, this.ClientSize, new Point(x, y));

            zombiesList.Add(zombie);
            zombiesList.Add(zombieBig);

            int randomIndex = randNum.Next(0, zombiesList.Count);
            this.Controls.Add(zombiesList[randomIndex]);
        }



        private void restartGame()
        {
            string[] tagsToRemove = { "gameOver", "reset", "ammoCount", "gameOver", "healthkit", "ammo", "bigZombie", "zombie" };

            this.Controls.Add(player);
            gameOver = false;
            isPause = true;

            GameTimer.Start();

            player.Image = Properties.Resources.hero_up;

            foreach (string tag in tagsToRemove)
            {
                removeObjectByTag(tag);
            }

            zombiesList.Clear();

            for (int i = 0; i < 2; i++)
                makeZombie();

            player.initPlayer();
            MainSound.Play();
            player.setIsAmmoVisible(false);
            player.setIsHealthkitVisable(false);
        }

        private void handleGameOver()
        {
            player.Image = Properties.Resources.skull;
            player.Size = new Size(99, 100);
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
                    gameState.PlayerHealth = player.getHealth();
                    gameState.PlayerScore = player.getScore();
                    gameState.PlayerAmmo = player.getAmmo();
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
                        player.setHealth(-player.getHealth());
                        player.setAmmo(-player.getAmmo());
                        player.setScore(-player.getScore());
                        player.setHealth(gameState.PlayerHealth);
                        player.setScore(gameState.PlayerScore);
                        player.setAmmo(gameState.PlayerAmmo);
                        player.setIsAmmoVisible(false);
                        player.setIsHealthkitVisable(false);

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
