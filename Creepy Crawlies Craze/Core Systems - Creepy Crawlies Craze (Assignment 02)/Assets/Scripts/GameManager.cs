using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string[] miniGameScenes; // Array to hold names of mini-game scenes
    private int currentMiniGameIndex = 0; // Tracks the current mini-game
    private bool hasLostAnyMiniGame = false; // Track if the player has lost any mini-game

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndCurrentMiniGame(bool didPlayerWin)
    {
        if (didPlayerWin)
        {
            // Get the next mini-game scene or end screen
            string nextScene = GetNextMiniGame();
            if (!string.IsNullOrEmpty(nextScene))
            {
                SceneManager.LoadScene("Transition Scene");  // Load the transition scene
                StartCoroutine(LoadNextSceneAfterDelay(nextScene));  // Load the next scene after the transition
            }
            else
            {
                // If all mini-games have been played, go to end screen
                SceneManager.LoadScene("End Screen");
            }
        }
        else
        {
            hasLostAnyMiniGame = true; // Mark that the player has lost
            SceneManager.LoadScene("End Screen"); // Load the end screen if the player lost
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(3f); // Wait for the transition scene duration
        SceneManager.LoadScene(sceneName); // Load the next mini-game scene
    }

    public string GetNextMiniGame()
    {
        if (currentMiniGameIndex < miniGameScenes.Length)
        {
            string nextMiniGame = miniGameScenes[currentMiniGameIndex];
            currentMiniGameIndex++;
            return nextMiniGame;
        }
        else
        {
            // If all mini-games have been played, go to end screen
            return "EndScreen";
        }
    }

    public void ResetMiniGameSequence()
    {
        currentMiniGameIndex = 0; // Resets the mini-game loop if necessary
        hasLostAnyMiniGame = false; // Reset loss status
    }

    public bool HasLostAnyMiniGame()
    {
        return hasLostAnyMiniGame;
    }
}
