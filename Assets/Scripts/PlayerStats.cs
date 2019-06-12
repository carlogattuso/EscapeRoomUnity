using System;
public class PlayerStats
{
    private int level;
    private int cash;
    private string time;
    private int enemiesSlained;
    private int life;
    private string weapon;
    private string shield;

    public PlayerStats(int level, int cash, string time, int enemiesSlained, int life, string weapon, string shield)
    {
        this.level = level;
        this.cash = cash;
        this.time = time;
        this.enemiesSlained = enemiesSlained;
        this.life = life;
        this.weapon = weapon;
        this.shield = shield;
    }

    public int getLevel()
    {
        return this.level;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    public int getCash()
    {
        return this.cash;
    }

    public void setCash(int cash)
    {
        this.cash = cash;
    }

    public string getTime()
    {
        return this.time;
    }

    public void setTime(string time)
    {
        this.time = time;
    }

    public int getEnemiesSlained()
    {
        return this.enemiesSlained;
    }

    public void setEnemiesSlained(int enemies)
    {
        this.enemiesSlained = enemies;
    }

    public int getLife()
    {
        return this.life;
    }

    public void setLife(int life)
    {
        this.life = life;
    }

    public void setWeapon(string weapon)
    {
        this.weapon = weapon;
    }

    public string getWeapon()
    {
        return this.weapon;
    }

    public void setShield(string shield)
    {
        this.shield = shield;
    }

    public string getShield()
    {
        return this.shield;
    }
}
