using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource; // The AudioSource component that will play the music
    public AudioClip backgroundMusic; // The music clip to play across scenes

    private void Awake()
    {
        // Check if an instance of AudioManager already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make this object persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate AudioManager objects
            return;
        }

        // Initialize the AudioSource and start playing the music if not already playing
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.Play();
    }

    // Optionally, you can add a method to stop the music when needed
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
