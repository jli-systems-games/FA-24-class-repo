using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;  // Main background music
    public AudioClip zone1Ambience;    // Ambient track for Zone 1
    public AudioClip zone2Ambience;    // Ambient track for Zone 2
    public AudioClip zone3Ambience;    // Ambient track for Zone 3

    private AudioSource backgroundSource, track01, track02, track03;
    private AudioSource activeTrack;   // Currently playing ambient track

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // Set up the AudioSources
        backgroundSource = gameObject.AddComponent<AudioSource>();
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();
        track03 = gameObject.AddComponent<AudioSource>();

        // Configure background music
        backgroundSource.clip = backgroundMusic;
        backgroundSource.loop = true;
        backgroundSource.volume = 0.5f;  // Adjust volume if needed
        backgroundSource.Play();

        // Configure ambient tracks
        track01.clip = zone1Ambience;
        track02.clip = zone2Ambience;
        track03.clip = zone3Ambience;
        track01.loop = track02.loop = track03.loop = true;

        // Start with no active ambient track
        activeTrack = null;
    }

    // Method to swap to a new ambient track based on the zone
    public void SwapAmbientTrack(int zoneNumber)
    {
        Debug.Log("Attempting to swap ambient track for zone: " + zoneNumber);  // Add debug log here
        StopAllCoroutines();

        AudioSource newTrack = GetTrackForZone(zoneNumber);

        if (newTrack != null && newTrack != activeTrack)
        {
            Debug.Log("Swapping to new track for zone: " + zoneNumber);  // Add debug log here
            StartCoroutine(FadeAmbientTrack(newTrack));
        }
    }

    // Returns the appropriate ambient track for the specified zone number
    private AudioSource GetTrackForZone(int zoneNumber)
    {
        switch (zoneNumber)
        {
            case 1:
                return track01;
            case 2:
                return track02;
            case 3:
                return track03;
            default:
                return null;  // Invalid zone
        }
    }

    // Coroutine to fade out the current track and fade in the new one
    private IEnumerator FadeAmbientTrack(AudioSource newTrack)
    {
        float timeToFade = 0.5f;  // Duration for fade-out and fade-in
        float timeElapsed = 0;

        // Fade out the currently active track, if any
        if (activeTrack != null)
        {
            while (timeElapsed < timeToFade)
            {
                activeTrack.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            activeTrack.Stop();
        }

        // Start the new track and fade it in
        newTrack.Play();
        timeElapsed = 0;
        while (timeElapsed < timeToFade)
        {
            newTrack.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Set the new track as the currently active track
        activeTrack = newTrack;
    }
}
