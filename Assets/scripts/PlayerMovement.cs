using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float boostAmount = 2f;
    public float speedDecay = 1f;
    private float currentSpeed = 0f;
    private bool isMashing = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        KeyInput();
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
    }

    void DecaySpeed()
    {
        if (!isMashing)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= speedDecay * Time.deltaTime;
                if (currentSpeed < 0)
                {
                    currentSpeed = 0; // no negative speed
                }
            }
        }
    }

    void MovePlayer()
    {
        Vector2 movement = new Vector2(currentSpeed * Time.fixedDeltaTime, 0);
        rb.MovePosition(rb.position + movement);
    }
}