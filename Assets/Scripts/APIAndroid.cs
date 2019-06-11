using System.Collections;
using System.Collections.Generic;

public class APIAndroid
{
    private PlayerStats playerStats;
    private List<Object> inventory;
    private List<Map> maps;

    public APIAndroid()
    {
        playerStats = new PlayerStats(1,50,"00:05:23",2,80);

        inventory = new List<Object>();

        inventory.Add(new Object("llave", "red"));
        inventory.Add(new Object("llave", "yellow"));
        inventory.Add(new Object("llave", "yellow"));
        inventory.Add(new Object("weapon", "sword"));
        inventory.Add(new Object("shield", "wood"));

        maps = new List<Map>();

        List<Box> boxes = new List<Box>();
        boxes.Add(new Box(4, 4, "Izan"));
        boxes.Add(new Box(18, 8, "Carlo"));
        boxes.Add(new Box(11, 18, "Mario"));

        string level1 =      "bbbbbbbOOOOOOOOOOOOOObbbbbbb" +
                             "bbbbbbbLTTTTTTTTTTTTRbbbbbbb" +
                             "bbbbbbbL            Rbbbbbbb" +
                             "bbbbbbbL  N         Rbbbbbbb" +
                             "bbbbbbbL            Rbbbbbbb" +
                             "bbbbbbbL      N     Rbbbbbbb" +
                             "bbbbbbbL            Rbbbbbbb" +
                             "bbbbbbblBwBc CBBwBBBrbbbbbbb" +
                             "bbbbbbbbbbOL1ROObbbbbbbbbbbb" +
                             "bbbbbbbbbbLT TTRbbbbbbbbbbbb" +
                             "bbbbbbbbbbL    Rbbbbbbbbbbbb" +
                             "bbbbbbbbbbL    RbOOOOOObbbbb" +
                             "bbbbbbbbbbL    RbLTTTTRbbbbb" +
                             "bbbbbbbbbbL    RbL    Rbbbbb" +
                             "bbbbbbbbbbL    RbL    Rbbbbb" +
                             "bbbbbbbbbbL    RbL  N Rbbbbb" +
                             "bbbbbbbbbbL N  RbL    Rbbbbb" +
                             "OOOOOOOOObL    Rblc Cwrbbbbb" +
                             "LTTTTTTTTTT    RbbL2Rbbbbbbb" +
                             "L              TTTT Rbbbbbbb" +
                             "L                   Rbbbbbbb" +
                             "lwwBc CwBBBwBBwBwBwBrbbbbbbb" +
                             "OOOOL3ROObbbbbbbbbbbbbbbbbbb" +
                             "LTTTT TTRbbbbbbbbbbbbbbbbbbb" +
                             "L       Rbbbbbbbbbbbbbbbbbbb" +
                             "L       Rbbbbbbbbbbbbbbbbbbb" +
                             "L       Rbbbbbbbbbbbbbbbbbbb" +
                             "lwBBwBBBrbbbbbbbbbbbbbbbbbbb";

        Map mapa1 = new Map(level1,28,boxes);

        List<Box> boxes2 = new List<Box>();
        boxes2.Add(new Box(4, 4, "Sheila"));

        string level2 =     "bbbbbbbbbbbbbbbbbb" +
                            "bbbbbbbbbbbbbbbbbb" +
                            "bbbbbbbbbbbbbbbbbb" +
                            "bbbbbbbbbbbbbbbbbb" +
                            "bbbbbbbbbbbbbbbbbb" +
                            "bbbbbbbbbbbbbbbbbb" +
                            "bbbbbbbbbbbbbbbbbb" +
                            "OOOOOOOOOOOOOOOOOO" +
                            "LTTTTTTTTTTTTTTTTR" +
                            "L                R" +
                            "L                R" +
                            "lwwBc CwBBwBwwBwBr" +
                            "OOOOL1ROObbbbbbbbb" +
                            "LTTTT TTRbbbbbbbbb" +
                            "L       Rbbbbbbbbb" +
                            "L       Rbbbbbbbbb" +
                            "L       Rbbbbbbbbb" +
                            "lwBBwBBBrbbbbbbbbb";

        Map mapa2 = new Map(level2,18, boxes2);

        this.maps.Add(mapa1);
        this.maps.Add(mapa2);
    }

    public PlayerStats getPlayerStats()
    {
        return this.playerStats;
    }

    public void setPlayerStats(PlayerStats stats)
    {
        this.playerStats = stats;
    }

    public List<Object> getInventory()
    {
        return this.inventory;
    }

    public void setInventory(List<Object> inventory)
    {
        this.inventory = inventory;
    }

    public List<Map> getMaps()
    {
        return this.maps;
    }

    public void setMaps(List<Map> maps)
    {
        this.maps = maps;
    }
}
