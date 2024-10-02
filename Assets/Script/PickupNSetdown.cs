using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupNSetdown : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] Transform holdPoint;

    Grabbable _grabbable;
    public bool isHoldingBby;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Ray ray = new Ray(Cam.position, Cam.forward);
        if (Input.GetMouseButtonDown(0))
        {
            if (!isHoldingBby)
            {
                if(Physics.Raycast(ray, out hit, 4f, pickUpLayer))
                {   
                    if(hit.transform.TryGetComponent(out _grabbable))
                    {   
                
                    _grabbable.Grab(holdPoint);
                    isHoldingBby = true;

                    //Debug.Log(_grabbable);
                    }
                }
            }
            else
            {   
                Debug.Log("drop");
                _grabbable.Drop();
               isHoldingBby = false;
            }
            
        }

        //Debug.Log(_grabbable);

        
    }
}
