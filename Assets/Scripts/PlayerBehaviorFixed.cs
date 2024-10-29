using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviorFixed : MonoBehaviour
{
    Vector2 moveVector;
    public float forwardForce = 10f, upwardForce = 40f;
    public Rigidbody target, stomach;

    Transform COM;
    void Start()
    {
        COM = target.GetComponentInChildren<Transform>();
        target.centerOfMass = COM.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stomach.AddForce(new Vector3(0, upwardForce, 0));

        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        target.AddForce(direction *  forwardForce);
        //stomach.AddForce(direction * forwardForce);
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
}
