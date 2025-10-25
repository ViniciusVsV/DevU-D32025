using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NPCDialogue : MonoBehaviour
{
    public GameObject dialoguePanel;
    public GameObject continueButton;
    public float wordSpeed;
    public Text dialogueText;
    public string[] dialogue;
    public Text NPCNameText;
    public string NPCName;
    public GameObject interactMenu;
    private int index;
    public bool playerInRange;
    private Coroutine isTyping;

    void Update()
    {
        if (playerInRange)
            interactMenu.SetActive(true);
        else
            interactMenu.SetActive(false);
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                NPCNameText.text = NPCName;
                isTyping = StartCoroutine(Typing());
            }
        }
        
        if(dialogueText.text == dialogue[index])
        {
            continueButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        if (isTyping != null)
        {
            StopCoroutine(isTyping);
        }
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (isTyping != null)
        {
            StopCoroutine(isTyping);
        }
        
        continueButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            isTyping = StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            zeroText();
        }
    }
}
