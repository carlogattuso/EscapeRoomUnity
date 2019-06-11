﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


using System.Collections.Generic;       //Allows us to use Lists. 
using System;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
    public float turnDelay = 0.1f;                          //Delay between each Player turn.
    public int playerFoodPoints = 10000;                      //Starting value for Player food points.
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //User variables
    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public List<Object> inventory = new List<Object>();
    [HideInInspector] public List<Map> maps = new List<Map>();

    //Counter variables
    float secondsCount;
    float minuteCount;
    float hourCount;

    //Connection with android to start game
    APIAndroid APIAndroid = new APIAndroid();

    [HideInInspector] public bool playersTurn = true;       //Boolean to check if it's players turn, hidden in inspector but public.
   
    //UI image
    private GameObject LevelImage;
    private GameObject Life;
    private GameObject SlainedEnemies;
    private GameObject TimeObject;
    private GameObject Cash;
    private Text cashText;
    private Text lifeText;
    private Text slainedEnemiesText;
    private Text LevelText;
    private Text timerText;

    [HideInInspector] public bool doingSetup;
    private BoardManager boardScript;                       //Store a reference to our BoardManager which will set up the level.
    private List<Enemy> enemies;                          //List of all Enemy units, used to issue them move commands.
    private bool enemiesMoving;                             //Boolean to check if enemies are moving.
    private bool playerMoving;

    private WelcomeDialogue dialogueTrigger;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Assign enemies to a new List of Enemy objects.
        enemies = new List<Enemy>();

        //Get a component reference to the attached BoardManager script
        boardScript = GetComponent<BoardManager>();

        //Pedimos los mapas, las estadísticas iniciales y el inventario del usuario
        this.maps = APIAndroid.getMaps();
        this.inventory = APIAndroid.getInventory();
        this.playerStats = APIAndroid.getPlayerStats();

        Debug.Log("Maps: " + maps.Count);
        Debug.Log("Inventory: " + inventory.Count);
        Debug.Log("PlayerStats--> Level:" + this.playerStats.getLevel() + "/Life:" + this.playerStats.getLife() +
            "/Cash:" + this.playerStats.getLevel() + "/EnemiesSlained:" + this.playerStats.getEnemiesSlained()
            + "/Time:" + this.playerStats.getTime());

        string[] timeValues = this.playerStats.getTime().Split(':');

        this.hourCount = Int32.Parse(timeValues[0]);
        this.minuteCount = Int32.Parse(timeValues[1]);
        this.secondsCount = Int32.Parse(timeValues[2]);

        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static public void CallbackInitialization()
    {
        //register the callback to be called everytime the scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    //This is called each time a scene is loaded.
    static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        instance.InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        doingSetup = true;

        TimeObject = GameObject.Find("Time");
        SlainedEnemies = GameObject.Find("SlainedEnemies");
        Cash = GameObject.Find("Cash");
        Life = GameObject.Find("Life");

        TimeObject.SetActive(false);
        SlainedEnemies.SetActive(false);
        Cash.SetActive(false);
        Life.SetActive(false);

        LevelImage = GameObject.Find("LevelImage");
        LevelText = GameObject.Find("LevelText").GetComponent<Text>();

        Debug.Log("NextLevel");
        Debug.Log("Maps: " + maps.Count);
        Debug.Log("Inventory: " + inventory.Count);
        Debug.Log("PlayerStats--> Level:" + this.playerStats.getLevel() + "/Life:" + this.playerStats.getLife() +
            "/Cash:" + this.playerStats.getLevel() + "/EnemiesSlained:" + this.playerStats.getEnemiesSlained()
            + "/Time:" + this.playerStats.getTime());

        LevelText.text = "Floor " + this.playerStats.getLevel();
        LevelImage.SetActive(true);

        Invoke("HideLevelImage", levelStartDelay);

        //Clear any Enemy objects in our List to prepare for next level.
        enemies.Clear();

        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene(this.maps[this.playerStats.getLevel() - 1].getLevelString(), this.maps[this.playerStats.getLevel() - 1].getDimension());
    }

    private void HideLevelImage()
    {
        LevelImage.SetActive(false);

        TimeObject.SetActive(true);
        SlainedEnemies.SetActive(true);
        Cash.SetActive(true);
        Life.SetActive(true);

        timerText = GameObject.Find("Time").GetComponent<Text>();
        lifeText = GameObject.Find("Life").GetComponent<Text>();
        cashText = GameObject.Find("Cash").GetComponent<Text>();
        slainedEnemiesText = GameObject.Find("SlainedEnemies").GetComponent<Text>();

        lifeText.text = "Life: " + this.playerStats.getLife();
        cashText.text = "Cash: " + this.playerStats.getCash();
        slainedEnemiesText.text = "Slained Enemies: " + this.playerStats.getEnemiesSlained();
        timerText.text = "Time\n" + hourCount + "h:" + minuteCount.ToString("00") + "m:" + ((int)secondsCount).ToString("00") + "s";

        doingSetup = false;
    }

    //Update is called every frame.
    void Update()
    {
        //Check that playersTurn or enemiesMoving or doingSetup are not currently true.
        if (enemiesMoving || playersTurn || doingSetup)
        {
            if (doingSetup)
            {
                return;
            }
            else
            {
                this.UpdateTimerUI();
                return;
            }
        }

        //Start moving enemies.
        StartCoroutine(MoveEnemies());
    }

    //Call this to add the passed in Enemy to the List of Enemy objects.
    public void AddEnemyToList(Enemy script)
    {
        //Add Enemy to List enemies.
        enemies.Add(script);
    }

    public void RemoveEnemyofList(Enemy script)
    {
        //Remove enemy of the list
        enemies.Remove(script);
    }

    //GameOver is called when the player reaches 0 food points
    public void GameOver()
    {
        LevelText.text = "After " + this.playerStats.getLevel() + " days, you starved.";
        LevelImage.SetActive(true);
        //Disable this GameManager.
        enabled = false;
    }

    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies()
    {
        bool someoneHasMoved = false;
        //While enemiesMoving is true player is unable to move.
        enemiesMoving = true;

        //Wait for turnDelay seconds, defaults to .1 (100 ms).
        yield return new WaitForSeconds(turnDelay);

        //If there are no enemies spawned (IE in first level):
        if (enemies.Count == 0)
        {
            someoneHasMoved = true;
            //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
            yield return new WaitForSeconds(turnDelay);
        }

        //Loop through List of Enemy objects.
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].CheckIfMovable())
            {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                enemies[i].MoveEnemy();

                someoneHasMoved = true;

                //Wait for Enemy's moveTime before moving next Enemy, 
                yield return new WaitForSeconds(enemies[i].moveTime);
            }
        }

        if (!someoneHasMoved)
        {
            yield return new WaitForSeconds(turnDelay);
        }

        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;

        //Enemies are done moving, set enemiesMoving to false.
        enemiesMoving = false;
    }

    public bool FindKey(string color)
    {
        foreach(Object o in this.inventory)
        {
            if (o.getType().Equals("llave")&&o.getAttribute().Equals(color))
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateTimerUI()
    {
        //set timer UI

        secondsCount += Time.deltaTime;
        timerText.text = "Time\n" + hourCount + "h:" + minuteCount.ToString("00") + "m:" + ((int)secondsCount).ToString("00") + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }

    public Box getBox(Vector2 position)
    {
        if (this.playerStats.getLevel() == 2)
        {
            Debug.Log(this.maps[this.playerStats.getLevel() - 1].getBoxes().Count);
        }

        Box box = null;

        foreach(Box b in this.maps[this.playerStats.getLevel()-1].getBoxes())
        {
            if (b.getPosition() == position)
            {
                box = new Box((int) b.getPosition().x, (int) b.getPosition().y, b.getAttribute());
            }
        }

        return box;
    }

    public string getWeapon()
    {
        string weapon = null;
        foreach (Object o in this.inventory)
        {
            if (o.getType().Equals("weapon"))
            {
                weapon = o.getAttribute();
            }
        }
        return weapon;
    }

    public string getShield()
    {
        string shield = null;
        foreach (Object o in this.inventory)
        {
            if (o.getType().Equals("shield"))
            {
                shield = o.getAttribute();
            }
        }
        return shield;
    }

    public void SetTime()
    {
        this.playerStats.setTime(hourCount + ":" + minuteCount.ToString("00") + ":" + ((int)secondsCount).ToString("00"));

    }
}