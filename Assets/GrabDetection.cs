using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabDetection : MonoBehaviour
{
    ArmGrabbing _grab;
   // [SerializeField] GameObject signal;
    void Start()
    {
        _grab = GetComponentInParent<ArmGrabbing>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("grabble"))
        {
            Debug.Log("grabbingON");
             _grab.grabbedON = true;
            EventManager.climbing(_grab.mouseBttn);
            Rigidbody obj = other.gameObject.GetComponent<Rigidbody>();
            if(obj.TryGetComponent<FixedJoint>(out FixedJoint fj))
            {
                //destroy that joint;
                Destroy(fj);
            }
            else
            {
                FixedJoint fjint = transform.parent.gameObject.AddComponent<FixedJoint>();
                _grab.grabbedObj = fjint;
                fjint.connectedBody = obj;
            }
            
           // signal.SetActive(true);
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("exiting");
        /*_grab.grabbedON = false;
       signal.SetActive(false);*/
    }
}
