using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public float fadeDuration = 2.0f; // Duration of the fade-in effect, in seconds
    public float maxVolume = 1.0f; // Target maximum volume for the audio
    private bool isMuted = false; // Track mute state

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0; // Start with volume at 0 for fade-in
        audioSource.playOnAwake = false;

        // Start playing the music with fade-in
        StartCoroutine(FadeInMusic());
    }

    private void Update()
    {
        // Check if the M key is pressed to toggle mute
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }

    private IEnumerator FadeInMusic()
    {
        audioSource.Play();

        float startVolume = 0;
        float elapsed = 0;

        // Gradually increase volume over the specified fade duration
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, maxVolume, elapsed / fadeDuration);
            yield return null;
        }

        audioSource.volume = maxVolume; // Ensure the volume reaches the target
    }

    private void ToggleMute()
    {
        isMuted = !isMuted; // Toggle mute state
        audioSource.mute = isMuted; // Set the AudioSource mute property based on the state
    }
}
