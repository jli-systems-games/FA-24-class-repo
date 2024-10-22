using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyMotion : MonoBehaviour
{
    public Transform target;
    public bool mirror;
    ConfigurableJoint joint;
    void Start()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mirror)
        {
            joint.targetRotation = target.rotation;
        }
        else
        {
            joint.targetRotation = Quaternion.Inverse(target.rotation);
        }
        
    }
}
