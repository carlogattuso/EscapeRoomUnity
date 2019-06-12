using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueDoor : MonoBehaviour
{
    public Sprite openSprite;

    public DialogueTrigger clueNotFoundTrigger;
    public ClueDialogTrigger clueDialogTrigger;
    public DoorDialogueTrigger doorDialogueTrigger;
    public DialogueTrigger clueErrorTrigger;
    public DialogueTrigger keyErrorTrigger;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    private string question;
    private string answer;
    private string clue;

    private Dialogue doorDialogue = new Dialogue();
    private Vector2 position;
    private string doorParameters;

    private bool clueUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        position.x = this.transform.position.x;
        position.y = this.transform.position.y;

        //Pediriamos a android la pista en la posicion de la puerta
        this.doorParameters = GameManager.instance.getBox(position).getAttribute();

        string[] parametersVector = doorParameters.Split('-');

        this.question = parametersVector[0];
        this.answer = parametersVector[1];
        this.clue = parametersVector[2];

        doorDialogue.name = "Puerta Azul";
        doorDialogue.sentences = new string[3];
        doorDialogue.sentences[0] = "Vaya! Parece que la puerta está cerrada...";
        doorDialogue.sentences[1] = "Puedes intentar abrirla con una llave azul o resolviendo el enigma...";
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
        if (GameManager.instance.FindClue("pistaAzul"))
        {
            Dialogue dialogue = new Dialogue();
            dialogue.name = "Pista Azul";
            dialogue.sentences = new string[1];
            dialogue.sentences[0] = this.clue + "...";

            clueDialogTrigger.TriggerDialogue(dialogue);
            clueUsed = true;
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
            GameManager.instance.DeleteClue("pistaAzul");
        }
    }
    public void OpenDoorWithKey()
    {
        if (!GameManager.instance.FindKey("llaveA"))
        {
            keyErrorTrigger.TriggerDialogue();
            return;
        }

        this.spriteRenderer.sprite = openSprite;
        gameObject.layer = 2;

        if (clueUsed)
        {
            GameManager.instance.DeleteClue("pistaAzul");
        }
    }
}
