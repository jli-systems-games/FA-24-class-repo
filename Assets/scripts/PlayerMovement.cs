using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float boostAmount = 2f;
    public float speedDecay = 1f;
    private float currentSpeed = 0f;
    private bool isMashing = false;

    public float jumpForce = 3f;
    private bool isGrounded = false;

    public float speedBoostDuration = 2f;
    public float speedBoostAmount = 5f;

    private bool hasSpeedBoost = false;
    private float speedBoostTimer = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
        SpeedBoost();
    }

    void FixedUpdate()
    {
        MovePlayer();
        DecaySpeed();
    }

    void KeyInput()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            isMashing = true;
            // increases speed when pressed
            currentSpeed += boostAmount * Time.deltaTime;
        }
        else
        {
            isMashing = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);    }

    void DecaySpeed()
    {
        if (!isMashing && currentSpeed > 0)
        {
            currentSpeed -= speedDecay * Time.deltaTime;
            if (currentSpeed < 0)
            {
                currentSpeed = 0; // no negative speed
            }
        }
    }

    void MovePlayer()
    {
        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void SpeedBoost()
    {
        if (hasSpeedBoost)
        {
            speedBoostTimer -= Time.deltaTime;
            if (speedBoostTimer <= 0)
            {
                hasSpeedBoost = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SpeedBoost"))
        {
            CollectSpeedBoost();
            Destroy(collision.gameObject);
        }
    }

    void CollectSpeedBoost()
    {
        hasSpeedBoost = true;
        speedBoostTimer = speedBoostDuration;
        currentSpeed += speedBoostAmount;
    }
}