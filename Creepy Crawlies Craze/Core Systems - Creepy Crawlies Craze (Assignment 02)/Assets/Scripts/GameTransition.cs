using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameTransition : MonoBehaviour
{
    public TMP_Text transitionMessage;
    public TMP_Text countdownText;
    private float countdownTime = 2.5f; // 2.5-second countdown
    public Animator transition;
    private static GameTransition instance; // To ensure it's persistent

    void Start()
    {
        // Set the transition message
        transitionMessage.text = "Get Ready to Scuttle Away!";
        StartCoroutine(CountdownToNextGame());
    }

    IEnumerator CountdownToNextGame()
    {
        // Countdown loop
        while (countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString("F0");
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        // Ensure the countdown text shows zero
        countdownText.text = "0";

        // Start the circlewipe animation
        transition.ResetTrigger("Start"); // Reset the trigger to ensure it's ready to be set again
        transition.SetTrigger("Start"); // Set the trigger to play the animation

        // Wait for the circlewipe animation to complete
        yield return new WaitForSeconds(1f);

        // Log and load the next game
        Debug.Log("Loading next game.");
        LoadNextGame();
    }

    void LoadNextGame()
    {
        // Retrieve the next game scene or end screen
        string nextGameScene = GameManager.instance.GetNextMiniGame();

        if (!string.IsNullOrEmpty(nextGameScene))
        {
            Debug.Log($"Loading scene: {nextGameScene}"); // Debug log to check scene name
            // Load the next mini-game scene
            SceneManager.LoadScene(nextGameScene);
        }
        else
        {
            Debug.Log("No next game scene found. Loading End Screen.");
            // No next game scene, so go to end screen
            SceneManager.LoadScene("End Screen");
        }
    }
}
