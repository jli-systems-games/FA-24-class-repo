using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] Transform Joint;
    private Transform holdPointTransform;
    private Rigidbody rb;
    Quaternion newRotation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(holdPointTransform != null)
        {
            /*float lerpSpeed = 10f;  
            Vector3 newPosition = Vector3.Lerp(transform.position, holdPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);*/
            //transform.LookAt(Camera.position);
            //transform.position = holdPointTransform.position;
            //calculatingFacing();
            //rb.useGravity = false;
        }
    }

    public void Grab(Transform holdPoint)
    {
        this.holdPointTransform = holdPoint;
        
        //transform.position = holdPointTransform.position;
        
        transform.SetParent(Joint);
       
        transform.localPosition = holdPointTransform.localPosition;
        transform.up = Vector3.up;
        transform.forward = Joint.parent.forward;
    }

    public void Drop()
    {
        transform.SetParent(null);
    }

    void calculatingFacing()
    {
        Vector3 directiontoCamera = transform.position - Joint.position;
        Vector3 opposite = transform.position + directiontoCamera;

        transform.LookAt(opposite);

    }
}
