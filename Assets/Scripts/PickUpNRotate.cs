using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class PickUpNRotate : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Grabbable _grabbable;
    ChangeSeason _seasons;
    FridgeAnimation _fridge;
    [SerializeField] Transform holdPoint;
    [SerializeField] Transform plyCamera;
    [SerializeField] LayerMask pickUplayermask;
    [SerializeField] TMP_Text instruction;

    Vector2 inputVect;
    bool pickedUP, turningKnob, allowtoTurn;
    void Start()
    {
        EventManager.enableThermoStat += EnableKnob; 
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(plyCamera.position, plyCamera.forward);
        Debug.DrawRay(plyCamera.position, plyCamera.forward * 15, Color.green);
        if (pickedUP && hit.transform != null)
        {
            Vector3 rotationDirection = new Vector3(inputVect.y, inputVect.x, 0);
            Debug.Log("inputY" + inputVect.y);
            hit.transform.Rotate(rotationDirection);
        }
        if (Physics.Raycast(ray, out hit, 15f, pickUplayermask))
        {
            if(hit.transform.TryGetComponent(out _grabbable)){
                //regular object
                instruction.text = "press f to pick up";

            }else if (hit.transform.TryGetComponent(out _fridge) && allowtoTurn)
            {
                instruction.text = "press f to open and g to close";
            }
        }

    }
    public void PickUpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(!pickedUP)
            {
                if (Physics.Raycast(ray, out hit, 15f, pickUplayermask))
                {
                    
                    if (hit.transform.TryGetComponent(out _grabbable))
                    {

                        _grabbable.Grab(holdPoint, plyCamera);

                        pickedUP = true;
                        turningKnob = false;
                        //Debug.Log(_grabbable);
                    }
                    else if (hit.transform.TryGetComponent(out _seasons) && allowtoTurn)
                    {
                        //turn on ui saying to use d pad to turn.
                        //Debug.Log("turning"); 
                        instruction.text = "use arrow keys to rotate";
                        turningKnob = true;
                    }
                    else if (hit.transform.TryGetComponent(out _fridge) && allowtoTurn)
                    {
                        bool opening = true;
                        
                        _fridge.doorOpens(opening);
                    }
                }
            }
            else
            {
                Debug.Log("dropping");
                instruction.text = string.Empty;
                _grabbable.Drop();
                pickedUP = false;
                turningKnob = false;
            }
            
        }
    }

    public void RotateInput(InputAction.CallbackContext context)
    {
        if (pickedUP)
        {
            Debug.Log("trying to rotate");
           
            inputVect = context.ReadValue<Vector2>();
        }
        else if (turningKnob)
        {
           
            if (context.performed)
            {
                instruction.text = "use left and right arrows to turn left and right";
                float turns  = context.ReadValue<Vector2>().x;
                
                _seasons.seasonSwitch(turns);
            
            }
            
            
        }
        
    }
    public void NegativeInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (Physics.Raycast(ray, out hit, 15f, pickUplayermask))
            {   
                if (hit.transform.TryGetComponent(out _fridge) && allowtoTurn)
                {   
                    
                    bool opening = false;
                    _fridge.doorOpens(opening);
                }
            }
        }
    }
    private void EnableKnob()
    {   

        allowtoTurn = true;
    }
    public void DisableKnob()
    {
        turningKnob = false;
    }
}
