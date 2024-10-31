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
        Debug.Log($"Collided with: {collision.gameObject.name}");

        if (collision.gameObject.CompareTag("OuterTarget") || collision.gameObject.CompareTag("Bullseye"))
        {
            Debug.Log("Blade collider touched the target");
            knifeThrower.StopKnife();
        }
        else if (collision.gameObject.CompareTag("Wall")) // Check if it collides with a wall
        {
            Debug.Log("Knife hit a wall. Resetting...");
            knifeThrower.ResetKnife(); // Reset the knife when it hits a wall
        }
        else
        {
            Debug.Log("Knife hit an unknown object. Resetting...");
            knifeThrower.ResetKnife(); // Reset for any other unexpected collision
        }
    }
}