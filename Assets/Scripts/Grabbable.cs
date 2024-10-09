using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Transform originalParent; // Store the original parent transform
    private Vector3 originalPosition; // Store the original position of the item
    private Quaternion originalRotation; // Store the original rotation of the item

    void Start()
    {
        // Store the initial position and rotation when the game starts
        originalParent = transform.parent;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    // Call this when the item is picked up
    public void Grab(Transform holdPoint, Transform playerCamera)
    {
        // Store the current parent in case we need to reset
        originalParent = transform.parent;

        // Set the position and parent to the hold point
        transform.position = holdPoint.position;
        transform.SetParent(holdPoint);

        // Optionally, reset rotation
        transform.rotation = Quaternion.identity;
    }

    // Call this when the item is dropped
    public void Drop()
    {
        // Reset to original parent
        transform.SetParent(originalParent);
    }

    // Reset the item's position and rotation to its original state
    public void ReturnToOriginalPosition()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }
}
