using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource[] musicSources; // Array of music AudioSources
    public AudioSource[] ambientSources; // Array of ambient AudioSources
    public float fadeDuration = 2.0f; // Duration for the fade effect

    private void Start()
    {
        // Ensure all audio sources start silent
        foreach (var source in musicSources)
        {
            source.volume = 0;
        }

        foreach (var source in ambientSources)
        {
            source.volume = 0;
        }
    }

    // Method to change music tracks dynamically
    public void ChangeMusic(AudioClip[] newMusicClips)
    {
        StartCoroutine(FadeOutMusic(newMusicClips)); // Fade out current music and pass new clips to load
    }

    // Coroutine to fade out current music
    private IEnumerator FadeOutMusic(AudioClip[] newMusicClips)
    {
        float startVolume = musicSources[0].volume;

        while (musicSources[0].volume > 0)
        {
            foreach (var source in musicSources)
            {
                source.volume -= startVolume * Time.deltaTime / fadeDuration;
            }
            yield return null;
        }

        // Assign new music clips and start playing
        for (int i = 0; i < musicSources.Length; i++)
        {
            if (i < newMusicClips.Length)
            {
                musicSources[i].clip = newMusicClips[i];
                musicSources[i].Play();
            }
            else
            {
                musicSources[i].Stop(); // Stop sources not being used
            }
        }

        StartCoroutine(FadeInMusic());
    }

    // Coroutine to fade in the music
    private IEnumerator FadeInMusic()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            foreach (var source in musicSources)
            {
                if (source.isPlaying) // Only increase volume for sources with clips
                {
                    source.volume = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var source in musicSources)
        {
            source.volume = 1; // Ensure final volume is set correctly
        }
    }

    // Method to change ambient sounds dynamically
    public void ChangeAmbient(AudioClip[] newAmbientClips)
    {
        StartCoroutine(FadeOutAmbient(newAmbientClips));
    }

    // Coroutine to fade out current ambient sound
    private IEnumerator FadeOutAmbient(AudioClip[] newAmbientClips)
    {
        float startVolume = ambientSources[0].volume;

        while (ambientSources[0].volume > 0)
        {
            foreach (var source in ambientSources)
            {
                source.volume -= startVolume * Time.deltaTime / fadeDuration;
            }
            yield return null;
        }

        // Assign new ambient clips and start playing
        for (int i = 0; i < ambientSources.Length; i++)
        {
            if (i < newAmbientClips.Length)
            {
                ambientSources[i].clip = newAmbientClips[i];
                ambientSources[i].Play();
            }
            else
            {
                ambientSources[i].Stop(); // Stop sources not being used
            }
        }

        StartCoroutine(FadeInAmbient());
    }

    // Coroutine to fade in the ambient sound
    private IEnumerator FadeInAmbient()
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < fadeDuration)
        {
            foreach (var source in ambientSources)
            {
                if (source.isPlaying) // Only increase volume for sources with clips
                {
                    source.volume = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        foreach (var source in ambientSources)
        {
            source.volume = 1; // Ensure final volume is set correctly
        }
    }
}
