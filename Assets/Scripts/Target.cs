using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float movementRange = 1f;   // The range of the up and down movement
    public float speed = 1f;           // The speed of the movement
    public int outerPoints = 1;        // Points for hitting the outer area
    public int bullseyePoints = 2;     // Points for hitting the bullseye

    private Vector3 initialPosition;
    private bool isPaused = false;     // Controls whether the target moves

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        // Only move when not paused
        if (!isPaused)
        {
            float yOffset = Mathf.Sin(Time.time * speed) * movementRange;
            transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);
        }
    }

    // Method to pause or resume movement
    public void SetPaused(bool pause)
    {
        isPaused = pause;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife")) // Ensure the knife has the tag "Knife"
        {
            // Check if the collision is with the bullseye
            if (collision.otherCollider.CompareTag("Bullseye"))
            {
                Debug.Log("Bullseye hit!");
                ScoreManager.instance.AddScore(bullseyePoints); // Add bullseye points
            }
            else if (collision.otherCollider.CompareTag("OuterTarget"))
            {
                Debug.Log("Outer target hit!");
                ScoreManager.instance.AddScore(outerPoints); // Add outer target points
            }
        }
    }
}
