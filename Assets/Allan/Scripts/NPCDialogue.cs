using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    [TextArea(2, 5)] public string[] dialogueLines;
    private bool playerInRange;

    public GameObject interactMenu;

    void Update()
    {
        interactMenu.SetActive(playerInRange);

        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.StartDialogue(dialogueLines, npcName);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}
