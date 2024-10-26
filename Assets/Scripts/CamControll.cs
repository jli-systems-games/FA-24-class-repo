using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControll : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public Transform target;
    [SerializeField] Customization custom;
    float mouseX, mouseY;

    public ConfigurableJoint hipJoint;
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        if(custom.done)CamBehavior();
    }

    void CamBehavior()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);
        //mouseX = Mathf.Clamp(mouseX, -35, 60);

        Quaternion rootRotation = Quaternion.Euler(mouseY, mouseX , 0);
        target.rotation = rootRotation;

        hipJoint.targetRotation = Quaternion.Euler(0, -mouseX *1.5f, 0);
    }

}
