using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    public RawImage cameraView;  // UI element to display the camera feed
    public GameObject targetCube;  // The Cube where the texture will be applied
    public GameObject photoCanvas; // Reference to the PhotoCanvas UICanvas
    private WebCamTexture webCamTexture;
    private Texture2D capturedTexture;
    public GameObject interactCanvas;

   // public GameObject photoPos;

    public float zoomFactor = 3.5f;  // Zoom factor to control the level of zoom
    public float cutSizeFactor = 1.5f; // Factor to reduce the captured square size

    void Start()
    {
        interactCanvas.SetActive(false);
        // Initialize the webcam texture
        webCamTexture = new WebCamTexture();
        cameraView.texture = webCamTexture;
        webCamTexture.Play();  // Start the camera feed

       // photoPos.SetActive(false);
        
        // Flip the camera view horizontally to mirror the image
        cameraView.rectTransform.localScale = new Vector3(-8, 4, 1);  // Flip horizontally
    }

    // Capture a square portion of the image inside the RawImage's area
    public void CapturePhoto()
    {
        // Calculate the size for the square portion (smaller of width or height)
        int squareSize = Mathf.Min(webCamTexture.width, webCamTexture.height);
        
        // Calculate the center position for the square region
        int xOffset = (webCamTexture.width - squareSize) / 2;
        int yOffset = (webCamTexture.height - squareSize) / 2;

        // Adjust the offset for zooming in
        int zoomedSquareSize = Mathf.FloorToInt(squareSize / zoomFactor);
        
        // Further reduce the size of the captured square
        int finalSquareSize = Mathf.FloorToInt(zoomedSquareSize / cutSizeFactor);
        
        int zoomedXOffset = xOffset + (squareSize - finalSquareSize) / 2;
        int zoomedYOffset = yOffset + (squareSize - finalSquareSize) / 2;

        // Set the size of the Texture2D to be square
        capturedTexture = new Texture2D(finalSquareSize, finalSquareSize);

        // Capture the square portion from the webcam texture
        capturedTexture.SetPixels(webCamTexture.GetPixels(zoomedXOffset, zoomedYOffset, finalSquareSize, finalSquareSize));
        capturedTexture.Apply();

        // Adjust texture settings
        capturedTexture.wrapMode = TextureWrapMode.Clamp;  // Prevent tiling or repetition

        // Apply the captured square texture to the Cube's material
        Material cubeMaterial = targetCube.GetComponent<Renderer>().material;
        cubeMaterial.mainTexture = capturedTexture;
        cubeMaterial.mainTextureScale = new Vector2(1, 1);  // Ensure full coverage on each face without repetition

        // Close the PhotoCanvas after capturing the photo
        photoCanvas.SetActive(false);
        interactCanvas.SetActive(true);
    }

    void OnDisable()
    {
        webCamTexture.Stop();  // Stop the webcam when the object is disabled
    }
}
