using System.Security.Cryptography.X509Certificates;

namespace Zombie_Slayer
{
    public partial class Form1 : Form
    {
        bool up, down, right, left, gameOver;
        string facing = "up";
        int playerHealth = 100;
        int speed = 20;
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


            ammoCount.Text = "Ammo: " + ammo;
            kills.Text = "kills: " + score;

            if (left && player.Left > 4)
                player.Left -= speed;

            if (right && player.Left + player.Width < this.ClientSize.Width)
                player.Left += speed;

            if (up && player.Top > 65)
                player.Top -= speed;

            if (down && player.Top + player.Height < this.ClientSize.Height)
                player.Top += speed;

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
                        playerHealth+=30;
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
                            score++;
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
            if (gameOver == false)
            {
                if (e.KeyCode == Keys.Left)
                {
                    left = true;
                    facing = "left";
                    player.Image = Properties.Resources.hero_left;
                }

                if (e.KeyCode == Keys.Right)
                {
                    right = true;
                    facing = "right";
                    player.Image = Properties.Resources.hero_right;
                }

                if (e.KeyCode == Keys.Up)
                {
                    up = true;
                    facing = "up";
                    player.Image = Properties.Resources.hero_up;
                }

                if (e.KeyCode == Keys.Down)
                {
                    down = true;
                    facing = "down";
                    player.Image = Properties.Resources.hero__down;
                }
            }
        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                left = false;

            if (e.KeyCode == Keys.Right)
                right = false;

            if (e.KeyCode == Keys.Up)
                up = false;

            if (e.KeyCode == Keys.Down)
                down = false;

            if (e.KeyCode == Keys.Space && ammo > 0)
            {
                ammo--;
                ShootBullet(facing);
            }
            if (ammo < 1 && !isAmmoVisable)
            {
                MakeAmmo();
            }
            if (playerHealth < 30 && !isHealthkitVisable )
            {
                MakeHealthKit();
            }
        }

        private void ShootBullet(string direction)
        {
            if (gameOver == false)
            {
                Bullet ShootBullet = new Bullet();
                ShootBullet.direction = direction;
                ShootBullet.bulletLeft = player.Left + (player.Width / 2) - 12;
                ShootBullet.bulletTop = player.Top + (player.Height / 2) - 10;
                ShootBullet.makeBullet(this);
            }
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

        private void MakeAmmo()
        {
            PictureBox ammo = new PictureBox();
            ammo.Tag = "ammo";
            ammo.Image = Properties.Resources.ammunition;
            ammo.Left = randNum.Next(0, this.ClientSize.Width - ammo.Width);
            ammo.Top = randNum.Next(0, this.ClientSize.Height - ammo.Height);
            ammo.Size = new Size(70, 60);
            ammo.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(ammo);
            ammo.BringToFront();
            player.BringToFront();
            isAmmoVisable = true;
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

        
    }
}