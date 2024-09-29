using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupNSetdown : MonoBehaviour
{
    [SerializeField] Transform Cam;
    [SerializeField] private LayerMask pickUpLayer;
    [SerializeField] Transform holdPoint;
    RaycastHit hit;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   Ray ray = new Ray(Cam.position, Cam.forward);
        if(Physics.Raycast(ray, out hit, 4f, pickUpLayer))
        {
            if(hit.transform.TryGetComponent(out Grabbable _grabbable))
            {   
                if(Input.GetMouseButtonDown(0))
                {
                    _grabbable.Grab(holdPoint);
                }
                

                Debug.Log(_grabbable);
            }
            
        }
    }
}
