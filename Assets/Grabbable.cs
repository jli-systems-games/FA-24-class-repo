using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] Transform Camera;
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
            calculatingFacing();
            //rb.useGravity = false;
        }
    }

    public void Grab(Transform holdPoint)
    {
        this.holdPointTransform = holdPoint;
        
        newRotation.eulerAngles = Vector3.zero;
        transform.rotation = newRotation;
        transform.SetParent(Camera);
    }

    void calculatingFacing()
    {
        Vector3 directiontoCamera = transform.position - Camera.position;
        Vector3 opposite = transform.position + directiontoCamera;

        transform.LookAt(opposite);

    }
}
