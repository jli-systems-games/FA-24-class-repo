using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Move : MonoBehaviour
{
    Vector2 moveVector;
    public float moveSpeed = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        //Debug.Log(direction);
        direction.Normalize();
        transform.Translate(moveSpeed * direction * Time.deltaTime);
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
}
