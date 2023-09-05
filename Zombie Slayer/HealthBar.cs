using System;
using System.Drawing;
using System.Windows.Forms;

public class HealthBar : PictureBox
{
    private int currentHealth;
    private int maxHealth;

    public HealthBar(int maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.Size = new Size(maxHealth * 2, 10); 
        this.BackColor = Color.Red; 
    }

    public void UpdateHealth(int newHealth)
    {
        this.currentHealth = newHealth;
        if (currentHealth < 0)
            currentHealth = 0;
    }
}
