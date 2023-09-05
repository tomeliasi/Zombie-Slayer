using System.Drawing;
using System.Windows.Forms;

namespace Zombie_Slayer
{
    public class Player : Entity
    {
        private int ammo = 0;
        private bool isAmmoVisible = false;
        private bool isHealthkitVisable = false;
        private int score = 0;

        public int getAmmo() { return ammo; }
        public int getScore() { return score; }
        public bool getIsAmmoVisible() { return isAmmoVisible; }
        public bool getIsHealthkitVisable() { return isHealthkitVisable; }
        public int getHealth() { return health; }
        public int getSpeed() { return speed; }
        public void setHealth(int health) { this.health += health; }

        public void setMaxHealth() { this.health = 100; }
        public void setScore(int score) { this.score += score; }
        public void setAmmo(int ammo) { this.ammo += ammo; }
        public void setIsAmmoVisible(bool value) { isAmmoVisible = value; }
        public void setIsHealthkitVisable(bool value) { isHealthkitVisable = value; }

        public Player()
        {
            initializePlayer();
        }

        public void initializePlayer()
        {
            Tag = "player";
            Size = new Size(Constants.PlayerSizeWidth, Constants.PlayerSizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            initPlayer();
            BackColor = Color.Transparent;
        }

        public void initPlayer()
        {
            Image = Properties.Resources.hero_up;
            Location = new Point(Constants.PlayerLocationWidth, Constants.PlayerLocationHeight);
            up = false;
            down = false;
            left = false;
            right = false;
            isAmmoVisible = false;
            facing = "up";
            health = Constants.PlayerInitialHealth;
            speed = Constants.PlayerSpeed;
            ammo = Constants.PlayerInitialAmmo;
            score = Constants.PlayerInitialScore;
        }

        public override void move()
        {
            if (left && Left > 4)
                Left -= speed;

            if (right && Left + Width < clientSize.Width)
                Left += speed;

            if (up && Top > 65)
                Top -= speed;

            if (down && Top + Height < clientSize.Height)
                Top += speed;
        }

        public void playerKeyIsDown(KeyEventArgs e, bool gameOver)
        {
            if (!gameOver)
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        left = true;
                        facing = "left";
                        Image = Properties.Resources.hero_left;
                        break;

                    case Keys.Right:
                        right = true;
                        facing = "right";
                        Image = Properties.Resources.hero_right;
                        break;

                    case Keys.Up:
                        up = true;
                        facing = "up";
                        Image = Properties.Resources.hero_up;
                        break;

                    case Keys.Down:
                        down = true;
                        facing = "down";
                        Image = Properties.Resources.hero__down;
                        break;
                }
            }
        }

        public void playerKeyIsUp(KeyEventArgs e, Ammo newAmmo, HealthKit healthKit)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    left = false;
                    break;

                case Keys.Right:
                    right = false;
                    break;

                case Keys.Up:
                    up = false;
                    break;

                case Keys.Down:
                    down = false;
                    break;

                case Keys.Space:
                    if (ammo > 0)
                        Shoot();
                    break;
            }

            if (ammo < 1 && !isAmmoVisible)
                newAmmo.MakeAmmo();

            if (health < 30 && !isHealthkitVisable)
                healthKit.makeHealthKit();
        }
        public void Shoot()
        {
            if (ammo > 0)
            {
                ammo--;
                ShootBullet(facing);
            }
        }

        private void ShootBullet(string direction)
        {
            Bullet shootBullet = new Bullet();
            shootBullet.direction = direction;
            shootBullet.bulletLeft = Left + (Width / 2) - 12;
            shootBullet.bulletTop = Top + (Height / 2) - 10;
            shootBullet.makeBullet();
            this.Invalidate(shootBullet.Bounds);

        }
    }

}
