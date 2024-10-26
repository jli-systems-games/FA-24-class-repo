using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallImageManager : MonoBehaviour
{
    public GameObject ball; // Reference to the ball object
    public Sprite stage1Image; // Sprite for stage 1
    public Sprite stage2Image; // Sprite for stage 2

    private int collisionCount = 0; // Count of collisions detected
    private const int maxCollisions = 3; // Maximum number of scores before disabling ball
    private SpriteRenderer ballSpriteRenderer; // SpriteRenderer to change ball image

    private void Start()
    {
        // Ensure the ball is active at the start
        if (ball != null)
        {
            ball.SetActive(true);
            ballSpriteRenderer = ball.GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        }
    }

    // Call this method when a player scores a goal or when addUp detects a collision
    public void OnScore()
    {
        Debug.Log("A score has been detected...");
        collisionCount++; // Increment the score count

        // Update the ball image based on the collision count
        UpdateBallImage(); // Update the ball image

        // If the max number of scores is reached, disable the ball
        if (collisionCount >= maxCollisions)
        {
            DisableBall(); // Disable the ball
        }
    }

    private void UpdateBallImage()
    {
        // Change the ball image based on collision count
        if (collisionCount == 1)
        {
            ballSpriteRenderer.sprite = stage1Image; // Set the first stage image
            Debug.Log("Ball image updated to Stage 1.");
        }
        else if (collisionCount == 2)
        {
            ballSpriteRenderer.sprite = stage2Image; // Set the second stage image
            Debug.Log("Ball image updated to Stage 2.");
        }
    }

    private void DisableBall()
    {
        if (ball != null)
        {
            ball.SetActive(false); // Disable the ball object
            Debug.Log("Ball disabled after " + maxCollisions + " scores.");
        }
    }
}
