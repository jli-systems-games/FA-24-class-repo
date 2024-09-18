using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource rotationSound; // AudioSource for rotation sound effects
    public AudioSource unlockSound; // AudioSource for unlock sound effects
    public AudioSource backgroundMusicSource; // AudioSource for background music

    [Range(0f, 1f)] public float rotationSoundVolume = 1f; // Volume for rotation sound
    [Range(0f, 1f)] public float unlockSoundVolume = 1f;   // Volume for unlock sound
    [Range(0f, 1f)] public float backgroundMusicVolume = 1f; // Volume for background music

    public float fadeDuration = 1f; // Duration for fade in/out effect

    void Start()
    {
        // Ensure that the background music starts playing
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = backgroundMusicVolume;
            backgroundMusicSource.Play();
        }
    }

    public void PlayRotationSound()
    {
        if (rotationSound != null && !rotationSound.isPlaying)
        {
            StartCoroutine(HandleSoundEffect(rotationSound, rotationSoundVolume));
        }
    }

    public void PlayUnlockSound()
    {
        if (unlockSound != null)
        {
            StartCoroutine(HandleSoundEffect(unlockSound, unlockSoundVolume));
        }
    }

    private IEnumerator HandleSoundEffect(AudioSource soundEffect, float soundEffectVolume)
    {
        // Fade out background music
        if (backgroundMusicSource != null)
        {
            yield return StartCoroutine(FadeAudioSource(backgroundMusicSource, backgroundMusicSource.volume, backgroundMusicVolume - 0.5f, fadeDuration));
        }

        // Play the sound effect
        soundEffect.volume = soundEffectVolume; // Adjust this as necessary
        soundEffect.Play();

        // Wait for the sound effect to finish
        yield return new WaitForSeconds(soundEffect.clip.length);

        // Fade background music back in
        if (backgroundMusicSource != null)
        {
            yield return StartCoroutine(FadeAudioSource(backgroundMusicSource, backgroundMusicSource.volume, backgroundMusicVolume, fadeDuration));
        }
    }

    private IEnumerator FadeAudioSource(AudioSource audioSource, float startVolume, float endVolume, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float elapsed = (Time.time - startTime) / duration;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsed);
            yield return null;
        }

        audioSource.volume = endVolume; // Ensure the final volume is set
    }
}