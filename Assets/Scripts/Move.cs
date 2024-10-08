using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Move : MonoBehaviour
{
    Vector2 moveVector;
    public float moveSpeed = 1f;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        //Debug.Log(direction);
        direction.Normalize();
        transform.Translate( moveSpeed * direction * Time.deltaTime);
        //rb.MovePosition(rb.position + (moveSpeed * direction));
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
}
