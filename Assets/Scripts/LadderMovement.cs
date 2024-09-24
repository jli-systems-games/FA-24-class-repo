using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    public float climbSpeed = 2.0f;
    private bool isClimbing = false;
    private Rigidbody playerRigidbody;
    private FirstPersonMovement firstPersonMovement;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = true;
            playerRigidbody = other.GetComponent<Rigidbody>();
            firstPersonMovement = other.GetComponent<FirstPersonMovement>(); // Disable the normal movement while climbing
            if (firstPersonMovement != null)
            {
                firstPersonMovement.enabled = false; // Disabling player movement during climb
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isClimbing = false;
            if (firstPersonMovement != null)
            {
                firstPersonMovement.enabled = true; // Re-enable player movement after climbing
            }
        }
    }

    void FixedUpdate()
    {
        if (isClimbing && playerRigidbody != null)
        {
            float vertical = Input.GetAxis("Vertical"); // Use Vertical input (W/S or Up/Down arrows) to climb
            Vector3 climbDirection = new Vector3(0, vertical * climbSpeed, 0);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, climbDirection.y, playerRigidbody.velocity.z);
        }
    }
}