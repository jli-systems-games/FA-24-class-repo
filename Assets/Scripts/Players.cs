using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Players : MonoBehaviour
{
    public InputActionReference Movement;

    Vector3 moveDirection = Vector3.zero;
    int moveSpeed = 3;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   moveDirection = Movement.action.ReadValue<Vector2>();
        Debug.Log(Movement.action.ReadValue<Vector2>());
    }
    private void OnDisable()
    {
        Movement.action.started -= moveAround;
    }

    private void OnEnable()
    {
        Movement.action.started += moveAround;
    }

    void moveAround(InputAction.CallbackContext obj)
    {
       
        
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
}
