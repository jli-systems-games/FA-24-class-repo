using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string[] miniGameScenes; // Array to hold names of mini-game scenes
    public AudioClip backgroundMusic; // Assign background music in the Inspector
    private AudioSource audioSource; // Reference to the AudioSource component
    private int currentMiniGameIndex = 0; // Tracks the current mini-game
    private int lossCount = 0; // Track the number of losses
    private const int maxLossesAllowed = 1; // Set the maximum number of losses allowed before game over


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            // Setup AudioSource for background music
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = backgroundMusic;
            audioSource.loop = true; // Loop the music
            audioSource.playOnAwake = true; // Play immediately
            audioSource.volume = 0.8f; // Adjust volume as needed
            audioSource.Play(); // Start playing the music
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
                // Proceed to the next mini-game or end screen
                string nextMiniGame = GetNextMiniGame();
                SceneManager.LoadScene("Transition Scene");
                StartCoroutine(LoadNextSceneAfterDelay(nextMiniGame));
            }
        }
    }

    private IEnumerator LoadNextSceneAfterDelay(string sceneName)
    {
        yield return new WaitForSeconds(2.5f); // Adjust to match your transition duration
        SceneManager.LoadScene(sceneName); // Load the next mini-game scene or end screen
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
