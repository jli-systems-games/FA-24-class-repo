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

    [SerializeField] private Transform holdPoint; // Position to hold the item
    [SerializeField] private Transform playerCamera; // Camera position for raycasting
    [SerializeField] private LayerMask pickUpLayerMask; // Specify layers to detect

    [SerializeField] private TextMeshProUGUI pickupText; // Text element for display
    [SerializeField] private TextMeshProUGUI pocketMessageText; // Text element to show pocket message
    [SerializeField] private FirstPersonMovement playerMovement; // Reference to player movement script
    [SerializeField] private FirstPersonLook cameraLook; // Reference to the camera look script

    private bool pickedUp; // Flag to check if an item is picked up
    private Transform pickedObjectTransform; // Store reference to the picked object transform
    private float raycastDistance = 1f; // Distance for raycast detection

    void Update()
    {
        // Create a ray from the player's camera position, pointing forward
        ray = new Ray(playerCamera.position, playerCamera.forward);
        Debug.DrawRay(playerCamera.position, playerCamera.forward * raycastDistance, Color.green);

        // Perform raycast to detect items in front
        DetectGrabbableItem();

        // Check for the "E" key press for pickup or pocketing
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpOrPocketItem();
        }

        // Check for rotation input when an item is picked up
        if (pickedUp)
        {
            // Use mouse scroll wheel for rotation
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            if (Mathf.Abs(scrollInput) > 0.01f) // Check if there's significant scroll input
            {
                // Call the Rotate method on the Grabbable component
                _grabbable.Rotate(scrollInput * 100f); // Adjust the multiplier for sensitivity
            }
        }
    }

    // Detect grabbable items in front of the player using raycasting
    private void DetectGrabbableItem()
    {
        if (Physics.Raycast(ray, out hit, raycastDistance, pickUpLayerMask))
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

    // Handle picking up or pocketing the item based on the current state
    private void PickUpOrPocketItem()
    {
        if (!pickedUp)
        {
            TryPickUpItem();
        }
        else
        {
            PutItemInPocket();
        }
    }

    // Attempt to pick up the item if available
    private void TryPickUpItem()
    {
        if (hit.transform != null && hit.transform.TryGetComponent(out _grabbable))
        {
            // Call the Grab method on the Grabbable component
            _grabbable.Grab(holdPoint, playerCamera);

            // Store reference to the picked object's transform
            pickedObjectTransform = hit.transform;

            // Disable player and camera movement
            playerMovement.enabled = false;
            cameraLook.enabled = false;

            // Set flag
            pickedUp = true;

            // Hide any previous pocket message
            pocketMessageText.gameObject.SetActive(false);
        }
    }

    // Handle pocketing the item (destroying it)
    private void PutItemInPocket()
    {
        // If no item description is present, skip the message
        if (_itemDescription != null)
        {
            pocketMessageText.text = $"You picked up a {_itemDescription.itemName}!"; // Use item name instead
            pocketMessageText.gameObject.SetActive(true); // Show the message

            // Start the coroutine to hide the pocket message after 1.5 second
            StartCoroutine(HidePocketMessage(1.5f));
        }

        // Destroy the object and reset the reference
        if (pickedObjectTransform != null)
        {
            Destroy(pickedObjectTransform.gameObject);
            pickedObjectTransform = null;
        }

        // Enable player and camera movement again
        playerMovement.enabled = true;
        cameraLook.enabled = true;

        // Hide pickup message and reset flags
        pickedUp = false;
        DisplayPickUpText(""); // Clear the pickup text
    }

    // Coroutine to hide the pocket message after a specified time
    private IEnumerator HidePocketMessage(float delay)
    {
        yield return new WaitForSeconds(delay);
        pocketMessageText.gameObject.SetActive(false); // Hide the message
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