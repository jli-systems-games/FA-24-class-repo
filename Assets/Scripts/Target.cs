using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife")) // Ensure the knife has the tag "Knife"
        {
            Debug.Log("Target hit!");
            // Add any additional effects or scoring logic here
        }
    }
}
