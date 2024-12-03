using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1.3f; // Movement speed
    public float verticalLimit = 0.85f; // Maximum range for up/down movement

    private Vector3 originalScale; // To store the player's original scale
    private Animator animator; // Reference to the Animator component
    private Rigidbody2D rb; // Reference to the Rigidbody2D component

    void Start()
    {
        // Save the original scale of the player
        originalScale = transform.localScale;

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Ensure the correct Animator Controller is assigned
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("No Animator Controller assigned to the Animator component!");
        }
    }

    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            // Disable movement by setting velocity to zero and stopping movement animations
            rb.velocity = Vector2.zero;
            animator.SetFloat("Speed", 0);
            return; // Exit FixedUpdate early to prevent further movement logic
        }

        // Get horizontal and vertical input from arrow keys or WASD
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Calculate movement vector
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * speed;

        // Apply movement to the Rigidbody2D
        rb.velocity = movement;

        // Clamp the player's position within the vertical limit
        rb.position = new Vector2(rb.position.x, Mathf.Clamp(rb.position.y, -verticalLimit, verticalLimit));

        // Update Animator's Speed parameter based on movement magnitude
        animator.SetFloat("Speed", movement.magnitude);

        // Flip the player based on horizontal movement direction
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
    }
}