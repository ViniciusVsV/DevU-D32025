using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    [TextArea(2, 5)] public string[] dialogueLines;
    private bool playerInRange;

    public GameObject interactMenu;

    void Update()
    {
        //interactMenu.SetActive(playerInRange);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            DialogueManager.Instance.StartDialogue(dialogueLines, npcName);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.EndDialogue();
            playerInRange = false;
        }
    }
}
