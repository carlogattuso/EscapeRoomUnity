﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedDoor : MonoBehaviour
{
    public Sprite openSprite;

    public DialogueTrigger clueNotFoundTrigger;
    public ClueDialogTrigger clueDialogTrigger;
    public DoorDialogueTrigger doorDialogueTrigger;
    public DialogueTrigger clueErrorTrigger;
    public DialogueTrigger keyErrorTrigger;

    private Dialogue doorDialogue = new Dialogue();
    private Vector2 position;
    private string doorParameters;

    private string question;
    private string answer;
    private string clue;

    private bool clueUsed = false;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        position.x = this.transform.position.x;
        position.y = this.transform.position.y;

        //Pediriamos a android la pista en la posicion de la puerta
        this.doorParameters = GameManager.instance.getBox(position).getAttribute();

        string [] parametersVector = doorParameters.Split('-');

        this.question = parametersVector[0];
        this.answer = parametersVector[1];
        this.clue = parametersVector[2];

        doorDialogue.name = "Puerta Roja";
        doorDialogue.sentences = new string[3];
        doorDialogue.sentences[0] = "Vaya! Parece que la puerta está cerrada...";
        doorDialogue.sentences[1] = "Puedes intentar abrirla con una llave roja o resolviendo el enigma...";
        doorDialogue.sentences[2] = this.question;

        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDialog()
    {
        doorDialogueTrigger.TriggerDialogue(doorDialogue);
    }
    public void ShowClueDialog()
    {
        if (GameManager.instance.FindClue("pistaRoja"))
        {
            Dialogue dialogue = new Dialogue();
            dialogue.name = "Pista Roja";
            dialogue.sentences = new string[1];
            dialogue.sentences[0] = this.clue + "...";

            clueDialogTrigger.TriggerDialogue(dialogue);
            clueUsed = false;
            return;
        }

        clueNotFoundTrigger.TriggerDialogue();
    }
    public void OpenDoorWithSolution(string solution)
    {
        if (!solution.Equals(this.answer))
        {
            clueErrorTrigger.TriggerDialogue();
            return;
        }

        this.spriteRenderer.sprite = openSprite;
        gameObject.layer = 2;

        if (clueUsed)
        {
            GameManager.instance.DeleteClue("pistaRoja");
        }
    }
    public void OpenDoorWithKey()
    {
        if (!GameManager.instance.FindKey("llaveR"))
        {
            keyErrorTrigger.TriggerDialogue();
            return;
        }

        this.spriteRenderer.sprite = openSprite;
        gameObject.layer = 2;
        

        if (clueUsed)
        {
            GameManager.instance.DeleteClue("pistaRoja");
        }
    }
}
