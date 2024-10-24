using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scratch : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioSource[] audioSources;

    void SelectRandomClip()
    {
        int randomIndex = Random.Range(0, audioSources.Length);
        audioSource = audioSources[randomIndex];
    }

    void Update()
    {
        // Check if the left mouse button is being held down
        if (Input.GetMouseButton(0))  // 0 is for the left mouse button
        {
            if (!audioSource.isPlaying)
            {
                SelectRandomClip();
                audioSource.Play();  // Play the audio if not already playing
            }
        }
        
        if (Input.GetMouseButtonUp(0)) 
        {
            if (audioSource.isPlaying)
            {
                audioSource.Pause();  // Pause the audio if the button is released
            }
        }

    }

}
