using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;  // Assign your AudioSource here
    public float fadeDuration = 5f;  // Duration of the fade-in in seconds
    public float maxVolume = 0.5f;   // Maximum volume (set to 0.7)

    // Different audio clips for actions
    public AudioClip feedSound; // Sound for feeding
    public AudioClip playSound; // Sound for playing
    public AudioClip cleanSound; // Sound for cleaning

    void Start()
    {
        // Ensure the volume starts at 0 and the music is playing
        audioSource.volume = 0f;
        audioSource.Play();

        // Start the fade-in
        StartCoroutine(FadeInAudio());
    }

    private System.Collections.IEnumerator FadeInAudio()
    {
        float currentTime = 0f;

        // Gradually increase the volume
        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, maxVolume, currentTime / fadeDuration);
            yield return null;
        }

        // Ensure the final volume is exactly the target volume
        audioSource.volume = maxVolume;
    }

    // Method to play specific sound effects
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
