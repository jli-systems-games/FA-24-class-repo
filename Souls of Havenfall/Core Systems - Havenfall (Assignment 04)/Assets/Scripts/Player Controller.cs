using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    private float translation;
    private float straffe;
    private Rigidbody rb;

    void Start()
    {
        // Lock the cursor to the screen
        Cursor.lockState = CursorLockMode.Locked;

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get input for movement
        translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        straffe = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        // Debug log to check input
        Debug.Log($"Translation: {translation}, Strafe: {straffe}");

        // Unlock the cursor if the Escape key is pressed
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void FixedUpdate()
    {
        // Move the player using Rigidbody physics
        Vector3 movement = new Vector3(straffe, 0, translation);
        rb.MovePosition(rb.position + transform.TransformDirection(movement));
    }
}
