using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueDialogTrigger : MonoBehaviour
{
    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
