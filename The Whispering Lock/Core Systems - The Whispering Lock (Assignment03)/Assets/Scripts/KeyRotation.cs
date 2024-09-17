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
    public float stuckDuration = 4f; // How long the key stays stuck
    public float stuckInterval = 2f; // Interval between stuck events
    private float stuckTimer = 2f; // Timer to manage when key gets unstuck
    private float stuckCheckTimer = 0f; // Timer to manage stuck checks
    public TextMeshProUGUI unlockMessage; // Reference to a TextMeshProUGUI element
    private float totalRotation = 0f; // Tracks total Y-axis rotation

    void Start()
    {
        // Initialize totalRotation based on current rotation
        totalRotation = transform.eulerAngles.y % 360f;
        stuckCheckTimer = stuckInterval; // Start the stuck check timer
    }

    void Update()
    {
        switch (keyState)
        {
            case KeyState.Locked:
                // If the key is not stuck, allow rotation
                float rotationAmount = 0f;
                float input = Input.GetAxis("Horizontal"); // Default axis name for A/D or Left/Right arrows

                if (input != 0)
                {
                    rotationAmount = rotationSpeed * input * Time.deltaTime; // Smooth rotation
                    Debug.Log("Rotating: " + rotationAmount);

                    // Apply the rotation on the Y-axis only
                    transform.Rotate(0, rotationAmount, 0);
                    totalRotation += Mathf.Abs(rotationAmount); // Track the total rotation amount

                    // Debug log for total rotation
                    Debug.Log("Total Rotation: " + totalRotation);

                    // Check if total rotation has reached or exceeded 360 degrees (full circle)
                    if (totalRotation >= 360f)
                    {
                        Unlock();
                    }
                }
                break;

            case KeyState.Stuck:
                // Countdown for when the key is stuck
                stuckTimer -= Time.deltaTime;
                if (stuckTimer <= 0f)
                {
                    keyState = KeyState.Locked; // Unstick the key after timer runs out
                    Debug.Log("Key is now unstuck.");
                }
                break;
        }

        // Timer to handle the stuck event
        stuckCheckTimer -= Time.deltaTime;
        if (stuckCheckTimer <= 0f)
        {
            if (keyState == KeyState.Locked)
            {
                keyState = KeyState.Stuck;
                stuckTimer = stuckDuration;
                Debug.Log("Key is stuck!");
            }
            stuckCheckTimer = stuckInterval; // Reset the stuck check timer
        }
    }

    void Unlock()
    {
        Debug.Log("The lock has been unlocked!"); // Unlock event (for testing)

        // Show the unlock message using TextMeshProUGUI
        if (unlockMessage != null)
        {
            unlockMessage.text = "Unlocked!";
        }

        // Reset the rotation and total rotation tracker
        transform.rotation = Quaternion.Euler(0, 0, 0); // Reset rotation to start
        totalRotation = 0f; // Reset total rotation

        // Start a coroutine to handle the delay before resetting the message
        StartCoroutine(ResetUnlockMessage());
    }

    IEnumerator ResetUnlockMessage()
    {
        // Wait for a few seconds
        yield return new WaitForSeconds(2f);

        // Reset the unlock message
        if (unlockMessage != null)
        {
            unlockMessage.text = "Locked!";
        }
    }
}