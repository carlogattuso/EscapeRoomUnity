﻿using System.Collections;
using System.Collections.Generic;
using System;

public class APIAndroid
{
    private PlayerStats playerStats;
    private List<Object> inventory = new List<Object>();
    private Map map = new Map();

    public APIAndroid(){}

    public PlayerStats getPlayerStats()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            String stringAndroid = javaClass.CallStatic<String>("getPlayerStats");
            Debug.Log("Stats: ");
            Debug.Log(stringAndroid);
        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }
#else
        string stringAndroid = "1,50,00:05:23,2,200,woodSword,ironShield";
#endif


        string[] playerStatsVector = stringAndroid.Split(',');

        PlayerStats player = new PlayerStats(Int32.Parse(playerStatsVector[0]), Int32.Parse(playerStatsVector[1]),
            playerStatsVector[2], Int32.Parse(playerStatsVector[3]), Int32.Parse(playerStatsVector[4]),
            playerStatsVector[5], playerStatsVector[6]);

        this.playerStats = player;
        return this.playerStats;
    }

    public void setPlayerStats(PlayerStats stats)
    {
        this.playerStats = stats;
    }

    public List<Object> getInventory()
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            String stringAndroid = javaClass.CallStatic<String>("getInventory");
            Debug.Log("Inventory: ");
            Debug.Log(stringAndroid);
        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }
#else
        string stringAndroid = "llave,llaveR,1/llave,llaveA,2/pista,pistaAmarilla,1/pista,pistaAzul,1";
#endif

        string[] inventoryVector = stringAndroid.Split('/');

        List<Object> inventoryList = new List<Object>();

        foreach(string s in inventoryVector)
        {
            string [] sVector = s.Split(',');
            Object o = new Object(sVector[0], sVector[1], sVector[2]);
            inventoryList.Add(o);
        }

        this.inventory = inventoryList;
        return this.inventory;
    }

    public void setInventory(List<Object> inventory)
    {
        this.inventory = inventory;
    }

    public Map getMap(int level)
    {
#if UNITY_ANDROID
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            String stringAndroid = javaClass.CallStatic<String>("getMap", level);
            Debug.Log("Map: ");
            Debug.Log(stringAndroid);

            string[] mapa1Vector = stringAndroid.Split('*');

                string[] boxes1Vector = mapa1Vector[2].Split('/');

                List<Box> boxes = new List<Box>();

                foreach(string s in boxes1Vector)
                {
                    string [] sVector = s.Split(',');
                    boxes.Add(new Box(Int32.Parse(sVector[0]), Int32.Parse(sVector[1]), sVector[2]));
                }

                Map mapObject = new Map(mapa1Vector[0], Int32.Parse(mapa1Vector[1]),boxes);
                this.map = mapObject;

                return this.map;
        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }
#else
        string mapa1 =   "bbbbbbbOOOOOOOOOOOOOObbbbbbb" +
                         "bbbbbbbLTTTTTTTTTTTTRbbbbbbb" +
                         "bbbbbbbLV          VRbbbbbbb" +
                         "bbbbbbbL  N     X   Rbbbbbbb" +
                         "bbbbbbbL            Rbbbbbbb" +
                         "bbbbbbbL      N     Rbbbbbbb" +
                         "bbbbbbbL            Rbbbbbbb" +
                         "bbbbbbblBwBc CBBwBBBrbbbbbbb" +
                         "bbbbbbbbbbOL1ROObbbbbbbbbbbb" +
                         "bbbbbbbbbbLf fTRbbbbbbbbbbbb" +
                         "bbbbbbbbbbLV   Rbbbbbbbbbbbb" +
                         "bbbbbbbbbbL    RbOOOOOObbbbb" +
                         "bbbbbbbbbbL    RbLTTTTRbbbbb" +
                         "bbbbbbbbbbL    RbL    Rbbbbb" +
                         "bbbbbbbbbbL    RbL    Rbbbbb" +
                         "bbbbbbbbbbL    RbL  N Rbbbbb" +
                         "bbbbbbbbbbL N  RbL    Rbbbbb" +
                         "OOOOOOOOObL    Rblc Cwrbbbbb" +
                         "LTTTTTTTTTT    ROOL3RObbbbbb" +
                         "LV             TTTf fRbbbbbb" +
                         "L                    Rbbbbbb" +
                         "lwwBc CwBBBwBBwBwBwBBrbbbbbb" +
                         "OOOOL3ROObbbbbbbbbbbbbbbbbbb" +
                         "LTTTf fTRbbbbbbbbbbbbbbbbbbb" +
                         "LV      Rbbbbbbbbbbbbbbbbbbb" +
                         "L       Rbbbbbbbbbbbbbbbbbbb" +
                         "L      VRbbbbbbbbbbbbbbbbbbb" +
                         "lwBBwBBBrbbbbbbbbbbbbbbbbbbb*28*"+
                         "4,4,Capital de España?-Madrid-Real/"+
                         "18,8,En qué equipo juega Messi?-Barcelona-Empieza por B/"+
                         "11,18,Mas vale tarde que...?-nunca-jamás";

        string mapa2 = "bbbbbbbbbbbbbbbbbb" +
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
                       "OOOOL3ROObbbbbbbbb" +
                       "LTTTf fTRbbbbbbbbb" +
                       "L       Rbbbbbbbbb" +
                       "L       Rbbbbbbbbb" +
                       "L       Rbbbbbbbbb" +
                       "lwBBwBBBrbbbbbbbbb*18*"+
                       "4,4,Quien es el más feo de la UPC?-Mario-Empieza por M";

        switch (level)
        {
            case 1:
                //Cogemos el mapa 1
                string[] mapa1Vector = mapa1.Split('*');

                string[] boxes1Vector = mapa1Vector[2].Split('/');

                List<Box> boxes = new List<Box>();

                foreach(string s in boxes1Vector)
                {
                    string [] sVector = s.Split(',');
                    boxes.Add(new Box(Int32.Parse(sVector[0]), Int32.Parse(sVector[1]), sVector[2]));
                }

                Map mapObject = new Map(mapa1Vector[0], Int32.Parse(mapa1Vector[1]),boxes);
                this.map = mapObject;

                break;
            case 2:
                //Cogemos el mapa 2
                string[] mapa2Vector = mapa2.Split('*');

                string[] boxes2Vector = mapa2Vector[2].Split('/');

                List<Box> boxes2 = new List<Box>();

                foreach (string s in boxes2Vector)
                {
                    string[] sVector = s.Split(',');
                    boxes2.Add(new Box(Int32.Parse(sVector[0]), Int32.Parse(sVector[1]), sVector[2]));
                }

                Map mapObject2 = new Map(mapa2Vector[0], Int32.Parse(mapa2Vector[1]), boxes2);
                this.map = mapObject2;

                break;
        }
        
        return this.map;
#endif
    }
    
    public void setMap(Map map)
    {
        this.map = map;
    }

    public string sendPlayerStats(PlayerStats playerStats)
    {
        string stringUnity = "";

        stringUnity += playerStats.getLevel() + ",";
        stringUnity += playerStats.getCash() + ",";
        stringUnity += playerStats.getTime() + ",";
        stringUnity += playerStats.getEnemiesSlained() + ",";
        stringUnity += playerStats.getWeapon() + ",";
        stringUnity += playerStats.getShield() + ",";

        return stringUnity;
    }

    public string sendInventory(List<Object> inventory)
    {
        string stringUnity = "";

        foreach(Object o in inventory)
        {
            stringUnity += o.getType() + ",";
            stringUnity += o.getName() + ",";
            stringUnity += o.getAttribute() + ",";
            stringUnity += "/";
        }

        return stringUnity;
    }
}
