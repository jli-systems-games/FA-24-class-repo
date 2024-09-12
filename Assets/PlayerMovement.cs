using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(0, 0, 70f, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(70f, 0, 0, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(0, 0, -70f, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(-70f, 0, 0, ForceMode.Impulse);
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
