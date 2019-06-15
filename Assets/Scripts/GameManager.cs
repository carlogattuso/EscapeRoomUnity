using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


using System.Collections.Generic;       //Allows us to use Lists. 
using System;

public class GameManager : MonoBehaviour
{
    public float levelStartDelay = 2f;                      //Time to wait before starting level, in seconds.
    public float turnDelay = 0.02f;                          //Delay between each Player turn.
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.

    //User variables
    [HideInInspector] public PlayerStats playerStats;
    [HideInInspector] public List<Object> inventory = new List<Object>();
    [HideInInspector] public Map map;

    [HideInInspector] public int damageToPlayer = 20;
    [HideInInspector] public int damageToEnemy = 10;

    [HideInInspector] public bool finalBossKilled = false;

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
        this.playerStats = APIAndroid.getPlayerStats();
        this.inventory = APIAndroid.getInventory();

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

        this.map = APIAndroid.getMap(this.playerStats.getLevel());

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

        LevelText.text = "Floor " + this.playerStats.getLevel();
        LevelImage.SetActive(true);

        Invoke("HideLevelImage", levelStartDelay);

        //Clear any Enemy objects in our List to prepare for next level.
        enemies.Clear();

        //Call the SetupScene function of the BoardManager script, pass it current level number.
        boardScript.SetupScene(this.map.getLevelString(), this.map.getDimension());
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
        doingSetup = true;

        LevelText.text = "Game Over";
        LevelImage.SetActive(true);

        TimeObject.SetActive(false);
        SlainedEnemies.SetActive(false);
        Cash.SetActive(false);
        Life.SetActive(false);

        //Disable this GameManager.
        enabled = false;

        Application.Quit();
    }

    public void SendPlayerStats()
    {
        string finalDeNivel = APIAndroid.sendPlayerStats(this.playerStats);
        Debug.Log("Final De Nivel: "+finalDeNivel);
    }

    public void SendInventory()
    {
        string finalDeNivel = APIAndroid.sendInventory(this.inventory);
        Debug.Log("Final De Nivel : " + finalDeNivel);
    }

    //Coroutine to move enemies in sequence.
    IEnumerator MoveEnemies()
    {
        //While enemiesMoving is true player is unable to move.
        enemiesMoving = true;

        //Wait for turnDelay seconds, defaults to .1 (100 ms).
        yield return new WaitForSeconds(turnDelay);

        //If there are no enemies spawned (IE in first level):
        if (enemies.Count == 0)
        {
            //Wait for turnDelay seconds between moves, replaces delay caused by enemies moving when there are none.
            yield return new WaitForSeconds(turnDelay);
        }

        //Loop through List of Enemy objects.
        for (int i = 0; i < enemies.Count; i++)
        {
                //Call the MoveEnemy function of Enemy at index i in the enemies List.
                enemies[i].MoveEnemy();

                //Wait for Enemy's moveTime before moving next Enemy, 
                yield return new WaitForSeconds(enemies[i].moveTime);
        }

        //Once Enemies are done moving, set playersTurn to true so player can move.
        playersTurn = true;

        //Enemies are done moving, set enemiesMoving to false.
        enemiesMoving = false;
    }

    public bool FindKey(string llave)
    {
        foreach(Object o in this.inventory)
        {
            if (o.getType().Equals("llave")&&o.getName().Equals(llave))
            {
                int count = Int32.Parse(o.getAttribute()) - 1;
                o.setAttribute(count.ToString());
                if(count == 0)
                {
                    this.inventory.Remove(o);
                }
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
        Box box = null;

        foreach(Box b in this.map.getBoxes())
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
        return this.playerStats.getWeapon();
    }

    public string getShield()
    {
        return this.playerStats.getShield();
    }

    public void SetTime()
    {
        this.playerStats.setTime(hourCount + ":" + minuteCount.ToString("00") + ":" + ((int)secondsCount).ToString("00"));

    }

    public bool FindClue(string name)
    {
        foreach (Object o in this.inventory)
        {
            if (o.getType().Equals("pista") && o.getName().Equals(name))
            {
                return true;
            }
        }
        return false;
    }

    public void DeleteClue(string name)
    {
        Object objeto = null;
        bool empty = false;
        foreach (Object o in this.inventory)
        {
            if (o.getType().Equals("pista") && o.getName().Equals(name))
            {
                int count = Int32.Parse(o.getAttribute())-1;
                o.setAttribute(count.ToString());
                if (count == 0)
                {
                    objeto = o;
                    empty = true;
                }
            }
        }
        if (empty)
        {
            this.inventory.Remove(objeto);
        }
    }
}