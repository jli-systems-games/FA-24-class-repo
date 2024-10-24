using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrubbing : MonoBehaviour
{
    public AudioSource audioSource;
    public float minPitch = -2f;
    public float maxPitch = 2f;
    public float sensitivity = 1f;

    private bool isScratching = false;

    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;

    public pickTrack track1bool;
    public pickTrack track2bool;
    public pickTrack track3bool;

    void Start()
    {
        if(track1bool.track1 == true)
        {
            audioSource = track1;
        }

        if(track2bool.track2 == true)
        {
            audioSource = track2;
        }

        if(track3bool.track3 == true)
        {
            audioSource = track3;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            isScratching = true;
        }
        else
        {
            isScratching = false;
        }

        if (isScratching)
        {
            float rotationInput = Input.GetAxis("Mouse X");
            float pitchValue = Mathf.Clamp(rotationInput * sensitivity, minPitch, maxPitch);
            audioSource.pitch = pitchValue;
        }
        else
        {
            audioSource.pitch = 1f;
        }
    }
}
