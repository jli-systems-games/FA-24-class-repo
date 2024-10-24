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
