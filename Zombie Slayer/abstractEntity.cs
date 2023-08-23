public abstract class Entity : PictureBox
{
    protected bool up, down, right, left;
    protected string facing = string.Empty;
    protected int speed;
    protected int health;

    public abstract void Move();
}