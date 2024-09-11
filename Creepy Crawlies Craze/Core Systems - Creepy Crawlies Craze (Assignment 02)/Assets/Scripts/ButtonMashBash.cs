using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonMashBash : MonoBehaviour
{
    public TMP_Text pressCountText;  // UI Text to display button press count
    public TMP_Text timerText;       // UI Text to display timer
    public float timeLimit = 15f;    // Time limit for the mini-game
    public int pressThreshold = 5;   // How many presses are required to win

    public Button button1;  // Reference to UI Button 1
    public Button button2;  // Reference to UI Button 2
    public Button button3;  // Reference to UI Button 3

    public TMP_Text messageText; // Reference to the message text component
    public float messageDisplayTime = 2.5f; // Time to display the result message

    private int pressCount = 0;      // How many times the selected button has been pressed
    private bool gameStarted = false;
    private Button correctButton;    // The correct button to press

    void Start()
    {
        // Setup button click listeners
        button1.onClick.AddListener(() => OnButtonPressed(button1));
        button2.onClick.AddListener(() => OnButtonPressed(button2));
        button3.onClick.AddListener(() => OnButtonPressed(button3));

        StartCoroutine(StartGameCountdown());
    }

    void OnButtonPressed(Button pressedButton)
    {
        if (!gameStarted) return;

        // Check if the pressed button is the correct one
        if (pressedButton == correctButton)
        {
            pressCount++;
            pressCountText.text = "Press Count: " + pressCount;

            if (pressCount >= pressThreshold)
            {
                EndMiniGame(true);
            }
        }
    }

    IEnumerator StartGameCountdown()
    {
        // Choose a correct button randomly
        int correctButtonIndex = Random.Range(1, 4); // Random number between 1 and 3

        // Set the correct button based on the random number
        switch (correctButtonIndex)
        {
            case 1:
                correctButton = button1;
                break;
            case 2:
                correctButton = button2;
                break;
            case 3:
                correctButton = button3;
                break;
        }

        pressCountText.text = "Press Count: 0"; // Initialize press count display

        gameStarted = true;
        float remainingTime = timeLimit;

        // Timer countdown loop
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.Ceil(remainingTime); // Update timer display
            yield return null;  // Wait for next frame
        }

        // If the time runs out and player hasn't pressed enough, they lose
        StartCoroutine(ShowResult("Time's Up! You Lose!")); // End game if time runs out
    }

    IEnumerator ShowResult(string message)
    {
        // Display the result message
        messageText.text = message;

        // Wait for the specified amount of time
        yield return new WaitForSeconds(messageDisplayTime);

        // End the current mini-game
        EndMiniGame(message == "You Win!"); // End game with win or loss
    }

    void EndMiniGame(bool didPlayerWin)
    {
        gameStarted = false;
        GameManager.instance.EndCurrentMiniGame(didPlayerWin);
    }
}
