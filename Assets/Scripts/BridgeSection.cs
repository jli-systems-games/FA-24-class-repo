using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSection : MonoBehaviour
{
    public float Mass;
    public float AngularDrag;
    public float Drag;
    public float SpringStrength;
    public float SpringDampening;

    private SpringJoint[] Springs;
    private Rigidbody bridgeSectionBody;

    public float CorrectiveUpForce;
    // Start is called before the first frame update
    void Start()
    {
        Springs = GetComponents<SpringJoint>();
        for (int i = 0; i < Springs.Length; i++)
        {
            Springs[i].spring = SpringStrength;
            Springs[i].damper = SpringDampening;
        }
        bridgeSectionBody = GetComponent<Rigidbody>();
        bridgeSectionBody.mass = Mass;
        bridgeSectionBody.drag = Drag;
        bridgeSectionBody.angularDrag = AngularDrag;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        AddCorrectiveForce();
    }
    private void AddCorrectiveForce()
    {
        Vector3 _rotAxisUp;
        float _rotDegreesUp;

        Quaternion _upDiff = Quaternion.FromToRotation(bridgeSectionBody.transform.forward, Vector3.up);
        _upDiff.ToAngleAxis(out _rotDegreesUp, out _rotAxisUp);
        float _rotRadiansUp = _rotDegreesUp * Mathf.Deg2Rad;
        bridgeSectionBody.AddTorque(_rotAxisUp * _rotRadiansUp * CorrectiveUpForce);
    }
}
