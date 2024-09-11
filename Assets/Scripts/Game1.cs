using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game1 : MonoBehaviour
{
    public TMP_Text randomLetterTMP;  // Reference to the TMP Text component
    private char currentLetter;       // The letter the player has to press

    // Start is called before the first frame update
    void Start()
    {
        RandomLetter();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the correct letter key
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(currentLetter.ToString().ToLower()) || Input.GetKeyDown(currentLetter.ToString().ToUpper()))
            {
                Debug.Log("Correct!");  // Player pressed the correct key
                RandomLetter(); // Generate a new letter
            }
            else
            {
                Debug.Log("Wrong key!");
            }
        }
    }

    void RandomLetter()
    {
        currentLetter = (char)Random.Range(65, 91); // Random letter between 'A' (65) and 'Z' (90)
        randomLetterTMP.text = currentLetter.ToString(); // Display the letter using TMP
    }
}
