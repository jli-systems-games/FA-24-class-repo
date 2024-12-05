using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // Singleton instance

    [Header("Audio Sources")]
    public AudioSource backgroundMusic; // Reference to the dedicated background music AudioSource

    [Header("Settings")]
    public float fadeInDuration = 5.0f; // Duration for the fade-in effect

    private void Awake()
    {
        // Ensure only one instance of MusicManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
            return;
        }

        // Ensure the backgroundMusic AudioSource is assigned
        if (backgroundMusic == null)
        {
            Debug.LogError("No AudioSource assigned for background music in MusicManager!");
        }
    }

    private void Start()
    {
        // Fade in music only in the first scene
        if (SceneManager.GetActiveScene().buildIndex == 0) // Assuming the first scene has build index 0
        {
            StartCoroutine(FadeInMusic());
        }
    }

    // Play the specified music clip in the background music slot
    public void PlayBackgroundMusic(AudioClip clip, bool loop = true)
    {
        if (backgroundMusic == null) return;

        if (backgroundMusic.clip == clip)
        {
            return; // Prevent restarting the same music
        }

        backgroundMusic.clip = clip;
        backgroundMusic.loop = loop;
        backgroundMusic.Play();
    }

    // Stop playing background music
    public void StopBackgroundMusic()
    {
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            backgroundMusic.Stop();
        }
    }

    // Adjust the background music volume
    public void SetBackgroundMusicVolume(float volume)
    {
        if (backgroundMusic != null)
        {
            backgroundMusic.volume = Mathf.Clamp01(volume); // Ensure volume is between 0 and 1
        }
    }

    // Fade in the background music
    private IEnumerator FadeInMusic()
    {
        if (backgroundMusic == null) yield break;

        float targetVolume = backgroundMusic.volume; // Save the target volume
        backgroundMusic.volume = 0; // Start at 0 volume
        backgroundMusic.Play();

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime += Time.deltaTime;
            backgroundMusic.volume = Mathf.Lerp(0, targetVolume, elapsedTime / fadeInDuration);
            yield return null;
        }

        backgroundMusic.volume = targetVolume; // Ensure it's set to the target volume at the end
    }
}
