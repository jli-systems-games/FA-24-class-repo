using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    public AudioSource spot1;
    public AudioSource spot2;
    public AudioSource spot3;
    public AudioSource spot4;
    public AudioSource spot5;
    public AudioSource spot6;

    public AudioSource track;

    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;

    public AudioSource randomScratch;
    public AudioSource[] audioSources;

    public pickTrack track1bool;
    public pickTrack track2bool;
    public pickTrack track3bool;

    public customToggleGroup samplesList;

    public Animator people1;
    public Animator people2;

    void SelectRandomClip()
    {
        int randomIndex = Random.Range(0, audioSources.Length);
        randomScratch = audioSources[randomIndex];
    }

    void Start()
    {
        spot1 = samplesList.pickedSamples[0];
        spot2 = samplesList.pickedSamples[1];
        spot3 = samplesList.pickedSamples[2];
        spot4 = samplesList.pickedSamples[3];
        spot5 = samplesList.pickedSamples[4];
        spot6 = samplesList.pickedSamples[5];
    }

    void Update()
    {
        if(track1bool.track1 == true)
        {
            track = track1;
        }

        if(track2bool.track2 == true)
        {
            track = track2;
        }

        if(track3bool.track3 == true)
        {
            track = track3;
        }

        if (Input.GetKeyDown(KeyCode.P) && !track.isPlaying)
        {
            track.Play();
            people1.SetBool("musicPlaying", true);
            people2.SetBool("musicPlaying", true);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            spot1.Play();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            spot2.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            spot3.Play();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            spot4.Play();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            spot5.Play();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            spot6.Play();
        }

        if (Input.GetMouseButton(0))
        {
            if(!track.isPlaying)
            {
                track.Play();
                people1.SetBool("musicPlaying", true);
                people2.SetBool("musicPlaying", true);
            }

            if (!randomScratch.isPlaying)
            {
                SelectRandomClip();
                randomScratch.Play();
            }
        }
        
        if (Input.GetMouseButtonUp(0)) 
        {
            if (randomScratch.isPlaying)
            {
                randomScratch.Pause();
            }
        }

        if (Input.GetKey(KeyCode.O))
        {
            track.volume += 0.0008f;
        }

        if (Input.GetKey(KeyCode.L))
        {
            track.volume -= 0.0008f;
        }

    }
}
