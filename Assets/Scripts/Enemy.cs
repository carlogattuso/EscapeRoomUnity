﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
public class Enemy : MovingObject
{
    int MinDist = 4;

    //Health system of the enemy
    public int playerDamage;                            
    public int enemyLife;

    private Text enemiesSlainedText;

    private Animator animator;                          //Variable of type Animator to store a reference to the enemy's Animator component.
    private Transform target;                           //Transform to attempt to move toward each turn.
    private bool skipMove;                              //Boolean to determine whether or not enemy should skip a turn or move this turn.

    private bool playerMoving;
    private Vector2 lastMove;

    private bool attacking;
    public float attackTime;
    private float attackTimeCounter;

    private bool movable=false;

    //Start overrides the virtual Start function of the base class.
    protected override void Start()
    {
        //Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
        //This allows the GameManager to issue movement commands.
        GameManager.instance.AddEnemyToList(this);

        //Get and store a reference to the attached Animator component.
        animator = GetComponent<Animator>();

        //Find the Player GameObject using it's tag and store a reference to its transform component.
        target = GameObject.FindGameObjectWithTag("Player").transform;

        //Call the start function of our base class MovingObject.
        base.Start();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position,target.position) >= MinDist)
        {
            movable = false;
            playerMoving = false;
            animator.SetBool("PlayerMoving", playerMoving);
        }
        else
        {
            movable = true;
        }
    }

    //Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns.
    //See comments in MovingObject for more on how base AttemptMove function works.
    protected override void AttemptMove<T>(ref int xDir, ref int yDir)
    {
        //Check if skipMove is true, if so set it to false and skip this turn.
        if (skipMove)
        {
            skipMove = false;
            return;

        }

        //Call the AttemptMove function from MovingObject.
        base.AttemptMove<T>(ref xDir,ref yDir);

        //Now that Enemy has moved, set skipMove to true to skip next move.
        skipMove = true;
    }

    //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
    public void MoveEnemy()
    {
        //Declare variables for X and Y axis move directions, these range from -1 to 1.
        //These values allow us to choose between the cardinal directions: up, down, left and right.
        int xDir = 0;
        int yDir = 0;

        //If the difference in positions is approximately zero (Epsilon) do the following:
        if (Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)

            //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            yDir = target.position.y > transform.position.y ? 1 : -1;

        //If the difference in positions is not approximately zero (Epsilon) do the following:
        else
            //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            xDir = target.position.x > transform.position.x ? 1 : -1;

        if (!attacking)
        {
            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player
            AttemptMove<Player>(ref xDir, ref yDir);
            playerMoving = true;
            lastMove = new Vector2(xDir, yDir);
        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }
        else
        {
            attacking = false;
            animator.SetBool("Attack", false);
        }

        animator.SetFloat("MoveX",xDir);
        animator.SetFloat("MoveY", yDir);
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }

    //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
    //and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
    protected override void OnCantMove<T>(T component)
    {
        //Declare hitPlayer and set it to equal the encountered component.
        Player hitPlayer = component as Player;

        attacking = true;
        attackTimeCounter = attackTime;

        //Set the attack trigger of animator to trigger Enemy attack animation.
        animator.SetBool("Attack", true);

        hitPlayer.LoseLife(playerDamage);
    }

    public void DamageEnemy(int damage)
    {
        this.enemyLife -= damage;
        ChechIfDisable();
        Debug.Log("enemyLife: " + enemyLife);
    }

    public void ChechIfDisable()
    {
        if (this.enemyLife <= 0) {
            GameManager.instance.playerStats.setEnemiesSlained(GameManager.instance.playerStats.getEnemiesSlained()+1);
            enemiesSlainedText = GameObject.Find("SlainedEnemies").GetComponent<Text>();
            enemiesSlainedText.text = "Enemies Slained: "+ GameManager.instance.playerStats.getEnemiesSlained();
            this.gameObject.SetActive(false);
            GameManager.instance.RemoveEnemyofList(this);
            StopAllCoroutines();
        }
    }

    public bool CheckIfMovable()
    {
        return this.movable;
    }
}