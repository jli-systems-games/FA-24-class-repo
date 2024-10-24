using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickTrack : MonoBehaviour
{

    public Toggle track1Toggle;
    public Toggle track2Toggle;
    public Toggle track3Toggle;

    public bool track1;
    public bool track2;
    public bool track3;

    public AudioSource track1preview;
    public AudioSource track2preview;
    public AudioSource track3preview;

    public GameObject canvas;
    public GameObject gameParent;

    public void nextButton()
    {
        gameParent.gameObject.SetActive(true);
        if(track1Toggle.isOn)
        {
            track1 = true;
            track2 = false;
            track3 = false;
        }

        if(track2Toggle.isOn)
        {
            track2 = true;
            track1 = false;
            track3 = false;
        }

        if(track3Toggle.isOn)
        {
            track3 = true;
            track1 = false;
            track2 = false;
        }
        canvas.gameObject.SetActive(false);
    }

    public void playtrack1()
    {
        if(track2preview.isPlaying)
        {
            track2preview.Pause();
        }
        if(track3preview.isPlaying)
        {
            track3preview.Pause();
        }

        track1preview.Play();
    }

    public void playtrack2()
    {
        if(track1preview.isPlaying)
        {
            track1preview.Pause();
        }
        if(track3preview.isPlaying)
        {
            track3preview.Pause();
        }

        track2preview.Play();
    }

    public void playtrack3()
    {
        if(track2preview.isPlaying)
        {
            track2preview.Pause();
        }
        if(track1preview.isPlaying)
        {
            track1preview.Pause();
        }

        track3preview.Play();
    }
}
