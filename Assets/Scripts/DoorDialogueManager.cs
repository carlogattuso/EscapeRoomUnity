using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorDialogueManager : MonoBehaviour
{
    public Animator animator;

    public Text nameText;
    public Text dialogueText;
    public Text continueButtonText;
    public Button keyButton;
    public Button clueButton;
    public InputField inputField;

    private float letterTime = 0.02f;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        GameManager.instance.doingSetup = true;

        keyButton.interactable = false;
        clueButton.interactable = false;
        continueButtonText.text = "Continue >>";
        inputField.text = "";

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else if(sentences.Count == 1)
        {
            continueButtonText.text = "Exit";
            keyButton.interactable = true;
            clueButton.interactable = true;
        }
        else
        {

        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(letterTime);
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        GameManager.instance.doingSetup = false;
    }
}
