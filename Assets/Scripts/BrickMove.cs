using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMove : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;
    public float sizeIncrease = 0.2f;

    private bool hasPlayerMoved = false;
    private float targetY;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize boundaries for y-axis movement
        pointA = new Vector3(transform.position.x, -4, transform.position.z); // Lower boundary
        pointB = new Vector3(transform.position.x, 4, transform.position.z); // Upper boundary

        // Set an initial random target
        SetRandomTargetY();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for player input (WASD or Arrow keys)
        if (!hasPlayerMoved)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                hasPlayerMoved = true; // Start movement once player presses a key
                SetRandomTargetY();     // Set an initial target for the bricks
            }
        }

        // If player has moved, start brick movement
        if (hasPlayerMoved)
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

    public void IncreaseSpeed()
    {
        speed += 1f;
        Debug.Log("Speed is" + speed);
    }

    public void IncreaseSize()
    {
        Vector3 newSize = transform.localScale + new Vector3(sizeIncrease, sizeIncrease, sizeIncrease);
        transform.localScale = newSize;
    }
}
