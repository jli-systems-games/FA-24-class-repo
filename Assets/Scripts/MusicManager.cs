using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    private AudioSource musicSource;

    [Range(0f, 1f)]
    public float maxVolume = 1f;       // The maximum volume for the music
    public float fadeInDuration = 2f;  // Duration of the fade-in effect, in seconds

    private void Awake()
    {
        // Ensure only one instance of MusicManager exists across scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Initialize AudioSource
            musicSource = GetComponent<AudioSource>();
            musicSource.volume = 0f; // Start volume at 0 for fade-in effect
            musicSource.Play();      // Start playing music

            // Start the fade-in effect
            StartCoroutine(FadeInMusic());
        }
    }

    // Coroutine to fade in the music
    private IEnumerator FadeInMusic()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeInDuration)
        {
            musicSource.volume = Mathf.Lerp(0f, maxVolume, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure final volume is set to maxVolume after the loop
        musicSource.volume = maxVolume;
    }
}
