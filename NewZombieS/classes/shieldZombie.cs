using Zombie_Slayer;
using System;
using System.Drawing;
using System.Windows.Forms;
using Zombie_Slayer.Properties;


public class shieldZombie : ZombieAbstract
{
    private Player playerInstance;
    private Random randNum = new Random();
    public int shield = 1;
    public int zombiedamage = 2;

    public shieldZombie(Player player, Size clientSize) 
    {
        playerInstance = player;
        Tag = "shieldzombie";
        Size = new Size(Constants.ZombieSizeWidth, Constants.ZombieSizeHeight);
        SizeMode = PictureBoxSizeMode.StretchImage;
        initShieldZombie(clientSize);
    }

    public void initShieldZombie(Size clientSize)
    {
        Left = randNum.Next(0, clientSize.Width - Width);
        Top = randNum.Next(0, clientSize.Height - Height);
        Image = Resources.shieldZombieDown;
        health = Constants.shieldZombieInitialHealth;
        shield = Constants.ZombieShield;
        speed = Constants.shieldZombieSpeed;
    }

    public override void move(Size ClientSize)
    {
        if (playerInstance != null)
        {
            if (Left > playerInstance.Left)
            {
                Left -= speed;
                if (shield == 1)
                    Image = Resources.shieldZombieLeft;
                else
                    Image = Resources.zombieGLeft;
            }
            else if (Left < playerInstance.Left)
            {
                Left += speed;
                if (shield == 1)
                    Image = Resources.shieldZombieRight;
                else
                    Image = Resources.zombieGRight;
            }

            if (Top > playerInstance.Top)
            {
                Top -= speed;
                if (shield == 1)
                    Image = Resources.shieldZombieUp;
                else
                    Image = Resources.zombieGUp;
            }
            else if (Top < playerInstance.Top)
            {
                Top += speed;
                if (shield == 1)
                    Image = Resources.shieldZombieDown;
                else
                    Image = Resources.zombieGDown;
            }
        }
    }
}