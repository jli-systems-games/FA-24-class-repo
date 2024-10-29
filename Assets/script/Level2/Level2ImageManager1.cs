using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2ImageManager1 : MonoBehaviour
{
    public Level2Ball ball; 
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
            FindObjectOfType<Level2Manager>().EndLevel2(); // Or your specific method to end the level
        }
    }
}