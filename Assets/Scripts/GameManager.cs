using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string[] gameScenes; // List of game scenes to cycle through
    private string[] allScenes; // All scenes in build settings

    private int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize allScenes and currentSceneIndex
        allScenes = new string[SceneManager.sceneCountInBuildSettings];
        for (int i = 0; i < allScenes.Length; i++)
        {
            allScenes[i] = SceneUtility.GetScenePathByBuildIndex(i).Substring(SceneUtility.GetScenePathByBuildIndex(i).LastIndexOf("/") + 1).Replace(".unity", "");
        }

        // Find the current scene index in the gameScenes list
        currentSceneIndex = System.Array.IndexOf(gameScenes, SceneManager.GetActiveScene().name);
        if (currentSceneIndex < 0) // If the current scene is not in the gameScenes list, default to 0
        {
            currentSceneIndex = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Go to the previous scene
            CycleScene(-1);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Go to the next scene
            CycleScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SelectionScreen");
        }


    }

    private void CycleScene(int direction)
    {
        currentSceneIndex += direction;

        // Ensure index wraps around
        if (currentSceneIndex < 0)
            currentSceneIndex = gameScenes.Length - 1;
        else if (currentSceneIndex >= gameScenes.Length)
            currentSceneIndex = 0;

        // Load the scene by name
        SceneManager.LoadScene(gameScenes[currentSceneIndex]);
    }
}