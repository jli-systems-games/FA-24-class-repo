using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    public RawImage cameraView;  // UI element to display the camera feed
    public GameObject targetCube;  // The Cube where the texture will be applied
    private WebCamTexture webCamTexture;
    private Texture2D capturedTexture;

    void Start()
    {
        // Initialize the webcam texture
        webCamTexture = new WebCamTexture();
        cameraView.texture = webCamTexture;
        webCamTexture.Play();  // Start the camera feed
    }

    // Capture the image inside the RawImage's area
    public void CapturePhoto()
    {
        // Use the camera's actual resolution for the Texture2D
        int cameraWidth = webCamTexture.width;
        int cameraHeight = webCamTexture.height;

        // Log the camera resolution for debugging
        Debug.Log($"Camera resolution: {cameraWidth} x {cameraHeight}");

        // Create a Texture2D with the camera's resolution
        capturedTexture = new Texture2D(cameraWidth, cameraHeight);
        
        // Capture the full pixels from the webcam texture
        capturedTexture.SetPixels(webCamTexture.GetPixels());
        capturedTexture.Apply();
        
        // Adjust texture settings to prevent tiling or repetition
        capturedTexture.wrapMode = TextureWrapMode.Clamp;

        // Apply the captured texture to the Cube's material
        Material cubeMaterial = targetCube.GetComponent<Renderer>().material;
        cubeMaterial.mainTexture = capturedTexture;
        cubeMaterial.mainTextureScale = new Vector2(1, 1);  // Ensure full coverage on each face without repetition
    }

    void OnDisable()
    {
        webCamTexture.Stop();  // Stop the webcam when the object is disabled
    }
}
