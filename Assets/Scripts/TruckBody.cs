using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckBody : MonoBehaviour
{
    private Rigidbody truckRigidbody;
    public float DragForce;
    public float CorrectionUpForce;
    // Start is called before the first frame update
    void Start()
    {
        truckRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        AddDragForce();
        AddCorrectiveForce();
        
    }
    private void AddDragForce()
    {
        truckRigidbody.AddForce(-truckRigidbody.velocity * DragForce);
    }

    private void AddCorrectiveForce()
    {
        Vector3 _rotAxisUp;
        float _rotDegreesUp;

        Quaternion _upDiff = Quaternion.FromToRotation(truckRigidbody.transform.forward, Vector3.up);
        _upDiff.ToAngleAxis(out _rotDegreesUp, out _rotAxisUp);
        float _rotRadiansUp = _rotDegreesUp * Mathf.Deg2Rad;
        truckRigidbody.AddTorque(_rotAxisUp * _rotRadiansUp * CorrectionUpForce);
    }
}
