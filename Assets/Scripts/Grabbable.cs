using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Transform originalParent;
    private Rigidbody objectRigidbody;
    private Collider objectCollider;

    // Assign the player's collider reference through the PickUpInspect script or Inspector
    public Collider playerCollider;

    // Call this when the item is picked up
    public void Grab(Transform holdPoint, Transform playerCamera)
    {
        // Store the original parent and component references
        originalParent = transform.parent;
        objectRigidbody = GetComponent<Rigidbody>();
        objectCollider = GetComponent<Collider>();

        // Disable physics (no gravity, no forces applied)
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = true;
            objectRigidbody.useGravity = false;
        }

        // Ignore collisions between the player and the object
        if (playerCollider != null && objectCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, objectCollider, true);
        }

        // Move item to the hold point and set it as a child of that point
        transform.position = holdPoint.position;
        transform.SetParent(holdPoint);
        transform.rotation = Quaternion.identity; // Reset rotation
    }

    // Call this when the item is dropped or when exiting the hold state
    public void Drop()
    {
        // If rigidbody exists, re-enable physics and gravity
        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = false;
            objectRigidbody.useGravity = true;
        }

        // Re-enable collisions with the player if both colliders exist
        if (playerCollider != null && objectCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, objectCollider, false);
        }

        // Detach the item from its current parent and restore the original parent
        transform.SetParent(originalParent);
    }

    // Rotate the object around its local Y-axis based on the scroll input
    public void Rotate(float rotationAmount)
    {
        // Rotate the object around the Y-axis (up)
        transform.Rotate(Vector3.up, rotationAmount);
    }

    // Optional: Call this method if you want to destroy the item when exiting
    public void DestroyItem()
    {
        // Make sure to restore the item’s physics state before destroying it
        if (playerCollider != null && objectCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, objectCollider, false);
        }

        if (objectRigidbody != null)
        {
            objectRigidbody.isKinematic = false;
            objectRigidbody.useGravity = true;
        }

        // Finally, destroy the game object
        Destroy(gameObject);
    }
}
