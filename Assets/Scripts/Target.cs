using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float movementRange = 2f;         // The range of the up and down movement
    public float speed = 1f;                 // The initial speed of the movement
    public int outerPoints = 1;              // Points for hitting the outer area
    public int bullseyePoints = 2;           // Points for hitting the bullseye
    public float glowSpeed = 2f;             // Speed of the glow in/out effect
    public float minAlpha = 0.5f;            // Minimum alpha value (50% visible)
    public float speedIncreaseRate = 0.04f;  // Rate at which the speed increases over time

    private Vector3 initialPosition;
    private bool isPaused = false;           // Controls whether the target moves
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Update()
    {
        // Increase speed gradually over time, capped at a maximum of 5
        speed = Mathf.Clamp(speed + speedIncreaseRate * Time.deltaTime, 0, 5);

        // Only move when not paused
        if (!isPaused)
        {
            float yOffset = Mathf.Sin(Time.time * speed) * movementRange;
            transform.position = new Vector3(initialPosition.x, initialPosition.y + yOffset, initialPosition.z);
        }

        // Apply glow effect by adjusting the alpha, ensuring it stays within minAlpha and 1
        float alpha = ((Mathf.Sin(Time.time * glowSpeed) + 1) / 2) * (1 - minAlpha) + minAlpha;
        Color glowColor = originalColor;
        glowColor.a = alpha;
        spriteRenderer.color = glowColor;
    }

    public void SetPaused(bool pause)
    {
        isPaused = pause;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife")) // Ensure the knife has the tag "Knife"
        {
            if (collision.otherCollider.CompareTag("Bullseye"))
            {
                Debug.Log("Bullseye hit!");
                ScoreManager.instance.AddScore(bullseyePoints);
            }
            else if (collision.otherCollider.CompareTag("OuterTarget"))
            {
                Debug.Log("Outer target hit!");
                ScoreManager.instance.AddScore(outerPoints);
            }
        }
    }
}
