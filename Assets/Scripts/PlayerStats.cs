using System;
public class PlayerStats
{
    private int level;
    private int cash;
    private string time;
    private int enemiesSlained;
    private int life;

    public PlayerStats(int level, int cash, string time, int enemiesSlained, int life)
    {
        this.level = level;
        this.cash = cash;
        this.time = time;
        this.enemiesSlained = enemiesSlained;
        this.life = life;
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

    public void setEnemies(int cash)
    {
        this.cash = cash;
    }

    public int getLife()
    {
        return this.life;
    }

    public void setLife(int life)
    {
        this.life = life;
    }
}
