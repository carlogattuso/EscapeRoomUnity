using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDialogueTrigger : MonoBehaviour
{
    public void TriggerDialogue(Dialogue dialogue)
    {
        FindObjectOfType<DoorDialogueManager>().StartDialogue(dialogue);
    }
}
