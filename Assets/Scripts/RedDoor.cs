using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedDoor : MonoBehaviour
{
    public Sprite openSprite;
    public DoorDialogueTrigger doorDialogueTrigger;
    public DialogueTrigger clueErrorTrigger;
    public DialogueTrigger keyErrorTrigger;

    private Vector2 position;
    private string solution;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        position.x = this.transform.position.x;
        position.y = this.transform.position.y;
        Debug.Log(position.x + "," + position.y);
        //Pediriamos a android la pista en la posicion de la puerta
        this.solution = GameManager.instance.getBox(position).getAttribute();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowDialog()
    {
        doorDialogueTrigger.TriggerDialogue();
    }
    public void OpenDoorWithSolution(string solution)
    {
        if (!solution.Equals(this.solution))
        {
            clueErrorTrigger.TriggerDialogue();
            return;
        }

        this.spriteRenderer.sprite = openSprite;
        gameObject.layer = 2;
    }
    public void OpenDoorWithKey()
    {
        if (!GameManager.instance.FindKey("red"))
        {
            keyErrorTrigger.TriggerDialogue();
            return;
        }

        this.spriteRenderer.sprite = openSprite;
        gameObject.layer = 2;
    }
}
