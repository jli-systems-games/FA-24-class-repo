using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum objectState
{
    pickedUp,
    setDown,
}
public class Grabbable : MonoBehaviour
{
    [SerializeField] Transform newParent;
    private Transform holdPointTransform;
    private Rigidbody rb;
    Vector3 ogPosition;
    Quaternion ogRotation;
    public objectState currentState = objectState.setDown;
    void Start()
    {
         //rb = GetComponent<Rigidbody>();
         ogPosition = transform.position;
         ogRotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

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
        transform.SetParent(newParent);
        transform.position = ogPosition;
        transform.rotation = ogRotation;
        ChangeState(objectState.setDown);
    }

    public void ChangeState(objectState state)
    {
        currentState = state;
        //notifyEvents

    }
}
