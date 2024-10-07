using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Transform holdPointTransform,viewingCam;
    private Rigidbody rb;
    Vector3 ogPosition;
    Quaternion ogRotation;
    void Start()
    {
         //rb = GetComponent<Rigidbody>();
         ogPosition = transform.position;
         ogRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

        /*if (holdPointTransform != null && !rb.useGravity)
        {
            float lerpSpeed = 10f;

            Vector3 newPosition = Vector3.Lerp(transform.position, holdPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
            
        }*/
    }
    public void Grab(Transform holdPoint, Transform camView)
    {
        this.holdPointTransform = holdPoint;
        transform.SetParent(holdPoint);
        transform.localPosition = holdPoint.localPosition;
        transform.position = holdPoint.position;
        //rb.useGravity = false;
    }

    public void Drop()
    {
        transform.SetParent(null);
        transform.position = ogPosition;
        transform.rotation = ogRotation;
        //rb.useGravity = true;
    }
}
