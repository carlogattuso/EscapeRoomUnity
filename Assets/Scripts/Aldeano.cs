using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aldeano : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private string villagerParameters;
    private Vector2 position;

    public ClueDialogTrigger clueDialogTrigger;

    private Dialogue villagerDialogue = new Dialogue();

    private string villagerName;
    private string [] conversation;

    // Start is called before the first frame update
    void Start()
    {
        position.x = this.transform.position.x;
        position.y = this.transform.position.y;

        //Pediriamos a android la pista en la posicion del cofre
        this.villagerParameters = GameManager.instance.getBox(position).getAttribute();

        string[] parametersVector = villagerParameters.Split('-');

        this.villagerName = parametersVector[0];

        this.conversation = new string[parametersVector.Length-1];

        for(int i = 1; i < parametersVector.Length; i++)
        {
            this.conversation[i-1] = parametersVector[i];
        }

        villagerDialogue.name = this.villagerName;
        villagerDialogue.sentences = new string[this.conversation.Length];
        villagerDialogue.sentences = this.conversation;
    }

    public void LaunchDialog()
    {
        this.clueDialogTrigger.TriggerDialogue(villagerDialogue);
    }
}