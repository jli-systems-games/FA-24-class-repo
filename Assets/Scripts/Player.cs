using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f; // Movement speed
    public float verticalLimit = 2f; // Maximum range for up/down movement

    private Vector3 originalScale; // To store the player's original scale
    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Save the original scale of the player
        originalScale = transform.localScale;

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Ensure the correct Animator Controller is assigned
        if (animator.runtimeAnimatorController == null)
        {
            Debug.LogError("No Animator Controller assigned to the Animator component!");
        }
    }

    void Update()
    {
        // Get horizontal and vertical input from arrow keys or WASD
        float horizontalInput = Input.GetAxisRaw("Horizontal"); // Left/Right input (-1, 0, 1)
        float verticalInput = Input.GetAxisRaw("Vertical");     // Up/Down input (-1, 0, 1)

        // Calculate vertical movement with clamping
        float targetY = Mathf.Clamp(transform.position.y + verticalInput * speed * Time.deltaTime, -verticalLimit, verticalLimit);

        // Move the player
        transform.position = new Vector3(
            transform.position.x + horizontalInput * speed * Time.deltaTime, // Horizontal movement
            targetY,                                                         // Vertical movement (clamped)
            transform.position.z                                             // Keep Z position unchanged
        );

        // Flip the player based on direction
        if (horizontalInput > 0)
        {
            // Moving right: face right
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (horizontalInput < 0)
        {
            // Moving left: face left (flip horizontally)
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

        // Update Animator's Speed parameter
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput * speed));
    }
}
