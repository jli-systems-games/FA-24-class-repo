using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    [Header("Paddle Settings")]
    public KeyCode activateKey;
    private float paddleForce = 2500f, restAngle = 0f, returnSpeed = 10f;
    public float hitAngle;

    private HingeJoint hinge;
    private JointSpring hingeSpring;

    void Awake()
    {
        hinge = GetComponent<HingeJoint>();
        Rigidbody rb = GetComponent<Rigidbody>();
        
        //rb.isKinematic = false;
        //rb.useGravity = false;
        //rb.mass = 1f;

        //hinge.useSpring = true;
        //hinge.useLimits = true;

        hingeSpring = new JointSpring
        {
            spring = paddleForce,
            damper = 10f,
            targetPosition = restAngle
        };
        hinge.spring = hingeSpring;

        JointLimits limits = hinge.limits;
        limits.min = restAngle; 
        limits.max = hitAngle;
        hinge.limits = limits;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(activateKey))
        {
            ActivatePaddle();
        }
        else
        {
            ResetPaddle();
        }
    }

    private void ActivatePaddle()
    {
        hingeSpring.targetPosition = hitAngle;
        hinge.spring = hingeSpring;
    }

    private void ResetPaddle()
    {
        hingeSpring.targetPosition = Mathf.Lerp(hingeSpring.targetPosition, restAngle, Time.deltaTime * returnSpeed);
        hinge.spring = hingeSpring;
    }
}
