using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class controller : MonoBehaviour
{

    public float speed = 12f;

    private PlayerInput playerInput;

    public Rigidbody rb;

    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    
        playerInputActions.player1.Enable();
        playerInputActions.player1.Move.performed += Move_performed;
   }

    private void Move_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        //Vector2 inputVector = context.ReadValue<Vector2>();
        //float speed = 5f;
        //rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    /*
    private void Update()
    {
        Vector2 inputVector = playerInputActions.Gameplay.Movement.ReadValue<Vector2>();
        float speed = 5f;
        rb.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }
    */
   
}
