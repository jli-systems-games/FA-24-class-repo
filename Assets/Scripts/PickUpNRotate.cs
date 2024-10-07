using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PickUpNRotate : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    Grabbable _grabbable;
    [SerializeField] Transform holdPoint;
    [SerializeField] Transform plyCamera;
    [SerializeField] LayerMask pickUplayermask;
    Vector2 inputVect;
    bool pickedUP;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(plyCamera.position, plyCamera.forward);
        Debug.DrawRay(plyCamera.position, plyCamera.forward * 15, Color.green);
        if (pickedUP && hit.transform != null)
        {
            Vector3 rotationDirection = new Vector3(inputVect.y, inputVect.x, 0);
            Debug.Log(hit.collider.gameObject);
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
                        //Debug.Log(_grabbable);
                    }
                }
            }
            else
            {
                _grabbable.Drop();
                pickedUP = false;
            }
            
        }
    }

    public void RotateInput(InputAction.CallbackContext context)
    {
        Debug.Log("trying to rotate");
        inputVect = context.ReadValue<Vector2>();
    }
}
