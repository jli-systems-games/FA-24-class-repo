using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scratching : MonoBehaviour
{
    public AudioSource track;
    public float scratchSpeed = 0.1f;

    private bool isScratching = false;

    void Update()
    {
        // Detect mouse button down
        if (Input.GetMouseButtonDown(0))
        {
            isScratching = true;
        }

        // Detect mouse button up
        if (Input.GetMouseButtonUp(0))
        {
            isScratching = false;
        }

        // If scratching, adjust playback
        if (isScratching)
        {
            float mouseMovement = Input.GetAxis("Mouse X");
            if (mouseMovement != 0)
            {
                // Change the audio time based on mouse movement
                track.time += mouseMovement * scratchSpeed;

                // Loop the audio if it goes out of bounds
                if (track.time >= track.clip.length)
                {
                    track.time = 0;
                }
                else if (track.time < 0)
                {
                    track.time = track.clip.length;
                }
            }
        }
    }
}
