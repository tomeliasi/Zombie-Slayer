using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Linq;

namespace Zombie_Slayer
{

    public partial class Form1 : Form
    {
        private Player player = new Player();
        private Ammo ammo;
        private HealthKit healthKit;
        private List<MonsterAbstract> zombiesList = new List<MonsterAbstract>();
        private List<BigZombie> bigZombiesList = new List<BigZombie>();
        private object pts;
        private bool isPause = true;
        private GameState gameState = new GameState();
        private bool isBoss = true;
        private Dragon currentDragon = null;
        private HealthBar dragonHealthBar;

        static public bool isBackToFront = false;
        public bool isGameOver = false;


        bool gameOver;
        Random randNum = new Random();
        SoundPlayer gameOverSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "GameOverSound.wav"));
        SoundPlayer MainSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "MainSound.wav"));

        public Form1()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            restartGame();
            if (!gameOver)
                MainSound.Play();
            else
                MainSound.Stop();

            ammo = new Ammo(player, this);
            healthKit = new HealthKit(player, this);


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

            if (player.getScore() >= Constants.BossLevel && isBoss)
            {
                makeDragon();
                isBoss = false;
            }

            if (currentDragon != null)
            {
            Console.WriteLine(currentDragon.getHealth());
                currentDragon.move(ClientSize);
                this.Invalidate(currentDragon.Bounds);
                currentDragon.healthBar.Visible = true;
            }

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
                        ammo = null;
                        ammo = new Ammo(player, this);
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
                            player.setHealth(30);
                        else
                            player.setMaxHealth();

                        player.setIsHealthkitVisable(false);
                        healthKit = null;
                        healthKit = new HealthKit(player, this);
                    }
                }
                if (entity is Zombie)
                {
                    Zombie zombieEntity = (Zombie)entity;
                    zombieEntity.move(ClientSize);
                    this.Invalidate(zombieEntity.Bounds);

                    if (player.Bounds.IntersectsWith(entity.Bounds))
                        player.setHealth(-Constants.ZombieDammage);

                }

                if (entity is BigZombie)
                {
                    BigZombie bigZombieEntity = (BigZombie)entity;
                    bigZombieEntity.move(ClientSize);
                    this.Invalidate(bigZombieEntity.Bounds);


                    if (player.Bounds.IntersectsWith(entity.Bounds))
                        player.setHealth(-bigZombieEntity.getDemmage());

                }

                if (entity is Dragon)
                {
                    Dragon dragonEntity = (Dragon)entity;
                    dragonEntity.move(ClientSize);
                    this.Invalidate(dragonEntity.Bounds);


                    if (player.Bounds.IntersectsWith(dragonEntity.Bounds))
                        player.setHealth(-dragonEntity.getDemmage());

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


                    if (entity2 is PictureBox && (string)entity2.Tag == "bullet" && entity is BigZombie)
                    {
                        BigZombie bigZombieEntity = (BigZombie)entity;
                        if (bigZombieEntity.Bounds.IntersectsWith(entity2.Bounds))
                        {
                            this.Controls.Remove(entity2);
                            ((PictureBox)entity2).Dispose();

                            bigZombieEntity.getDamaged(1);


                            if (bigZombieEntity.Width < 100)
                            {
                                player.setScore(1);
                                bigZombiesList.Remove(bigZombieEntity);
                                bigZombieEntity.Dispose();
                                makeZombie();
                            }
                        }
                    }

                    if (entity2 is PictureBox && (string)entity2.Tag == "bullet" && entity is Dragon)
                    {
                        Dragon dragonEntity = (Dragon)entity;
                        if (dragonEntity.Bounds.IntersectsWith(entity2.Bounds))
                        {
                            this.Controls.Remove(entity2);
                            ((PictureBox)entity2).Dispose();

                            dragonEntity.getDamaged(1);


                            if (dragonEntity.getHealth() == 0)
                            {
                                player.setScore(20);
                                this.Controls.Remove(dragonEntity);
                                dragonEntity.Dispose();
                            }
                        }
                    }
                }

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
            MonsterAbstract zombie = new Zombie(player, this.ClientSize);
            MonsterAbstract zombieBig = new BigZombie(player, this.ClientSize);
            zombiesList.Add(zombie);
            zombiesList.Add(zombieBig);
            int randomIndex = randNum.Next(0, zombiesList.Count);
            this.Controls.Add(zombiesList[randomIndex]);
        }

        private void makeDragon()
        {
            this.Refresh();

            string[] tagsToRemove = { "bigZombie", "zombie" };
            zombiesList.Clear();
            bigZombiesList.Clear();

            removeElements(tagsToRemove);
            currentDragon = new Dragon(player, this.ClientSize);

            currentDragon.healthBar.Location = new Point(currentDragon.Left, currentDragon.Top - currentDragon.healthBar.Height);

            this.Controls.Add(currentDragon);
            this.Controls.Add(currentDragon.healthBar);
            currentDragon.healthBar.BringToFront();

            player.setAmmo(-player.getAmmo());
            player.setAmmo(10);
            currentDragon.healthBar.Visible = true;
        }


        private void restartGame()
        {
            this.Controls.Add(player);
            gameOver = false;
            isPause = true;
            isBoss = true;
            GameTimer.Start();

            player.Image = Properties.Resources.hero_up;

            zombiesList.Clear();

            string[] tagsToRemove = { "gameOver", "reset", "ammoCount", "gameOver", "healthkit", "ammo", "bigZombie", "zombie", "dragon" };

            removeElements(tagsToRemove);
            for (int i = 0; i < 2; i++)
                makeZombie();

            player.initPlayer();
            MainSound.Play();
            player.setIsAmmoVisible(false);
            player.setIsHealthkitVisable(false);
        }

        private void removeElements(string[] tagsToRemove)
        {
            foreach (string tag in tagsToRemove)
            {
                foreach (Control control in this.Controls)
                {
                    if (control.Tag != null && control.Tag.ToString() == tag)
                    {
                        this.Controls.Remove(control);
                        control.Dispose();
                    }
                }
            }
        }



        private void handleGameOver()
        {
            player.Image = Properties.Resources.skull;
            player.Size = new Size(99, 100);
            GameTimer.Stop();
            MainSound.Dispose();
            renderGameoverSection();

            if (dragonHealthBar != null)
            {
                this.Controls.Remove(dragonHealthBar);
                dragonHealthBar.Dispose();
            }

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
