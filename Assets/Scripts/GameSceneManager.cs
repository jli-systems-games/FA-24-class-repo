using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{

    public AudioSource buttonSound;  // Reference to AudioSource for button sound

    // Method to load back to the Customize Scene
    public void BackToCustomization()
    {
        // Play the sound effect if assigned
        if (buttonSound != null)
        {
            buttonSound.Play();
        }

        // Load the Customize Scene after a slight delay to allow the sound to play
        StartCoroutine(LoadSceneAfterSound());
    }

    private IEnumerator LoadSceneAfterSound()
    {
        // Wait until the sound finishes playing, if there is a sound assigned
        if (buttonSound != null)
        {
            yield return new WaitForSeconds(buttonSound.clip.length);
        }

        // Load the Customize Scene
        SceneManager.LoadScene("Customize Scene"); // Replace with the actual name of your Customize Scene
    }
}
