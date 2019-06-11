using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeDialogue : MonoBehaviour
{
    public DialogueTrigger dialogueTrigger;

    public void launchDialogue()
    {
        this.dialogueTrigger.TriggerDialogue();
    }
}
