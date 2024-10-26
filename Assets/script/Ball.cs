using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed; // Initial speed of the ball
    public float speedIncreaseFactor; // Factor by which speed will increase over time
    public Rigidbody2D rb; // Reference to the Rigidbody2D component
    public Vector3 startPosition; // Starting position of the ball

    private float currentSpeed; // Current speed of the ball
    private float timeElapsed; // Time elapsed since the ball was launched
    private int triggerCollisionCount; // Count of collisions with trigger objects
    private int resetCount; // Counter for resets

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; // Store the starting position
        currentSpeed = initialSpeed; // Set the current speed to the initial speed
        timeElapsed = 0; // Initialize elapsed time
        triggerCollisionCount = 0; // Initialize the trigger collision counter
        resetCount = 0; // Initialize the reset counter
        StartCoroutine(LaunchAfterDelay()); // Launch the ball after a delay
    }

    // Method to reset the ball's position and speed
    public void Reset()
    {
        rb.velocity = Vector2.zero; // Stop the ball's movement
        transform.position = startPosition; // Reset position to the starting point
        currentSpeed = initialSpeed; // Reset the current speed to the initial speed
        timeElapsed = 0; // Reset elapsed time
        triggerCollisionCount = 0; // Reset trigger collision counter
        resetCount++; // Increment reset counter
        StartCoroutine(LaunchAfterDelay()); // Launch the ball after a delay
    }

    // Method to get the number of resets
    public int GetResetCount()
    {
        return resetCount; // Return the reset count
    }

    // Coroutine to launch the ball after a specified delay
    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(0.6f); // Wait for 0.6 seconds
        Launch(); // Launch the ball
    }

    // Method to launch the ball in a random direction
    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1; // Randomly choose -1 or 1 for x direction
        float y = Random.Range(0, 2) == 0 ? -1 : 1; // Randomly choose -1 or 1 for y direction
        rb.velocity = new Vector2(currentSpeed * x, currentSpeed * y); // Set the initial velocity
    }

    // Update is called once per frame
    void Update()
    {
        timeElapsed += Time.deltaTime; // Increment the elapsed time
        currentSpeed = initialSpeed + speedIncreaseFactor * timeElapsed; // Update the current speed based on elapsed time
        rb.velocity = rb.velocity.normalized * currentSpeed; // Apply the current speed to the ball's velocity
    }

    // Method to handle trigger collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger")) // Check if the collided object has the tag "Trigger"
        {
            triggerCollisionCount++; // Increment the trigger collision count
            if (triggerCollisionCount >= 3) // Check if the count reaches 5
            {
                Reset(); // Reset the ball
               // resetCount++;
            }
        }
    }
}
