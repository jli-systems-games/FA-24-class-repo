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
    [SerializeField] GameObject chicken, food, rebelProgress;
    refuteProgress _progressStats;
    float inputX, inputY;
    float xRotation = 0f;
    bool beingAttacked = false;
    public bool tuH, tuF,tuR;
    int progress = 0;
    void Start()
    {
        eventManager.triggerAttack += gettingLured;
        eventManager.resetAttack += resetingPly;
        _progressStats = rebelProgress.GetComponent<refuteProgress>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3(moveVector.x, 0, moveVector.y);
        direction.Normalize();
        transform.Translate(moveSpeed * direction * Time.deltaTime);

        if (!beingAttacked)
        {
            transform.Rotate(Vector3.up, inputX * Time.deltaTime);

            xRotation -= inputY;
            xRotation = Mathf.Clamp(xRotation, -xClamp, xClamp - 15f);
            Vector3 plyRotation = transform.eulerAngles;
            plyRotation.x = xRotation;
            plyrCam.eulerAngles = plyRotation;
        }
       
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
    public void pickUpChicken(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!beingAttacked && !tuF)
            {
                eventManager.switchItem(chicken);
            }
            else if(!tuR)
            {
                //call the refute function;
                refuting();
            }
            
        }
    }
    public void pickUpFood(InputAction.CallbackContext context)
    {
        if (context.started && !tuH)
        {
            if (!beingAttacked)
            {
                eventManager.switchItem(food);

            }
            
        }
    }
    void gettingLured(GameObject trigger)
    {
        if (trigger.name == "hunger" || trigger.name == "irritation")
        { 
            beingAttacked = true;
            rebelProgress.SetActive(true);
        }
        //beingAttacked and progress will both get reseted;
    }
    void refuting()
    {

        progress++;
        //depend on input one rotate left (-) one rotate right (+) ;

        transform.Rotate(Vector3.up, progress * sensitivityX);
        _progressStats.UpdateProgress(progress, 10f);
    }
    void resetingPly()
    {
        beingAttacked = false;
        progress = 0;
        rebelProgress.SetActive(false);
    }
}
