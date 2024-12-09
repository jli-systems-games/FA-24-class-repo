using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueUI;
    public DialogueNodes startingNode;
    public GameObject player;
    public FirstPersonMovement movementScript;

    private DialogueNodes currentNode;

    void Start()
    {
        dialogueUI.SetActive(false);
        movementScript.enabled = false;
        StartDialogue(startingNode);
    }

    public void StartDialogue(DialogueNodes startingNode)
    {
        currentNode = startingNode;
        dialogueUI.SetActive(true);
        ShowCurrentNode();
    }

    public void ShowNextNode()
    {
        if (currentNode != null && currentNode.nextNode != null)
        {
            currentNode = currentNode.nextNode;
            ShowCurrentNode();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowCurrentNode()
    {
        if (currentNode != null)
        {
            dialogueText.text = currentNode.dialogueText;
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        movementScript.enabled = true;
    }

    void Update()
    {
        if (dialogueUI.activeSelf && Input.GetMouseButtonDown(0))
        {
            ShowNextNode();
        }
    }
}