using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    Vector2 moveVector;
    public float moveSpeed = 1f;
    [SerializeField] float sensitivityX = 5f;
    [SerializeField] float sensitivityY = 0.85f;
    [SerializeField] Transform plyrCam;
    [SerializeField] float xClamp = 65f;
    float inputX, inputY;
    float xRotation = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        direction.Normalize();
        transform.Translate(moveSpeed * direction * Time.deltaTime);
        transform.Rotate(Vector3.up, inputX * Time.deltaTime);

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp - 15f);
        Vector3 plyRotation = transform.eulerAngles;
        plyRotation.x = xRotation;
        plyrCam.eulerAngles = plyRotation;
    }
    public void MovementInput(InputAction.CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }
    public void TurnInputX(InputAction.CallbackContext context)
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        inputX = context.ReadValue<float>() * sensitivityX;

    }
    public void TurnInputY(InputAction.CallbackContext context)
    {
        inputY = context.ReadValue<float>() * sensitivityY;
    }
}
