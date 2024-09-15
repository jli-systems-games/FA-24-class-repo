using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game1 : MonoBehaviour
{
    public TMP_Text randomLetterText;  // Reference to the TMP Text component
    private char currentLetter;        // The letter the player has to press
    private KeyCode currentLetterKeyCode; // The KeyCode corresponding to the letter

    // Start is called before the first frame update
    void Start()
    {
        RandomLetter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // If the player pressed the correct key
            if (Input.GetKeyDown(currentLetterKeyCode))
            {
                Debug.Log("Correct!");  // Player pressed the correct key
                GameManager.instance.OnGameComplete(true);
            }
            else
            {
                // Check for any other key that isn't the correct one
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(key) && key != currentLetterKeyCode)
                    {
                        Debug.Log("Incorrect!");  // Player pressed the wrong key
                        GameManager.instance.OnGameComplete(false);
                        break;
                    }
                }
            }
        }
    }

    // Randomly choose a letter and update the TMP text
    void RandomLetter()
    {
        currentLetter = (char)Random.Range(65, 91); // Random letter between 'A' (65) and 'Z' (90)
        currentLetterKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), currentLetter.ToString()); // Convert char to KeyCode
        randomLetterText.text = currentLetter.ToString(); // Display the letter using TMP
    }
}
