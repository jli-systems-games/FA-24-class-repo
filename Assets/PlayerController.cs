using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    PlayerInput playerInput;
    InputAction moveAction;

    public float sliderSpeed = 1f;
    public Rigidbody rb;

    public float ballSpeed;

    public Vector3 startPosition;

    public InputActionReference inputMove;

    public Vector3 topLimit;
    public Vector3 botLimit;


    private void Start()
    {
        startPosition = transform.position;

        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        moveAction = playerInput.actions.FindAction("Move");
        moveAction.Enable();
    }

    private void Update()
    {
        if (transform.position.z > topLimit.z)
        {

            transform.position = new Vector3(transform.position.x, 0, topLimit.z);
        }

        if (transform.position.z < botLimit.z)
        {

            transform.position = new Vector3(transform.position.x, 0, botLimit.z);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(direction.x, 0, direction.y) * sliderSpeed;

        rb.MovePosition(rb.position + move);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
