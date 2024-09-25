using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource backgroundMusicSource; // AudioSource for background music
    public AudioSource ambientSoundSource;    // AudioSource for ambient sounds

    public AudioClip musicClip;               // Background music clip
    public AudioClip ambientClip;             // Ambient sound clip (wind, trees, etc.)

    private bool isMuted = false;             // Track if the sound is muted or not

    void Start()
    {
        PlayBackgroundMusic();
        PlayAmbientSound();
    }

    void Update()
    {
        // Check if the "M" key is pressed to toggle mute/unmute
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMute();
        }
    }

    // Play the background music
    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && musicClip != null)
        {
            backgroundMusicSource.clip = musicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    // Play ambient sound (like wind, rustling trees)
    public void PlayAmbientSound()
    {
        if (ambientSoundSource != null && ambientClip != null)
        {
            ambientSoundSource.clip = ambientClip;
            ambientSoundSource.loop = true;
            ambientSoundSource.Play();
        }
    }

    // Toggle mute/unmute for both music and ambient sounds
    public void ToggleMute()
    {
        isMuted = !isMuted; // Toggle the mute state

        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.mute = isMuted;
        }

        if (ambientSoundSource != null)
        {
            ambientSoundSource.mute = isMuted;
        }

        Debug.Log("Audio " + (isMuted ? "Muted" : "Unmuted"));
    }
}
