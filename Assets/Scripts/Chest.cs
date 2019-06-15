using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private string chestParameters;
    private Vector2 position;

    public ClueDialogTrigger clueDialogTrigger;

    public Sprite openSprite;

    private Dialogue chestDialogue = new Dialogue();

    private string action;
    private int value;

    [HideInInspector] public bool opened = false;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        position.x = this.transform.position.x;
        position.y = this.transform.position.y;

        Debug.Log(position.x + "," + position.y);
        //Pediriamos a android la pista en la posicion del cofre
        this.chestParameters = GameManager.instance.getBox(position).getAttribute();

        string[] parametersVector = chestParameters.Split('-');

        this.action = parametersVector[0];
        this.value = Int32.Parse(parametersVector[1]);
    }

    public void OpenChest()
    {
        this.opened = true;
        this.spriteRenderer.sprite = openSprite;

        switch (action)
        {
            case "vida":
                int newLife = GameManager.instance.playerStats.getLife() + this.value;
                FindObjectOfType<Player>().lifeText.text = "Life: " + newLife;
                GameManager.instance.playerStats.setLife(newLife);
                chestDialogue.name = "Chest";
                chestDialogue.sentences = new string[1];
                chestDialogue.sentences[0] = "Has encontrado " + value + " puntos de vida en el cofre!!";
                break;
            case "dinero":
                int newCash = GameManager.instance.playerStats.getCash() + this.value;
                FindObjectOfType<Player>().cashText.text = "Cash: " + newCash;
                GameManager.instance.playerStats.setCash(newCash);
                chestDialogue.name = "Chest";
                chestDialogue.sentences = new string[1];
                chestDialogue.sentences[0] = "Has encontrado " + value + " monedas en el cofre!!";
                break;
        }

        this.clueDialogTrigger.TriggerDialogue(this.chestDialogue);
    }
}
