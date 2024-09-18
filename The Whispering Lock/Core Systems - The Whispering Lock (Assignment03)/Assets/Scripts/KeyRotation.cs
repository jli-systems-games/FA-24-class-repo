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

    public Sprite lockedSprite; // The locked state sprite
    public Sprite unlockedSprite; // The unlocked state sprite
    private bool spriteIsLocked = true; // Flag to track if sprite is in the locked state

    public SpriteRenderer lockSpriteRenderer; // SpriteRenderer for the lock (not the key)

    private SpriteRenderer spriteRenderer; // SpriteRenderer component reference

    private AudioManager audioManager; // Reference to the AudioManager

    void Start()
    {
        // Initialize totalRotation based on current rotation
        totalRotation = transform.eulerAngles.x % 720f;
        stuckCheckTimer = stuckInterval; // Start the stuck check timer
        Debug.Log("The Lock is locked.");

        // Find the AudioManager in the scene
        audioManager = FindObjectOfType<AudioManager>();

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the initial sprite for the lock to lockedSprite
        if (lockSpriteRenderer != null && lockedSprite != null)
        {
            lockSpriteRenderer.sprite = lockedSprite;
        }

    }

    void Update()
    {
         // Skip interactions if the key is in the Unlocked state
        if (keyState == KeyState.Unlocked)
        {
            return;
        }

        // Get the horizontal input axis (A/D or Left/Right arrows)
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

                    // Play rotation sound
                    if (audioManager != null)
                    {
                        audioManager.PlayRotationSound();
                    }

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
                    ResetLockMessageAndSprite(); // Reset the lock sprite immediately
                    if (isShaking) // Check if the key is currently shaking
                    {
                        StopCoroutine(ShakeKey()); // Stop the shaking coroutine
                        isShaking = false; // Reset shaking flag
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
                    // Play the stuck sound
                    if (audioManager != null)
                    {
                        audioManager.PlayStuckSound();
                    }

                    StartCoroutine(ShakeKey()); // Start shaking coroutine
                }
            }
            else if (keyState == KeyState.Stuck && !isInteracting) // Check if the key is stuck but not interacted with
            {
                keyState = KeyState.Locked; // Change key state to Locked
                ResetLockMessageAndSprite(); // Reset the lock sprite immediately
                if (isShaking) // Check if the key is currently shaking
                {
                    StopCoroutine(ShakeKey()); // Stop the shaking coroutine
                    isShaking = false; // Reset shaking flag
                }
                stuckCheckTimer = stuckInterval; // Reset stuck check timer
            }
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

        // Reset only the key's X-axis rotation, keep Y and Z axes unchanged
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // Reset X-axis rotation to 0
        totalRotation = 0f; // Reset total rotation tracker

        // Play unlock sound
        if (audioManager != null)
        {
            audioManager.PlayUnlockSound();
        }

        // Change the sprite of the lock to unlockedSprite
        if (lockSpriteRenderer != null && unlockedSprite != null)
        {
            lockSpriteRenderer.sprite = unlockedSprite; // Change the sprite to the unlocked version
            spriteIsLocked = false; // Set the flag to indicate the sprite is unlocked
        }

    }

    IEnumerator UnlockToLockedTransition()
    {
        // Keep unlocked state for 2 seconds (or any duration you want)
        yield return new WaitForSeconds(2f);

        // Now reset both the lock sprite and the message to Locked state
        ResetLockMessageAndSprite();
    }

    void ResetLockMessageAndSprite()
    {
        // Reset the sprite of the lock to lockedSprite
        if (lockSpriteRenderer != null && lockedSprite != null)
        {
            lockSpriteRenderer.sprite = lockedSprite;
            spriteIsLocked = true; // Set the flag to indicate the sprite is locked
        }

        // Update the unlock message only if the sprite is locked
        if (unlockMessage != null && spriteIsLocked)
        {
            unlockMessage.text = "Locked!"; // Reset the unlock message text
        }
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
}