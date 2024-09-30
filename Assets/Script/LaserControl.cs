using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    private LineRenderer lr;
    [SerializeField] Transform from;
    [SerializeField] PickupNSetdown pick;

    Ray _ray;
    void Start()
    {
        lr = GetComponent<LineRenderer>();  
         _ray= new Ray(transform.position, transform.forward);
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKey(KeyCode.E))
        {
            if (pick.isHoldingBby)
            {
                lr.enabled = true;
                lr.SetPosition(0,from.position);
                RaycastHit hit;
      
                if(Physics.Raycast(_ray, out hit, 50f))
                {
                     if(hit.collider) 
                     {
                        lr.SetPosition(1, hit.point);
                     }
                }
                else
                {
            
                    lr.SetPosition(1, transform.forward * 100f);
            
                }
            }
            
        }
        else
        {
            lr.enabled = false;
        }
        
    }
}
