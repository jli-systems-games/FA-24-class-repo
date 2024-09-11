using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string[] miniGameScenes; // Array to hold names of mini-game scenes
    private int currentMiniGameIndex = 0; // Tracks the current mini-game
    private int lossCount = 0; // Track the number of losses
    private const int maxLossesAllowed = 1; // Set the maximum number of losses allowed before game over

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
            lossCount++; // Increment the loss count
            if (lossCount > maxLossesAllowed)
            {
                SceneManager.LoadScene("End Screen"); // Load the end screen if too many losses
            }
            else
            {
                // Restart the current mini-game or proceed to the next one
                SceneManager.LoadScene("Transition Scene");  // Load the transition scene
                StartCoroutine(LoadNextSceneAfterDelay(GetCurrentMiniGame()));  // Reload the current mini-game
            }
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
            return "End Screen";
        }
    }

    public string GetCurrentMiniGame()
    {
        if (currentMiniGameIndex < miniGameScenes.Length)
        {
            return miniGameScenes[currentMiniGameIndex];
        }
        else
        {
            // If all mini-games have been played, go to end screen
            return "End Screen";
        }
    }

    public void ResetMiniGameSequence()
    {
        currentMiniGameIndex = 0; // Resets the mini-game loop if necessary
        lossCount = 0; // Reset loss count
    }

    public int GetLossCount()
    {
        return lossCount;
    }
}
