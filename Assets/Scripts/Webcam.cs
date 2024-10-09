using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Webcam : MonoBehaviour
{
    public RawImage webcamDisplay;
    private WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        webcamTexture = new WebCamTexture();
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
