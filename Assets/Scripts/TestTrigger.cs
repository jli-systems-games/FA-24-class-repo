using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrigger : MonoBehaviour
{
    public string sceneToLoad = "Scene 01"; // The name of the scene to transition to
    public bool requiresPlayerTag = true; // Only trigger with objects tagged "Player"

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (!requiresPlayerTag || other.CompareTag("Player"))
        {
            // Call the ScreenFader to transition to the specified scene
            if (ScreenFader.instance != null)
            {
                ScreenFader.instance.TransitionToScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("ScreenFader instance not found. Ensure the ScreenFader exists in the scene.");
            }
        }
    }
}
