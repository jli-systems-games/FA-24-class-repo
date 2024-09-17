using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform left;
    public Transform forward;
    public Transform right;

    Vector3 lookingDirect;//need to be updated
    public bool isLookingForward = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

      /*  if(transform.rotation.y ==  -90 || transform.rotation.y == 90)
        {
            isLookingForward = false;
        }
        else
        {
            isLookingForward = true;
        }*/

        Looking();
        
        
    }

    void Looking()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && isLookingForward)
           {    lookingDirect = left.position - transform.position;
                        Quaternion rotation = Quaternion.LookRotation(lookingDirect, Vector3.up);
                        transform.rotation = rotation;
                        isLookingForward = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && isLookingForward)
        {
            lookingDirect = right.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookingDirect, Vector3.up);
            transform.rotation = rotation;
            isLookingForward = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            lookingDirect = forward.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(lookingDirect, Vector3.up);
            transform.rotation = rotation;
            isLookingForward = true;
        }
       
    }
}
