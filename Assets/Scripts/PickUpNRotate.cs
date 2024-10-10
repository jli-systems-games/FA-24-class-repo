using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

    Vector2 inputVect;
    bool pickedUP, turningKnob, allowtoTurn;
    void Start()
    {
        EventManager.enableThermoStat += EnableKnob;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(plyCamera.position, plyCamera.forward);
        Debug.DrawRay(plyCamera.position, plyCamera.forward * 15, Color.green);
        if (pickedUP && hit.transform != null)
        {
            Vector3 rotationDirection = new Vector3(inputVect.y, inputVect.x, 0);
            //Debug.Log(rotationDirection);
            hit.transform.Rotate(rotationDirection);
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
                        Debug.Log("turning");
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
                //Debug.Log("dropping");
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
