using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class APIAndroid
{
    private PlayerStats playerStats;
    private List<Object> inventory = new List<Object>();
    private Map map = new Map();

    public APIAndroid(){}

    public PlayerStats getPlayerStats()
    {
#if UNITY_ANDROID

        String stringAndroid = null;

        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            stringAndroid = javaClass.CallStatic<String>("getPlayerStats");
            Debug.Log("Stats: ");
            Debug.Log(stringAndroid);
        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }

        if (stringAndroid == null)
            stringAndroid = "1,50,00:05:23,2,200,woodSword,ironShield";

#else
        string stringAndroid = "4,50,00:05:23,2,200,goldSword,goldShield";
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

        String stringAndroid = null;

        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            stringAndroid = javaClass.CallStatic<String>("getInventory");
            Debug.Log("Inventory: ");
            Debug.Log(stringAndroid);
        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }

        if (stringAndroid == null)
            stringAndroid = "llave,llaveR,1/llave,llaveA,2/pista,pistaAmarilla,1/pista,pistaAzul,1";

#else
        string stringAndroid = "llave,llaveR,1/llave,llaveB,2/pista,pistaAmarilla,1/pista,pistaAzul,1";
#endif

        string[] inventoryVector = stringAndroid.Split('/');

        List<Object> inventoryList = new List<Object>();

        foreach (string s in inventoryVector)
        {
            string[] sVector = s.Split(',');
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

        String stringAndroid = null;

        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            stringAndroid = javaClass.CallStatic<String>("getMap", level);
            Debug.Log("Map: ");
            Debug.Log(stringAndroid);


        }
        catch (Exception ex)
        {

            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }

        if (stringAndroid == null)
            stringAndroid = "bbbbbbbbbbbbbbbbbb" +
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
                       "lwBBwBBBrbbbbbbbbb*18*" +
                       "4,4,Quien es el más feo de la UPC?-Mario-Empieza por M";


        string[] mapa1Vector = stringAndroid.Split('*');

        string[] boxes1Vector = mapa1Vector[2].Split('/');

        List<Box> boxes = new List<Box>();

        foreach (string s in boxes1Vector)
        {
            string[] sVector = s.Split(',');
            boxes.Add(new Box(Int32.Parse(sVector[0]), Int32.Parse(sVector[1]), sVector[2]));
        }

        Map mapObject = new Map(mapa1Vector[0], Int32.Parse(mapa1Vector[1]), boxes);
        this.map = mapObject;
        return this.map;

#else

        string mapa1 =   "bbbbbbbOOOOOOOOOOOOOObbbbbbb" +
                         "bbbbbbbLTTTTTTTTTTTTRbbbbbbb" +
                         "bbbbbbbLV    H     VRbbbbbbb" +
                         "bbbbbbbL        X   Rbbbbbbb" +
                         "bbbbbbbL            Rbbbbbbb" +
                         "bbbbbbbL      N     Rbbbbbbb" +
                         "bbbbbbbL H          Rbbbbbbb" +
                         "bbbbbbblBwBc CBBwBBBrbbbbbbb" +
                         "bbbbbbbbbbOL1ROObbbbbbbbbbbb" +
                         "bbbbbbbbbbLf fTRbbbbbbbbbbbb" +
                         "bbbbbbbbbbLV v Rbbbbbbbbbbbb" +
                         "bbbbbbbbbbL    RbOOOOOObbbbb" +
                         "bbbbbbbbbbL    RbLTTTTRbbbbb" +
                         "bbbbbbbbbbL  H RbL    Rbbbbb" +
                         "bbbbbbbbbbL    RbL    Rbbbbb" +
                         "bbbbbbbbbbL    RbL  N Rbbbbb" +
                         "bbbbbbbbbbL N  RbL    Rbbbbb" +
                         "OOOOOOOOObL    Rblc Cwrbbbbb" +
                         "LTTTTTTTTTT    ROOL2RObbbbbb" +
                         "LV  v   H      TTTf fRbbbbbb" +
                         "L                    Rbbbbbb" +
                         "lwwBc CwBBBwBBwBwBwBBrbbbbbb" +
                         "OOOOL3ROObbbbbbbbbbbbbbbbbbb" +
                         "LTTTf fTRbbbbbbbbbbbbbbbbbbb" +
                         "LV     vRbbbbbbbbbbbbbbbbbbb" +
                         "L     H Rbbbbbbbbbbbbbbbbbbb" +
                         "L      VRbbbbbbbbbbbbbbbbbbb" +
                         "lwBBwBBBrbbbbbbbbbbbbbbbbbbb*28*" +
                         "4,4,Capital de España?-Madrid-Real/" +
                         "18,8,En qué equipo juega Messi?-Barcelona-Empieza por B/" +
                         "11,18,Mas vale tarde que...?-nunca-jamás/" +
                         "5,1,vida-20/" +
                         "7,7,dinero-15/" +
                         "16,6,dinero-30/" +
                         "12,13,vida-10/" +
                         "8,20,dinero-10/" +
                         "12,24,vida-50/" +
                         "6,2,Pepito-Ten cuidado donde pisas...-Podrias llevarte una sorpresa.../" +
                         "3,7,Jaimito-Los enemigos son muy peligrosos en este castillo...-Aunque parezca que no se mueven.../" +
                         "12,16,Pedrito-Has comprado pistas?-En la tienda puedes hacerlo...";

        string mapa2 = "bbbbbbbbbbbbbbbbbbbbbbb" +
                       "bbbbbbbbbbbbbbbbbbbbbbb" +
                       "bbbbbbbbbbbbbbbbbbbbbbb" +
                       "bbbbbbbbbbbbbbbbbbbbbbb" +
                       "OOOOOOOOOOOOOOOOOO00000" +
                       "LTTTTTTTTTTTTTTTTTTTTTR" +
                       "L N  R    X   V     V R" +
                       "L  L1R                R" +
                       "L  L RTTTTTTTf1fTTTTTTR" +
                       "L  L RL N             R" +
                       "L HL RL N  RLTTTTTTTTTR" +
                       "LBBL RL   NRL        VR" +
                       "L00L RL    RL         R" +
                       "bbLf2fTf2fTTf2fTTTTT TR" +
                       "bbL            RL     R" +
                       "bbL     v      RL   H R" +
                       "lwwBc CwBBwBl rwBwBwBwr" +
                       "OOOOL3ROObbbL Rbbbbbbbb" +
                       "LTTTf fTRbbbL Rbbbbbbbb" +
                       "LV      RLTTl lTTTTTTrb" +
                       "L      VRL        N HRb" +
                       "L       RLwwwwrwwwwwwrb" +
                       "lwBBwBBBrbbbbbbbbbbbbbb*23*" +
                       "4,4,Quien va acabar el Unity?-Izan-Se llama iz.../" +
                       "3,8,Como sigue la canción Cristina?-Cristina-Si te fijas ya te lo he dicho/" +
                       "7,8,Despues del 1 va el...?-2-Va antes que el 3/" +
                       "12,8,Despues del 3 va el...?-4-Va antes que el 5/" +
                       "3,14,Como se llama el novio de la hermana del Mario?-JSON-El nombre lo utilizamos constantemente./" +
                       "13,13,Que nota saco Carlo Gattuso en el primer control de telematica?-0.8-Puede ser que suspendiera./" +
                       "7,6,Pepito-El segundo nivel no será tan fácil como piensas...-No creo que lo consigas.../" +
                       "2,6,vida-20/" +
                       "19,1,dinero-10/" +
                       "19,6,dinero-20/" +
                       "1,11,vida-20";


        string mapa3 = "OOOOOOOOObb" +
                       "LTTTTTTTRbb" +
                       "LV     XRbb" +
                       "L H v   Rbb" +
                       "lwwBc Cwrbb" +
                       "OOOOL2ROObb" +
                       "LTTTf fTRbb" +
                       "LV      Rbb" +
                       "L      VRbb" +
                       "L       Rbb" +
                       "lwBBwBBBrbb*11*" +
                       "4,4,Quien es el más tonto del pueblo?-Carlo-Tiene una altura peculiar/"+
                       "1,6,vida-20/"+
                       "3,6,Pedrito-Donde crees que vas con esa vida?-Coge este cofre que lo necesitarás...";

        string mapa4 = "bbbbbbbbbbbbb" +
                       "bbbbbbbbbbbbb" +
                       "OOOOOOOOOOOOb" +
                       "LTTTTTTTTTTRb" +
                       "LH        HRb" +
                       "L   P      Rb" +
                       "l   t      Rb" +
                       "LH         Rb" +
                       "LwBBwBBBBc Rb" +
                       "LTTTTTTTTf3bb" +
                       "LV     v   Rb" +
                       "L         VRb" +
                       "lwBBwBBBBwwrb*13*" +
                       "9,2,Que es lo que más deseas en este mundo?-Aprobar DSA-Si no lo haces tendrás que pagar 10 créditos otra vez./" +
                       "0,4,vida-30/" +
                       "9,7,dinero-20/" +
                       "0,7,vida-45/" +
                       "6,1,Pedrito-Nadie pensaba que llegarias hasta aquí...-Pero lo has conseguido...-entra ahí dentro a por lo que te pertenece.";


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
            case 3:
                //Cogemos el mapa 1
                string[] mapa3Vector = mapa3.Split('*');

                string[] boxes3Vector = mapa3Vector[2].Split('/');

                List<Box> boxes3 = new List<Box>();

                foreach (string s in boxes3Vector)
                {
                    string[] sVector = s.Split(',');
                    boxes3.Add(new Box(Int32.Parse(sVector[0]), Int32.Parse(sVector[1]), sVector[2]));
                }

                Map mapObject3 = new Map(mapa3Vector[0], Int32.Parse(mapa3Vector[1]), boxes3);
                this.map = mapObject3;

                break;
            case 4:
                //Cogemos el mapa 1
                string[] mapa4Vector = mapa4.Split('*');

                string[] boxes4Vector = mapa4Vector[2].Split('/');

                List<Box> boxes4 = new List<Box>();

                foreach (string s in boxes4Vector)
                {
                    string[] sVector = s.Split(',');
                    boxes4.Add(new Box(Int32.Parse(sVector[0]), Int32.Parse(sVector[1]), sVector[2]));
                }

                Map mapObject4 = new Map(mapa4Vector[0], Int32.Parse(mapa4Vector[1]), boxes4);
                this.map = mapObject4;

                break;
        }
        
        return this.map;
#endif
    }
    
    public void setMap(Map map)
    {
        this.map = map;
    }

    public void sendPlayerStats(PlayerStats playerStats)
    {
        string stringUnity = "";

        stringUnity += playerStats.getLevel() + ",";
        stringUnity += playerStats.getCash() + ",";
        stringUnity += playerStats.getTime() + ",";
        stringUnity += playerStats.getEnemiesSlained() + ",";
        stringUnity += playerStats.getLife() + ",";
        stringUnity += playerStats.getWeapon() + ",";
        stringUnity += playerStats.getShield() + ",";

        stringUnity = stringUnity.TrimEnd(',');

#if UNITY_ANDROID
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            javaClass.CallStatic("sendStats", stringUnity);
            Debug.Log("stringStats: ");
            Debug.Log(stringUnity);

        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getPlayerStats");
            Debug.Log(ex);
        }
#endif
    }

    public void sendFinalStats(PlayerStats playerStats)
    {
        string stringUnity = "";

        stringUnity += playerStats.getCash() + ",";
        stringUnity += playerStats.getTime() + ",";

        stringUnity = stringUnity.TrimEnd(',');

#if UNITY_ANDROID
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            javaClass.CallStatic("sendFinalStats", stringUnity);
            Debug.Log("stringFinalStats: ");
            Debug.Log(stringUnity);

        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method getFinalStats");
            Debug.Log(ex);
        }
#endif
    }

    public void sendInventory(List<Object> inventory)
    {
        string stringUnity = "";

        foreach(Object o in inventory)
        {
            stringUnity += o.getType() + ",";
            stringUnity += o.getName() + ",";
            stringUnity += o.getAttribute() + ",";
            stringUnity += "/";
        }

        stringUnity = stringUnity.TrimEnd('/');

        stringUnity = stringUnity.TrimEnd(',');

#if UNITY_ANDROID
        try
        {
            AndroidJavaClass javaClass = new AndroidJavaClass("edu.upc.dsa.escaperoomapp.ApiUnity");
            javaClass.CallStatic("sendInventory", stringUnity);
            Debug.Log("stringInventory: ");
            Debug.Log(stringUnity);
        }
        catch (Exception ex)
        {
            Debug.Log("Error Unity, method sendInventory");
            Debug.Log(ex);
        }
#endif
    }
}
