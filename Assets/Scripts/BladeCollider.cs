using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeCollider : MonoBehaviour
{
    private KnifeThrower knifeThrower;

    private void Start()
    {
        knifeThrower = GetComponentInParent<KnifeThrower>();
        if (knifeThrower == null)
        {
            Debug.LogError("KnifeThrower not found on parent.");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.name}"); // Debug log

        if (collision.gameObject.CompareTag("Target")) // If it hits the target
        {
            Debug.Log("Blade collider touched the target");  // Confirm collision with target
            knifeThrower.StopKnife(); // Stop the knife
            transform.SetParent(collision.transform); // Stick to the target
            transform.localPosition = Vector3.zero; // Align knife to target
            transform.localRotation = Quaternion.identity; // Reset rotation
        }
        else // If it hits walls or other colliders
        {
            Debug.Log("Knife hit a wall or other collider. Resetting...");
            knifeThrower.ResetKnife(); // Reset the knife immediately
        }
    }
}