using Zombie_Slayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zombie_Slayer;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
// Now, you can use 'soundFilePath' to play the sound in your application.


namespace Zombie_Slayer
{
    public partial class Form1 : Form
    {
        private Player player = new Player();
        private Ammo ammo;
        private HealthKit healthKit;
        private List<ZombieAbstract> zombiesList = new List<ZombieAbstract>();
        private List<shieldZombie> shieldZombiesList = new List<shieldZombie>();
        static public bool isBackToFront = false;
        public bool isGameOver = false;
        private object pts;
        private bool isPause = true;
        

        bool gameOver;
        Random randNum = new Random();
        SoundPlayer gameOverSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "GameOverSound.wav"));
        SoundPlayer MainSound = new SoundPlayer(Path.Combine(Application.StartupPath, "Sounds", "MainSound.wav"));


        public Form1()
        {
            InitializeComponent(); 
            restartGame();
            if (!gameOver)
                MainSound.Play();
            else
                MainSound.Stop();

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
                gameOverSound.Play();
            }
            ammoCount.Text = "Ammo: " + player.getAmmo();
            Kills.Text = "kills: " + player.getScore();

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
                            player.setHeath(30);
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

                    if (player.Bounds.IntersectsWith(entity.Bounds))
                        player.setHeath(-Constants.ZombieDammage);

                }
                if(entity is shieldZombie)
                {
                    shieldZombie sheildzombieEntity = (shieldZombie)entity;
                    sheildzombieEntity.move(ClientSize);
                    if (player.Bounds.IntersectsWith(entity.Bounds))
                        player.setHeath(-2 * Constants.ZombieDammage);
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

                    if (entity2 is PictureBox && (string)entity2.Tag == "bullet" && entity is shieldZombie)
                    {
                        shieldZombie zombieEntity = (shieldZombie)entity;

                        if (zombieEntity.Bounds.IntersectsWith(entity2.Bounds))
                        {
                            zombieEntity.zombiedamage--;
                            zombieEntity.shield = 0;
                            player.setScore(1);
                            if(zombieEntity.shield == 0 && zombieEntity.zombiedamage == 0)
                            {
                                this.Controls.Remove(entity2);
                                ((PictureBox)entity2).Dispose();
                                this.Controls.Remove(zombieEntity);
                                zombieEntity.Dispose();
                                shieldZombiesList.Remove(zombieEntity);
                                if(shieldZombiesList.Count > 2)
                                makeShieldZombie();
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
            ZombieAbstract zombie = new Zombie(player, this.ClientSize);
            ZombieAbstract zombie1 = new shieldZombie(player, this.ClientSize);
            zombiesList.Add(zombie);
            zombiesList.Add(zombie1);

            this.Controls.Add(zombie);
            this.Controls.Add(zombie1);
        }
        private void makeShieldZombie()
        {
            shieldZombie zombie = new shieldZombie(player, this.ClientSize);
            zombie.shield = 1;
            shieldZombiesList.Add(zombie);
            this.Controls.Add(zombie);
        }
        

        private void restartGame()
        {
            string[] tagsToRemove = { "gameOver", "reset", "ammoCount", "gameOver", "healthkit", "ammo" };

            this.Controls.Add(player);
            gameOver = false;

            GameTimer.Start();

            player.Image = Properties.Resources.hero_up;

            foreach (string tag in tagsToRemove) {
                removeObjectByTag(tag);
            }



            foreach (PictureBox zombie in zombiesList)
            {
                this.Controls.Remove(zombie);
            }
            zombiesList.Clear();
            foreach(PictureBox zombie in shieldZombiesList)
            {
                this.Controls.Remove(zombie);
            }

            shieldZombiesList.Clear();

            for (int i = 0; i < 3; i++)
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

        private void save_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            saveFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                GameState gameState = new GameState();
                gameState.PlayerData = player;  // Assuming Player class is serializable
                gameState.ZombiesList = zombiesList;

                IFormatter formatter = new BinaryFormatter();
                using (Stream stream = new FileStream(saveFileDialog1.FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, gameState);
                }
            }
        }


        private void pause_Click(object sender, EventArgs e)
        {
            if (isPause)
            {
                GameTimer.Stop();
                pause.Image = Properties.Resources.PlayGame;
                isPause = !isPause;
            }
            else
            {
                GameTimer.Start();
                pause.Image = Properties.Resources.PauseGame;
                isPause = !isPause;
            }
        }

        private void load_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            openFileDialog1.Filter = "model files (*.mdl)|*.mdl|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                GameState gameState;
                IFormatter formatter = new BinaryFormatter();

                using (Stream stream = new FileStream(openFileDialog1.FileName, FileMode.Open))
                {
                    gameState = (GameState)formatter.Deserialize(stream);
                }

                // Now you have the loaded game state, you can use it to restore the game's state
                player = gameState.PlayerData; // Update player data
                zombiesList = gameState.ZombiesList; // Update zombies list

                // Update other game elements as needed

                // Redraw the game or update UI accordingly
                // For example, you might need to remove existing controls and add the loaded ones
            }
        }


    }
}
