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
    [SerializeField] TMP_Text instruct;
    Vector2 inputVect;
    
    bool pickedUP, turningKnob, allowtoTurn, opening;
    void Start()
    {
        EventManager.enableThermoStat += EnableKnob;
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(plyCamera.position, plyCamera.forward);
        //Debug.DrawRay(plyCamera.position, plyCamera.forward * 15, Color.green);
        if (pickedUP && hit.transform != null)
        {
            Vector3 rotationDirection = new Vector3(inputVect.y, inputVect.x, 0);
            //Debug.Log(rotationDirection);
            hit.transform.Rotate(rotationDirection);
        }
        Debug.Log(holdPoint.childCount);
        displayUI(ray);
    }
    public void PickUpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if(!pickedUP && holdPoint.childCount == 0)
            {
                if (Physics.Raycast(ray, out hit, 15f, pickUplayermask))
                {   
                    Debug.Log("trying to pickup" + hit.transform.name);
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
                        turningKnob = true;
                    }
                    else if (hit.transform.TryGetComponent(out _fridge) && allowtoTurn)
                    {
                        opening = true;
                        _fridge.doorOpens(opening);
                    }
                }
            }
            else
            {
                Debug.Log("pickingUP" + pickedUP);
                pickedUP = _grabbable.Drop(hit.transform);
                Debug.Log("pickingUP then" + pickedUP);
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
                float turns  = context.ReadValue<Vector2>().x;
                //Debug.Log("x: " + turns);
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
                    opening = false;
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

    void displayUI(Ray _facing)
    {
        if(Physics.Raycast(_facing, out RaycastHit _outHit, 15f, pickUplayermask))
        {

            if(_outHit.transform.TryGetComponent(out _grabbable))
            {
                if (!pickedUP)
                {
                    instruct.text = "F to pick up";
                }
               
                

            }else if(_outHit.transform.TryGetComponent(out _seasons))
            {

                if (!turningKnob && allowtoTurn)
                {
                    instruct.text = "F to start turning";
                }
                else
                {
                    instruct.text = "use left and right arrow to turn";
                }

            }
            else if (_outHit.transform.TryGetComponent(out _fridge))
            {
                if (!opening && allowtoTurn)
                {
                    instruct.text = "F to open";
                }
                else
                {
                    instruct.text = "G to close";
                }
            }
            




        }else
        {
            if (!pickedUP)
            {
                instruct.text = string.Empty;
            }
            else
            {
                instruct.text = "use Arrows to rotate";
            }
                
        }
    }
}
