using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float gravity;  // Gravity applied when in the air
    public Vector2 velocity;  // Player's movement speed and direction
    public float maxXVelocity = 32;// Cap on horizontal speed to prevent infinite acceleration
    public float maxAcceleration = 5; // Maximum rate of acceleration for player movement
    public float acceleration = 5; // Current acceleration value used for horizontal movement
    public float distance = 0; // Tracks the total horizontal distance traveled by the player
    public float jumpVelocity = 50;  // Initial speed when jumping

    public float groundHeight = -2.5f;  // Y-coordinate for ground level
    public bool isGrounded = false;  // Tracks if player is on the ground

    public bool isHoldingJump = false;  // Tracks if jump key is held
    public float maxHoldJumpTime = 0.4f;  // Maximum time jump key can affect jump height
    public float maxMaxHoldJumpTime = 0.04f;
    public float holdJumpTimer = 0.0f;  // Timer to track how long jump is held

    public float jumpGroundThreshold = 1;  // Distance from ground where player can still jump

    public bool isDead = false;

    public LayerMask groundLayerMask;
    public LayerMask obstacleLayerMask;

    GroundFall fall;
    CameraController cameraController;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
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

                if (fall != null)
                {
                    fall.player = null;
                    fall = null;
                    cameraController.StopShaking();
                }
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

        if (isDead)
        {
            return;
        }

        if (pos.y < -7)
        {
            isDead = true;
        }

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

            Vector2 rayOrigin = new Vector2(pos.x + 0.5f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, groundLayerMask);
            if (hit2D.collider != null)
            {
                Ground ground = hit2D.collider.GetComponent<Ground>();
                if (ground != null)
                {
                    if (pos.y >= ground.groundHeight)
                    {
                        groundHeight = ground.groundHeight;
                        pos.y = groundHeight;  // Reset to ground level
                        velocity.y = 0;
                        isGrounded = true;  // Set grounded status
                        holdJumpTimer = 0;  // Reset jump hold timer
                    }

                    fall = ground.GetComponent<GroundFall>();
                    if (fall != null)
                    {
                        fall.player = this;
                        cameraController.StartShaking();
                    }
                }
                Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.red);

                Vector2 wallOrigin = new Vector2(pos.x, pos.y);
                RaycastHit2D wallHit = Physics2D.Raycast(wallOrigin, Vector2.right, velocity.x * Time.fixedDeltaTime, groundLayerMask);
                if (wallHit.collider != null)
                {
                    Ground wallGround = wallHit.collider.GetComponent<Ground>();  // Renamed variable
                    if (wallGround != null)
                    {
                        if (pos.y < wallGround.groundHeight)
                        {
                            velocity.x = 0;
                        }
                    }
                }
            }
        }

        // Distance Tracking: Increment total horizontal distance based on current velocity
        distance += velocity.x * Time.fixedDeltaTime;

        // Horizontal Movement Logic: Accelerate and cap speed when grounded
        if (isGrounded)
        {
            float velocityRatio = velocity.x / maxXVelocity;  // Ratio of current speed to max speed
            acceleration = maxAcceleration * (1 - velocityRatio);  // Decrease acceleration as speed increases
            maxHoldJumpTime = maxMaxHoldJumpTime * velocityRatio;

            velocity.x += acceleration * Time.fixedDeltaTime;  // Update horizontal velocity

            // Cap horizontal speed to prevent exceeding max velocity
            if (velocity.x >= maxXVelocity)
            {
                velocity.x = maxXVelocity;
            }

            Vector2 rayOrigin = new Vector2(pos.x - 0.5f, pos.y);
            Vector2 rayDirection = Vector2.up;
            float rayDistance = velocity.y * Time.fixedDeltaTime;
            if (fall != null)
            {
                rayDistance = -fall.fallSpeed * Time.fixedDeltaTime;
            }
            RaycastHit2D hit2D = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance);
            if (hit2D.collider == null)
            {
                isGrounded = false;  // Set grounded status
            }
            Debug.DrawRay(rayOrigin, rayDirection * rayDistance, Color.yellow);
        }

        // Obstacle Detection (Horizontal)
        Vector2 obstOrigin = new Vector2(pos.x, pos.y);
        float rayDistanceX = Mathf.Max(velocity.x * Time.fixedDeltaTime, 1.0f); // Minimum ray length of 1.0f
        RaycastHit2D obstHitX = Physics2D.Raycast(obstOrigin, Vector2.right, rayDistanceX, obstacleLayerMask);

        if (obstHitX.collider != null)
        {
            Obstacle obstacle = obstHitX.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }

        // Obstacle Detection (Vertical)
        RaycastHit2D obstHitY = Physics2D.Raycast(obstOrigin, Vector2.up, velocity.y * Time.fixedDeltaTime, obstacleLayerMask);
        if (obstHitY.collider != null)
        {
            Obstacle obstacle = obstHitY.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                hitObstacle(obstacle);
            }
        }

        transform.position = pos;  // Update player position
    }

    void hitObstacle(Obstacle obstacle)
    {
        Debug.Log("Obstacle hit! Reducing velocity.");
        Destroy(obstacle.gameObject);
        velocity.x *= 0.6f;

    }
}