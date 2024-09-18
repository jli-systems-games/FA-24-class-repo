using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

// Enum for key states
public enum KeyState
{
    Locked,
    Unlocked,
    Stuck
}

public class KeyRotation : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed at which the key rotates
    public KeyState keyState = KeyState.Locked; // Current state of the key
    public float stuckDuration = 2f; // How long the key stays stuck
    public float stuckInterval = 4f; // Interval between stuck events
    private float stuckTimer = 2f; // Timer to manage when key gets unstuck
    private float stuckCheckTimer = 0f; // Timer to manage stuck checks
    public TextMeshProUGUI unlockMessage; // Reference to a TextMeshProUGUI element
    private float totalRotation = 0f; // Tracks total Y-axis rotation

    private bool isShaking = false; // Check if the key is currently shaking
    private bool isInteracting = false; // Check if the key is being interacted with

    void Start()
    {
        // Initialize totalRotation based on current rotation
        totalRotation = transform.eulerAngles.y % 720f;
        stuckCheckTimer = stuckInterval; // Start the stuck check timer
        Debug.Log("The Lock is locked.");
    }

    void Update()
    {
         // Skip interactions if the key is in the Unlocked state
        if (keyState == KeyState.Unlocked)
        {
            return;
        }

        // Get the horizontal input axis (typically A/D or Left/Right arrows)
        float input = Input.GetAxis("Horizontal");

        // Determine if the player is interacting with the key
        isInteracting = input != 0;

        switch (keyState)
        {
            case KeyState.Locked:
                // If the key is in the Locked state

                if (isInteracting) // Check if player is interacting
                {
                    // Calculate the amount of rotation based on input and rotation speed
                    float rotationAmount = rotationSpeed * input * Time.deltaTime;

                    // Smooth rotation
                    // Debug.Log("Rotating: " + rotationAmount);

                    transform.Rotate(0, rotationAmount, 0); // Apply the rotation to the Y-axis
                    totalRotation += Mathf.Abs(rotationAmount); // Accumulate total rotation

                    // Debug log for total rotation
                    // Debug.Log("Total Rotation: " + totalRotation);

                    if (totalRotation >= 720f) // Check if a full rotation has been completed
                    {
                        Unlock(); // Unlock the lock
                    }
                }
                break;

            case KeyState.Stuck:
                // If the key is in the Stuck state
                stuckTimer -= Time.deltaTime; // Decrement the stuck timer
                if (stuckTimer <= 0f) // Check if the stuck duration has expired
                {
                    keyState = KeyState.Locked; // Change the key state to Locked
                    if (isShaking) // Check if the key is currently shaking
                    {
                        StopCoroutine(ShakeKey()); // Stop the shaking coroutine
                        isShaking = false; // Reset shaking flag
                        transform.position = Vector3.zero; // Optionally reset position if necessary
                    }
                }
                break;
        }

        // Update stuck check timer
        stuckCheckTimer -= Time.deltaTime;
        if (stuckCheckTimer <= 0f)
        {
            if (keyState == KeyState.Locked && isInteracting) // Check if the key should get stuck
            {
                keyState = KeyState.Stuck; // Change key state to Stuck
                stuckTimer = stuckDuration; // Set the stuck timer
                if (!isShaking) // Check if the key is not currently shaking
                {
                    StartCoroutine(ShakeKey()); // Start shaking coroutine
                }
            }
            else if (keyState == KeyState.Stuck && !isInteracting) // Check if the key is stuck but not interacted with
            {
                keyState = KeyState.Locked; // Change key state to Locked
                if (isShaking) // Check if the key is currently shaking
                {
                    StopCoroutine(ShakeKey()); // Stop the shaking coroutine
                    isShaking = false; // Reset shaking flag
                    transform.position = Vector3.zero; // Optionally reset position if necessary
                }
                stuckCheckTimer = stuckInterval; // Reset stuck check timer
            }
            stuckCheckTimer = stuckInterval; // Reset stuck check timer
        }
    }

    void Unlock()
    {
        Debug.Log("The lock has been unlocked!"); // Unlock event (for testing)
        // Ensure no shaking occurs
        // When the key is unlocked
        if (unlockMessage != null) // Check if the unlockMessage reference is set
        {
            unlockMessage.text = "Unlocked!"; // Update the unlock message text
            StopCoroutine(ShakeKey()); // Stop the shaking coroutine if it’s running
            isShaking = false; // Reset shaking flag
        }

        transform.rotation = Quaternion.Euler(0, 0, 0); // Reset the key's rotation
        totalRotation = 0f; // Reset total rotation tracker

        StartCoroutine(ResetUnlockMessage()); // Start coroutine to reset unlock message after a delay
    }

    IEnumerator ShakeKey()
    {
        isShaking = true; // Set shaking flag to true
        float shakeDuration = 1f; // Duration of the shake effect
        float shakeMagnitude = 0.02f; // Magnitude of the shake effect

        Vector3 originalPosition = transform.position; // Store the original position of the key

        float elapsedTime = 0f; // Timer for shake duration
        while (elapsedTime < shakeDuration && isInteracting) // Continue shaking while interacting
        {
            // Random offset for shaking effect
            float xOffset = Random.Range(-shakeMagnitude, shakeMagnitude);
            float yOffset = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = originalPosition + new Vector3(xOffset, yOffset, 0); // Apply offset to position

            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait until the next frame
        }

        transform.position = originalPosition; // Reset position to original
        isShaking = false; // Reset shaking flag
    }

    IEnumerator ResetUnlockMessage()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds

        if (unlockMessage != null) // Check if the unlockMessage reference is set
        {
            unlockMessage.text = "Locked!"; // Reset the unlock message text

            // To check if the game/key & lock has been reset
            Debug.Log("Game has been Reset!");
        }
    }
}