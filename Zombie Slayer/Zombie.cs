namespace Zombie_Slayer
{
    public class Zombie : Entity
    {
        private Player playerInstance;
        private Random randNum = new Random();

        public Zombie(Player player, Size clientSize)
        {
            playerInstance = player;

            Tag = "zombie";
            Size = new Size(100, 100);
            SizeMode = PictureBoxSizeMode.StretchImage;
            initZombie(clientSize);
        }
        public void initZombie(Size clientSize)
        {
            Left = randNum.Next(0, clientSize.Width - Width);
            Top = randNum.Next(0, clientSize.Height - Height);
            Image = Properties.Resources.zombieGDown;
            up = false;
            down = false;
            left = false;
            right = false;
            facing = "down";
            health = 1;
            speed = 3;
        }


        public override void move(Size ClientSize)
        {
            if (playerInstance != null)
            {
                if (Left > playerInstance.Left)
                {
                    Left -= speed;
                    Image = Properties.Resources.zombieGLeft;
                }

                if (Left < playerInstance.Left)
                {
                    Left += speed;
                    Image = Properties.Resources.zombieGRight;
                }

                if (Top > playerInstance.Top)
                {
                    Top -= speed;
                    Image = Properties.Resources.zombieGUp;
                }

                if (Top < playerInstance.Top)
                {
                    Top += speed;
                    Image = Properties.Resources.zombieGDown;
                }
            }

        }

    }
}
