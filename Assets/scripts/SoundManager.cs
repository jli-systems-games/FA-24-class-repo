using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;       // Reference to the AudioSource
    public GameObject[] targetObjects;    // Array of objects that will be disabled
    public AudioClip soundEffect;         // The sound effect you want to play

    private bool[] objectDisabled;        // Track which objects are disabled

    void Start()
    {
        // Initialize the array to track disabled states
        objectDisabled = new bool[targetObjects.Length];
    }

    void Update()
    {
        // Loop through all target objects
        for (int i = 0; i < targetObjects.Length; i++)
        {
            // Check if the object is inactive and hasn't been marked as disabled yet
            if (!targetObjects[i].activeInHierarchy && !objectDisabled[i] && Input.GetMouseButtonDown(0))
            {
                PlaySound();
                objectDisabled[i] = true;  // Mark this object as disabled to avoid repeating the sound
            }
        }
    }

    void PlaySound()
    {
        if (audioSource != null && soundEffect != null)
        {
            audioSource.PlayOneShot(soundEffect);
        }
    }
}
