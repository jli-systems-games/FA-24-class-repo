using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public GameManager gameManager;
    public Rigidbody2D rb2d;
    public float moveSpeed = 1f;
    public float maxInitialAngle = 1f;  // Increased the max initial angle for more unpredictability
    public float maxStartY = 4f;

    void Start()
    {
        InitialPush();
    }

    void Update()
    {
        // More significantly alter the current direction every frame to simulate unpredictable ghostly behavior
        ChangeDirection();
    }

    private void InitialPush()
    {
        // Give the ball a more unpredictable initial push with a wider range of angles
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }

    private void ChangeDirection()
    {
        // Significantly alter the current velocity to create an even more unpredictable "ghostly" movement
        rb2d.velocity += new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        rb2d.velocity = rb2d.velocity.normalized * moveSpeed;
    }

    private void ResetBall()
    {
        // Reset the ball's position after a score
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(0f, posY);
        transform.position = position;

        // Re-initialize movement
        InitialPush();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handle scoring when the ball enters a score zone
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            gameManager.OnScoreZoneReached(scoreZone.id);
            ResetBall();
        }
    }
}