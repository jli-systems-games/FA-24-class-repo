using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float gravity;  // Gravity applied when in the air
    public Vector2 velocity;  // Player's movement speed and direction
    public float maxXVelocity = 100; // Cap on horizontal speed to prevent infinite acceleration
    public float maxAcceleration = 10; // Maximum rate of acceleration for player movement
    public float acceleration = 10; // Current acceleration value used for horizontal movement
    public float distance = 0; // Tracks the total horizontal distance traveled by the player
    public float jumpVelocity = 12;  // Initial speed when jumping

    public float groundHeight = -2.5f;  // Y-coordinate for ground level
    public bool isGrounded = false;  // Tracks if player is on the ground

    public bool isHoldingJump = false;  // Tracks if jump key is held
    public float maxHoldJumpTime = 0.4f;  // Maximum time jump key can affect jump height
    public float holdJumpTimer = 0.0f;  // Timer to track how long jump is held

    public float jumpGroundThreshold = 1;  // Distance from ground where player can still jump

    void Start()
    {

    }

    void Update()
    {
        Vector2 pos = transform.position;  // Current player position
        float groundDistance = Mathf.Abs(pos.y - groundHeight);  // Distance to ground

        // Allow jump if grounded or close enough to ground (for ledge jumps)
        if (isGrounded || groundDistance <= jumpGroundThreshold)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;  // Set upward speed
                isHoldingJump = true;  // Start holding jump
            }
        }

        // Stop holding jump when key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isHoldingJump = false;
        }
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;  // Current position

        if (!isGrounded)
        {
            // Increase hold timer if jump key is still held
            if (isHoldingJump)
            {
                holdJumpTimer += Time.fixedDeltaTime;

                // Stop holding jump if max time reached
                if (holdJumpTimer >= maxHoldJumpTime)
                {
                    isHoldingJump = false;
                }
            }

            pos.y += velocity.y * Time.fixedDeltaTime;  // Move based on velocity

            // Apply gravity only if not holding jump (for realistic jump cutoff)
            if (!isHoldingJump)
            {
                velocity.y += gravity * Time.fixedDeltaTime;
            }

            // Check if player reached or fell below ground level
            if (pos.y <= groundHeight)
            {
                pos.y = groundHeight;  // Reset to ground level
                isGrounded = true;  // Set grounded status
                holdJumpTimer = 0;  // Reset jump hold timer
            }
        }
       
        // Distance Tracking: Increment total horizontal distance based on current velocity
        distance += velocity.x * Time.fixedDeltaTime;

        // Horizontal Movement Logic: Accelerate and cap speed when grounded
        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;  // Ratio of current speed to max speed
            acceleration = maxAcceleration * (1 - velocityRatio);  // Decrease acceleration as speed increases

            velocity.x += acceleration * Time.fixedDeltaTime;  // Update horizontal velocity

            // Cap horizontal speed to prevent exceeding max velocity
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }
        }

        transform.position = pos;  // Update player position
    }
}
