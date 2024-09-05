using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMove : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;

    private float targetY;
    private bool playerStart = false; // Track if the player has pressed a key

    void Start()
    {
        // Initialize boundaries for y-axis movement
        pointA = new Vector3(transform.position.x, -4, transform.position.z);
        pointB = new Vector3(transform.position.x, 4, transform.position.z);
    }

    void Update()
    {
        // Check for player input (WASD or Arrow keys)
        if (!playerStart)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                playerStart = true; // Start movement once player presses a key
                SetRandomTargetY();     // Set an initial target for the bricks
            }
        }

        // If player has moved, start brick movement
        if (playerStart)
        {
            float step = speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetY, step), transform.position.z);

            if (Mathf.Abs(transform.position.y - targetY) < 0.001f)
            {
                SetRandomTargetY();
            }
        }
    }

    void SetRandomTargetY()
    {
        targetY = Random.Range(pointA.y, pointB.y);
    }
}

