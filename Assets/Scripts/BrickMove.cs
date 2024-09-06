using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickMove : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 2.0f;

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
        float step = speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, Mathf.MoveTowards(transform.position.y, targetY, step), transform.position.z);

        // Check if the object has reached the target position
        if (Mathf.Abs(transform.position.y - targetY) < 0.001f)
        {
            // If reached, set a new random target between pointA.y and pointB.y
            SetRandomTargetY();
        }
    }

    void SetRandomTargetY()
    {
        targetY = Random.Range(pointA.y, pointB.y);
    }
}