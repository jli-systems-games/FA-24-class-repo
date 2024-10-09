using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PickUpInspect : MonoBehaviour
{
    // Raycasting and reference variables
    private Ray ray;
    private RaycastHit hit;
    private Grabbable _grabbable; // Reference to the Grabbable script
    private ItemDescription _itemDescription; // Reference to the ItemDescription script
    [SerializeField] Transform holdPoint; // Position to hold the item
    [SerializeField] Transform playerCamera; // Camera position for raycasting
    [SerializeField] LayerMask pickUpLayerMask; // Specify layers to detect

    [SerializeField] private TextMeshProUGUI pickupText; // Text element for display
    [SerializeField] private FirstPersonMovement playerMovement; // Reference to player movement script
    [SerializeField] private FirstPersonLook cameraLook; // Reference to the camera look script

    private bool pickedUp; // Flag to check if an item is picked up
    private Vector3 originalPosition; // Store the original position of the item

    void Update()
    {
        // Create a ray from the player's camera position, pointing forward
        ray = new Ray(playerCamera.position, playerCamera.forward);
        Debug.DrawRay(playerCamera.position, playerCamera.forward * 10, Color.green);

        // Check for the "E" key press for pickup
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpInput();
        }

        // Rotate the item if it is picked up and exists in the hit variable
        if (pickedUp && hit.transform != null)
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input
            if (scrollInput != 0f) // Check if there is any scroll input
            {
                // Rotate the item based on scroll input
                hit.transform.Rotate(0, scrollInput * 100f, 0); // Adjust the multiplier for speed
            }
        }

        // Perform raycast to detect items in front
        if (Physics.Raycast(ray, out hit, 1f, pickUpLayerMask))
        {
            if (hit.transform.TryGetComponent(out _grabbable))
            {
                // Only show the description if not picked up
                if (hit.transform.TryGetComponent(out _itemDescription) && !pickedUp)
                {
                    DisplayPickUpText(_itemDescription.itemDescription);
                }
            }
        }
        else
        {
            DisplayPickUpText(""); // Hide text if nothing is detected
        }
    }

    private void PickUpInput()
    {
        // If we haven't picked up an item yet
        if (!pickedUp)
        {
            // Check if we are hitting a grabbable item
            if (hit.transform != null && hit.transform.TryGetComponent(out _grabbable))
            {
                // Store the original position
                originalPosition = hit.transform.position;

                // Call the Grab method on the Grabbable component
                _grabbable.Grab(holdPoint, playerCamera);

                // Disable player and camera movement
                playerMovement.enabled = false;
                cameraLook.enabled = false;

                // Set flags
                pickedUp = true;
            }
        }
        else
        {
            // Drop the item if already holding it
            _grabbable.Drop();

            // Return the item to its original position
            hit.transform.position = originalPosition;

            // Enable player and camera movement again
            playerMovement.enabled = true;
            cameraLook.enabled = true;

            // Reset flags
            pickedUp = false;
        }
    }

    // Method to display the pickup text
    private void DisplayPickUpText(string message)
    {
        if (pickupText != null)
        {
            pickupText.text = message;
            pickupText.gameObject.SetActive(!string.IsNullOrEmpty(message)); // Show or hide based on content
        }
    }
}