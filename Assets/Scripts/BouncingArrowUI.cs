using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingArrowUI : MonoBehaviour
{
    public float bounceHeight = 10f; // How high the arrow bounces
    public float bounceSpeed = 2f;  // Speed of the bouncing

    private Vector3 originalPosition;
    private bool isBouncing = true; // Determines if the arrow should bounce

    void Start()
    {
        // Store the initial position of the arrow
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Check if the dialogue is at the last line
        if (!DialogueManager.GetInstance().currentStory.canContinue && DialogueManager.GetInstance().currentStory.currentChoices.Count == 0)
        {
            isBouncing = false;
        }
        else
        {
            isBouncing = true;
        }

        // If bouncing, move the arrow; otherwise, reset to original position
        if (isBouncing)
        {
            float newY = originalPosition.y + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
            transform.localPosition = new Vector3(originalPosition.x, newY, originalPosition.z);
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }
}
