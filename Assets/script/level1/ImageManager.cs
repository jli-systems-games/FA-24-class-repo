using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    public Ball ball; 
    public GameObject ballItem;
    public GameObject[] images; // Array of images to manage

    private void Update()
    {
       
        HandleImageVisibility();
        CheckForLevelEnd(); // Check if the level should end
    }

    private void HandleImageVisibility()
    {
        int resetCount = ball.GetResetCount(); // Get the current reset count

        // Disable an image for every 3 resets
        for (int i = 0; i < images.Length; i++)
        {
            if (i < resetCount / 1) 
            {
               // GameObject.Find("GameManager").GetComponent<GameManager>().ResetPosition();
                images[i].SetActive(false); // Disable image
            }
        }
    }

    private void CheckForLevelEnd()
    {
        bool allImagesDisabled = true;

        foreach (GameObject img in images)
        {
            if (img.activeSelf) // If any image is still active
            {
                allImagesDisabled = false;
                break;
            }
        }

        if (allImagesDisabled)
        {
            // Call the EndLevel method from your GameManager
            ballItem.SetActive(false);
            FindObjectOfType<GameManager>().EndLevel(); // Or your specific method to end the level
        }
    }
}