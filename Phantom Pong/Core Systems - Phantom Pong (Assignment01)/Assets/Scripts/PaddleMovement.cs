using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public int id;
    public float moveSpeed = 2f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        GameManager.instance.onReset += ResetPosition;
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
        rb2d.velocity = Vector2.zero;  // Ensure the paddle stops moving on reset
    }

    private void Update()
    {
        if (GameManager.instance.isGameStarted)  // Check if the game has started
        {
            float movement = ProcessInput();
            Move(movement);
        }
        else
        {
            rb2d.velocity = Vector2.zero;  // Stop paddle movement if the game hasn't started
        }
    }

    private float ProcessInput()
    {
        float movement = 0f;
        switch (id)
        {
            case 1:
                movement = Input.GetAxis("MovePlayer1");
                break;

            case 2:
                movement = Input.GetAxis("MovePlayer2");
                break;
        }

        return movement;
    }

    private void Move(float movement)
    {
        Vector2 velo = rb2d.velocity;
        velo.y = moveSpeed * movement;
        rb2d.velocity = velo;
    }
}
