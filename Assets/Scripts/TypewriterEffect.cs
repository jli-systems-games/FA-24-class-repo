using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;
    public float currentTypingSpeed;

    private bool isTyping = false;
    private bool finishedTyping = false;

    public string[] dialogueLines;

    private int currentLineIndex = 0;

    private Coroutine typingCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        dialogueLines = new string[]
        {
            "Welcome to Fidget Laboratories!",
            "We have a selection of curated experiences to satisfy your fidget needs.",
            "You can press Q and E to switch between each experience.",
            "Select your first experience with the buttons below."
        };

        currentTypingSpeed = typingSpeed;
        StartNextDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for Enter press
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                currentTypingSpeed = typingSpeed / 10;
            }
            else if (finishedTyping)
            {
                StartNextDialogue();
            }
        }
    }

    void StartNextDialogue()
    {
        if (currentLineIndex < dialogueLines.Length)
        {
            // Reset the state and start typing the next line
            finishedTyping = false;
            currentTypingSpeed = typingSpeed;

            if (typingCoroutine != null)
            {
                StopCoroutine(typingCoroutine);
            }

            typingCoroutine = StartCoroutine(TypeText(dialogueLines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            dialogueText.text = "Select your first experience with the buttons below.";
        }
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentTypingSpeed);
        }

        isTyping = false;
        finishedTyping = true;
    }
}