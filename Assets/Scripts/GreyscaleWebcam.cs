using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GreyscaleWebcam : MonoBehaviour
{
    public RawImage webcamDisplay;
    public Material grayscaleMaterial;

    private WebCamTexture webcamTexture;

    // Start is called before the first frame update
    void Start()
    {
        webcamTexture = new WebCamTexture();
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();

        // Apply the grayscale shader material to the RawImage
        // This sets the material for the RawImage's UI rendering, but doesn't replace the texture
        webcamDisplay.material = grayscaleMaterial;

        // Set the webcam texture to the shader's _MainTex property
        grayscaleMaterial.SetTexture("_MainTex", webcamTexture);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDestroy()
    {
        // Clean up the webcam texture when the object is destroyed
        if (webcamTexture != null && webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
    }
}
