using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 1f;
    public float maxInitialAngle = 1f;  // Increased the max initial angle for more unpredictability
    public float maxStartY = 4f;
    public float speedMultiplier = 1.1f;

    // Ghost effect variables
    public SpriteRenderer ballSprite;
    public float visibleDuration = 2f;   // Time ball stays visible
    public float invisibleDuration = 1f; // Time ball stays invisible
    public float fadeDuration = 0.5f;    // Time it takes to fade in/out
    private bool isFading = false;

    void Start()
    {
        GameManager.instance.onReset += ResetBall;
        GameManager.instance.gameUI.onStartGame += ResetBall;

        if (ballSprite == null)  // If not assigned in inspector, get SpriteRenderer component from the GameObject
        {
            ballSprite = GetComponent<SpriteRenderer>();
        }
    }

    private void ResetBall()
    {
        ResetBallPosition();
        if (GameManager.instance.isGameStarted)
        {
            InitialPush();
            StartGhostEffect();  // Start the ghost effect only when the game has started
        }
        else
        {
            StopGhostEffect();  // Stop the ghost effect when the game hasn't started
        }
    }

    void Update()
    {
        if (GameManager.instance.isGameStarted)
        {
            ChangeDirection();
        }
        else
        {
            rb2d.velocity = Vector2.zero;
        }
    }

    private void InitialPush()
    {
        // Giving the ball an unpredictable initial push with a wide range of angles
        Vector2 dir = Random.value < 0.5f ? Vector2.left : Vector2.right;
        dir.y = Random.Range(-maxInitialAngle, maxInitialAngle);
        rb2d.velocity = dir * moveSpeed;
    }

    private void ChangeDirection()
    {
        // Significantly altered the current velocity to create an even more unpredictable "ghostly" movement
        rb2d.velocity += new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f));
        rb2d.velocity = rb2d.velocity.normalized * moveSpeed;
    }

    private void ResetBallPosition()
    {
        // Resets the ball's position after a score
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(0f, posY);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Handles scoring when the ball enters a score zone
        ScoreZone scoreZone = collision.GetComponent<ScoreZone>();
        if (scoreZone)
        {
            GameManager.instance.OnScoreZoneReached(scoreZone.id);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PaddleMovement paddle = collision.collider.GetComponent<PaddleMovement>();
        if (paddle)
        {
            GameManager.instance.gameAudio.PlayPaddleSound();
            rb2d.velocity *= speedMultiplier;

            // Trigger screen shake
            CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
            if (cameraShake != null)
            {
                StartCoroutine(cameraShake.Shake(0.1f, 0.07f)); //Can adjust duration and magnitude/strength of shake
            }
        }
    }

    // --- Ghost Fading Effect Methods ---

    private void StartGhostEffect()
    {
        if (!isFading)
        {
            StartCoroutine(GhostEffectCoroutine());
        }
    }

    private void StopGhostEffect()
    {
        StopAllCoroutines();  // Stops the "ghost" effect when the game is not started
        isFading = false;
        ballSprite.color = new Color(1f, 1f, 1f, 1f); // Make sure the ball stays visible
    }

    private IEnumerator GhostEffectCoroutine()
    {
        isFading = true;

        while (true)  // Infinite loop to continue the effect
        {
            // Fade out (ball disappears)
            yield return Fade(1f, 0f, fadeDuration);
            yield return new WaitForSeconds(invisibleDuration);

            // Fade in (ball reappears)
            yield return Fade(0f, 1f, fadeDuration);
            yield return new WaitForSeconds(visibleDuration);
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        Color ballColor = ballSprite.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            ballColor.a = alpha;
            ballSprite.color = ballColor;
            yield return null;
        }

        ballColor.a = endAlpha;  
        ballSprite.color = ballColor;
    }
}