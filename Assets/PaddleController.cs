using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public InputActionReference inputMove;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Debug.Log(inputMove.action.ReadValue<Vector2>());
    }
    //all actions can be added to these if you have more
    private void OnEnable()
    {
        inputMove.action.started += Move;
        //inputPunch.action.started += Punch; //you can add more actions like this!
    }

    private void OnDisable()
    {
        inputMove.action.started -= Move;
        //inputPunch.action.started -= Punch; //if you add an action make sure you also add it here!
    }

    //the function that contains everything the “Jump” button should trigger. You must set up your function like this! 
    void Move(InputAction.CallbackContext obj)
    {
        //Debug.Log("move");
    }

}
