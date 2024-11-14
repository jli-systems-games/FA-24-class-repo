using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public TextMeshProUGUI actionText; // Reference to the action text element
    public float displayDuration = 1.5f; // Duration for the text to stay visible

    private Coroutine currentTextRoutine;

    // Method to display a message with optional persistence
    public void ShowActionText(string message, bool persistent = false)
    {
        // Stop any existing text display coroutine
        if (currentTextRoutine != null)
        {
            StopCoroutine(currentTextRoutine);
        }

        if (persistent)
        {
            // Display the text indefinitely if persistent
            actionText.text = message;
            actionText.gameObject.SetActive(true);
        }
        else
        {
            // Start a coroutine to show and then hide the text
            currentTextRoutine = StartCoroutine(DisplayTextRoutine(message));
        }
    }

    // Coroutine to handle showing and hiding the text
    private IEnumerator DisplayTextRoutine(string message)
    {
        actionText.text = message;
        actionText.gameObject.SetActive(true);

        yield return new WaitForSeconds(displayDuration);

        actionText.gameObject.SetActive(false);
    }
}
