using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    Vector3 lastVelocity;

    // New field to set the initial speed
    public Vector2 initialVelocity = new Vector2(2f, 2f); // Customize as needed

    void Awake()
    {
        rb.velocity = initialVelocity; // Give the Rigidbody an initial velocity
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);
    }
}
