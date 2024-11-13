using UnityEngine;
using System.Collections.Generic;

public class JointGenerator : MonoBehaviour
{
    public List<GameObject> components; 
    public float connectionDistance = 1.0f;

    public void GenerateJoints()
    {
        foreach (var component in components)
        {
            foreach (var otherComponent in components)
            {
                if (component == otherComponent) continue;

                
                float distance = Vector2.Distance(component.transform.position, otherComponent.transform.position);

                
                if (distance < connectionDistance)
                {
                    CreateJointBetween(component, otherComponent);
                }
            }
        }
    }

    private void CreateJointBetween(GameObject component, GameObject otherComponent)
    {
        if (component.tag == "Wheel" && otherComponent.tag == "Body")
        {
            WheelJoint2D wheelJoint = component.AddComponent<WheelJoint2D>();
            wheelJoint.connectedBody = otherComponent.GetComponent<Rigidbody2D>();
            wheelJoint.anchor = component.transform.InverseTransformPoint(otherComponent.transform.position);

            
            JointSuspension2D suspension = wheelJoint.suspension;
            suspension.dampingRatio = 0.7f;
            suspension.frequency = 4f;
            wheelJoint.suspension = suspension;
        }
        else if (component.tag == "Body" && otherComponent.tag == "Body")
        {
            FixedJoint2D fixedJoint = component.AddComponent<FixedJoint2D>();
            fixedJoint.connectedBody = otherComponent.GetComponent<Rigidbody2D>();
            fixedJoint.anchor = component.transform.InverseTransformPoint(otherComponent.transform.position);
        }
    }
}
