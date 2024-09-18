using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource rotationSound; // AudioSource for rotation sound effects
    public AudioSource unlockSound; // AudioSource for unlock sound effects
    public AudioSource stuckSound;    // AudioSource for stuck sound effects
    public AudioSource backgroundMusicSource; // AudioSource for background music

    [Range(0f, 1f)] public float rotationSoundVolume = 1f; // Volume for rotation sound
    [Range(0f, 1f)] public float unlockSoundVolume = 1f;   // Volume for unlock sound
    [Range(0f, 1f)] public float stuckSoundVolume = 1f;    // Volume for stuck sound
    [Range(0f, 1f)] public float backgroundMusicVolume = 1f; // Volume for background music


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
            rotationSound.volume = rotationSoundVolume;
            rotationSound.Play();
        }
    }

    public void PlayUnlockSound()
    {
        if (unlockSound != null)
        {
            unlockSound.volume = unlockSoundVolume;
            unlockSound.Play();
        }
    }

    public void PlayStuckSound()
    {
        if (stuckSound != null)
        {
            stuckSound.volume = stuckSoundVolume;
            stuckSound.Play();
        }
    }
}