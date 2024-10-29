using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmGrabbing : MonoBehaviour
{
    Rigidbody hand;
    [SerializeField] Rigidbody shoulder;
    public FixedJoint grabbedObj;
    [SerializeField] LayerMask _layer;
    [SerializeField] BoxCollider _hitBox;
    [SerializeField] public GameObject signal;
    CopyMotion shoulderMotion;

    ConfigurableJoint shoulderJnt;
    public float moveForce, radius,maxDistance;
    public int mouseBttn;
    bool ismovingArm;
    public bool grabbedON;
   
    Ray _ray;
    RaycastHit _hit;
    void Start()
    {
        hand = GetComponent<Rigidbody>();
        shoulderMotion = shoulder.GetComponent<CopyMotion>();
        shoulderJnt = shoulder.GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(mouseBttn))
        {
            if (!ismovingArm)
            {
                startGrabbing();

            }
           
        }
        if(Input.GetMouseButton(mouseBttn))
        {
            _ray = new Ray(transform.position,transform.forward);
            Vector3 direction = new Vector3 (-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) ;
            hand.AddForce(direction * moveForce);

            //need to stop the copmotion script;
            shoulderJnt.targetRotation = Quaternion.Euler(0, 0, -Input.GetAxis("Mouse Y") * 180f);
            Debug.DrawRay(transform.position, -transform.forward, Color.yellow);


        }


        if (Input.GetMouseButtonUp(mouseBttn))
        {
           
            if (ismovingArm)
            { 
                Debug.Log("letting go");
                stoppedGrabbing();
            }

        }
    }

   
    void startGrabbing()
    {
        shoulderMotion.enabled = false;
   
        _hitBox.enabled = true;
        ismovingArm = true;

    }
    void stoppedGrabbing()
    {
        if (grabbedON)
        {
            Debug.Log("destroying");
            if(grabbedObj != null)
            {
                Destroy(grabbedObj);
                grabbedON = false;
                shoulderMotion.enabled = true;
            }
           
        }
        else
        {
             
        }
       
        ismovingArm = false; 
        _hitBox.enabled = false;
    }

    
}
