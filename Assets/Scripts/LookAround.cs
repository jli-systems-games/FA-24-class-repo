using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class LookAround : MonoBehaviour
{
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
    void Update()
    {
        //Debug.Log("xroate :" + inputX);
        transform.Rotate(Vector3.up, inputX *  Time.deltaTime);

        //Debug.Log("yroate :" + inputY);
        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp - 15f);
        Vector3 plyRotation = transform.eulerAngles;
        plyRotation.x = xRotation;
        plyrCam.eulerAngles = plyRotation;
    }
    public void TurnInputX(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<float>() * sensitivityX;

    }
    public void TurnInputY(InputAction.CallbackContext context)
    {
        inputY = context.ReadValue<float>() * sensitivityY;
    }
    public void stickTurnX(InputAction.CallbackContext context)
    {
        float modedX = context.ReadValue<float>() * 10f;
        inputX = modedX * sensitivityX;
    }

   
}
