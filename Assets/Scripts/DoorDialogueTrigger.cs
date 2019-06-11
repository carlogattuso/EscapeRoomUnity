using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DoorDialogueManager>().StartDialogue(dialogue);
    }
}
