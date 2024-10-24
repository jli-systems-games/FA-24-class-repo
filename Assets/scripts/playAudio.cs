using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    public AudioSource punch;
    public AudioSource synth;
    public AudioSource airhorn;
    public AudioSource kick;
    public AudioSource hihat;
    public AudioSource snare;
    public AudioSource clap;

    public AudioSource track;

    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;

    public AudioSource randomScratch;
    public AudioSource[] audioSources;

    public pickTrack track1bool;
    public pickTrack track2bool;
    public pickTrack track3bool;

    void SelectRandomClip()
    {
        int randomIndex = Random.Range(0, audioSources.Length);
        randomScratch = audioSources[randomIndex];
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
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            punch.Play();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            synth.Play();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            airhorn.Play();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            kick.Play();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            hihat.Play();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            snare.Play();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            clap.Play();
        }

        if (Input.GetMouseButton(0))
        {
            if(!track.isPlaying)
            {
                track.Play();
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
