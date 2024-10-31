using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class ArmGrabbing : MonoBehaviour
{
    Rigidbody hand;
    [SerializeField] Rigidbody shoulder;
    public FixedJoint grabbedObj;
    [SerializeField] LayerMask _layer;
    [SerializeField] BoxCollider _hitBox;
    //[SerializeField] public GameObject signal;
    CopyMotion shoulderMotion;
    Outline _highLight;
    ConfigurableJoint shoulderJnt;
    public float moveForce, radius,maxDistance;
    public int mouseBttn;
    bool ismovingArm;
    public bool grabbedON;

    Transform _hitObj;
    Ray _ray;
    RaycastHit _hit;
    void Start()
    {
        hand = GetComponent<Rigidbody>();
        shoulderMotion = shoulder.GetComponent<CopyMotion>();
        shoulderJnt = shoulder.GetComponent<ConfigurableJoint>();
        _highLight = GetComponent<Outline>();
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
           
            Vector3 direction = new Vector3 (-Input.GetAxis("Mouse X"), 0, -Input.GetAxis("Mouse Y")) ;
            hand.AddForce(direction * moveForce);

            Vector3 rayDirection = new Vector3(transform.right.x, 0, -transform.up.y);
            rayDirection = transform.TransformDirection(rayDirection.normalized);
             _ray = new Ray(transform.position, rayDirection);
            //need to stop the copmotion script;
            shoulderJnt.targetRotation = Quaternion.Euler(0, 0, -Input.GetAxis("Mouse Y") * 180f);
            //Debug.DrawRay(transform.position, rayDirection *50f, Color.yellow);
            if (Physics.Raycast(_ray,out _hit, 50f, _layer))
            {
                Highlighting(_hit.transform);
                _hitObj = _hit.transform;
            }
            else
            {
                StopHighight();
            }


        }


        if (Input.GetMouseButtonUp(mouseBttn))
        {
           
            if (ismovingArm)
            { 
                //Debug.Log("letting go");
                stoppedGrabbing();
            }

        }
       
    }

   
    void startGrabbing()
    {
        shoulderMotion.enabled = false;
        _highLight.enabled = true;
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
            EventManager.stopClimbing(mouseBttn);
        }
        
        _highLight.enabled = false;
        ismovingArm = false; 
        _hitBox.enabled = false;
        StopHighight();
    }
    void Highlighting(Transform target)
    {
        if (target.gameObject.TryGetComponent<Outline>(out Outline _outline))
        {
           //Turn it on
           if(_outline.enabled == false)
            {
                 _outline.enabled = true;
            }
          
        }
        else
        {
            Outline _outLine = target.gameObject.AddComponent<Outline>();
            _outLine.enabled = true;
            _outLine.OutlineColor = Color.yellow;
            _outLine.OutlineWidth = 7.0f;
        }
    }
    void StopHighight()
    {
        if(_hitObj != null)
        {
            //Debug.Log(_hitObj.name);
            if(_hitObj.gameObject.TryGetComponent<Outline>(out Outline _outline))
            {
                _outline.enabled = false;
            }
        }
    }
    
}
