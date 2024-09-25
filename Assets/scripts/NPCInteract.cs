using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCInteract : MonoBehaviour
{
    public GameObject interactPrompt;
    public GameObject dialogueBackground;
    public TextMeshProUGUI dialogueNPC;
    public string[] dialogueLines;

    private int currentDialogueIndex = 0;
    private bool playerInRange = false;
    private bool dialogueActive = false;

    void Start()
    {
        interactPrompt.SetActive(false);
        dialogueNPC.gameObject.SetActive(false);
        dialogueBackground.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E) && !dialogueActive)
        {
            StartDialogue();
        }

        if (dialogueActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextDialogue();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(true);
            playerInRange = true;
            currentDialogueIndex = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactPrompt.SetActive(false);
            playerInRange = false;
            EndDialogue();
            currentDialogueIndex = 0;
        }
    }

    void StartDialogue()
    {
        dialogueActive = true;
        interactPrompt.SetActive(false);
        dialogueNPC.gameObject.SetActive(true);
        dialogueBackground.SetActive(true);
        currentDialogueIndex = 0;
        dialogueNPC.text = dialogueLines[currentDialogueIndex];
    }

    void DisplayNextDialogue()
    {
        currentDialogueIndex++;
        if (currentDialogueIndex < dialogueLines.Length)
        {
            dialogueNPC.text = dialogueLines[currentDialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialogueNPC.gameObject.SetActive(false);
        dialogueBackground.SetActive(false);
        currentDialogueIndex = 0;
    }
}
