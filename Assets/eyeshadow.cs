using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eyeshadow : MonoBehaviour
{
    AudioSource audioSource;

    public GameObject eyeshadowCanvas;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            eyeshadowCanvas.gameObject.SetActive(true);
        }
    }
}
