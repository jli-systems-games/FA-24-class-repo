using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioZoneTrigger : MonoBehaviour
{
    public AudioClip[] newMusicClips; // Array for new music clips for this zone
    public AudioClip[] newAmbientClips; // Array for new ambient sounds for this zone

    private AudioManager audioManager; // Reference to the AudioManager

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>(); // Find the AudioManager in the scene
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the colliding object is the player
        {
            Debug.Log($"Entered audio zone: {gameObject.name} - Changing music and ambient sounds.");
            audioManager.ChangeMusic(newMusicClips); // Change music for this zone
            audioManager.ChangeAmbient(newAmbientClips); // Change ambient sound for this zone
        }
    }
}
