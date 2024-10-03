using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour
{
    [SerializeField] Rigidbody TruckBody;

    public LayerMask GroundLayer;
    public float WheelUpForce;
    public float SuspensionRestDistance;
    public float MaxRaycastDistance;
    public float ShockAbsorberForceStrength;
    public float ShockAbsorberDampeningStrength;

    public float SlipAmount;
    public float TopSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        RaycastHit _hit;
        if(Physics.Raycast(transform.position, -transform.up, out _hit, MaxRaycastDistance, GroundLayer))
        {
            CalculateVerticalForce(_hit.distance);
            CalculateSteeringForce();
            if (Input.GetKey(KeyCode.Space))
            {
                CalculateAccelerationForce();
            }
        }
    }

    private void CalculateVerticalForce(float rayDist)
    {
        //using a spring for vertical force
        //spring is local up direction of wheel (maybe change to global up?)
        Vector3 _springDirection = transform.up;
        Vector3 _currentWorldVelocity = TruckBody.GetPointVelocity(transform.position);

        //how far are we from our resting position?
        float _offset = SuspensionRestDistance - rayDist;

        //how closely are we in-line with our current moving velocity?
        float _currentVerticalVelocity = Vector3.Dot(_springDirection, _currentWorldVelocity);

        float _verticalForce = (_offset * ShockAbsorberForceStrength) - (_currentVerticalVelocity * ShockAbsorberDampeningStrength);
        TruckBody.AddForceAtPosition(_verticalForce * _springDirection, transform.position);
    }

    private void CalculateSteeringForce()
    {
        Vector3 _steeringDirection = transform.forward;
        Vector3 _currentWorldVelocity = TruckBody.GetPointVelocity(transform.position);

        float _currentSlipVelocity = Vector3.Dot(_steeringDirection, _currentWorldVelocity);

        float _neededAcceleration = -_currentSlipVelocity * SlipAmount / Time.fixedDeltaTime;

        TruckBody.AddForceAtPosition(_neededAcceleration * _steeringDirection, transform.position);
    }

    private void CalculateAccelerationForce()
    {
        Vector3 _accelerationDirection = transform.right;
        Vector3 _currentWorldVelocity = TruckBody.GetPointVelocity(transform.position);

        float _carSpeed = Vector3.Dot(_accelerationDirection, _currentWorldVelocity);
        float _normalizedSpeed = Mathf.Clamp01(Mathf.Abs(_carSpeed) / TopSpeed);

        TruckBody.AddForceAtPosition(_accelerationDirection, transform.position);
    }
}
