using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public GameObject dialoguePanel;
    public Text dialogueText;
    public Text npcNameText;
    public GameObject continueButton;

    public float wordSpeed = 0.05f;
    private string[] currentDialogue;
    private int index;
    private Coroutine typingCoroutine;
    private bool isTyping;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartDialogue(string[] dialogue, string npcName)
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue[index];
            isTyping = false;
            continueButton.SetActive(true);
            return;
        }

        currentDialogue = dialogue;
        index = 0;
        npcNameText.text = npcName;
        dialoguePanel.SetActive(true);
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        continueButton.SetActive(false);
        dialogueText.text = "";

        foreach (char c in currentDialogue[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
        continueButton.SetActive(true);
    }

    public void NextLine()
    {
        if (isTyping) return;

        continueButton.SetActive(false);

        if (index < currentDialogue.Length - 1)
        {
            index++;
            typingCoroutine = StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }
}
