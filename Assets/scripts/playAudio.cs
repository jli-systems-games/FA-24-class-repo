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

    public AudioSource randomScratch;
    public AudioSource[] audioSources;

    void SelectRandomClip()
    {
        int randomIndex = Random.Range(0, audioSources.Length);
        randomScratch = audioSources[randomIndex];
    }

    void Update()
    {
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
