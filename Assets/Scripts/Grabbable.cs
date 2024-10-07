using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Transform holdPointTransform;
    private Rigidbody rb;
    void Start()
    {
         rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (holdPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, holdPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.MovePosition(newPosition);
            rb.useGravity = false;
        }
    }
    public void Grab(Transform holdPoint)
    {
        this.holdPointTransform = holdPoint;
    }
}
