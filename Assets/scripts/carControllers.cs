using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carControllers : MonoBehaviour
{

    public WheelJoint2D frontwheel;
    public WheelJoint2D backwheel;

    JointMotor2D motorFront;
    JointMotor2D motorBack;

    public float speedF;
    public float speedB;

    public float torqueF;
    public float torqueB;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        motorBack.motorSpeed = speedB;
        motorBack.maxMotorTorque = torqueB;
        backwheel.motor = motorBack;

        motorFront.motorSpeed = speedB;
        motorFront.maxMotorTorque = torqueB;
        frontwheel.motor = motorFront;


        /*if(Input.GetAxisRaw("Vertical") > 0)
        {
            motorBack.motorSpeed = speedF;
            motorBack.maxMotorTorque = torqueF;
            backwheel.motor = motorBack;

            motorFront.motorSpeed = speedF;
            motorFront.maxMotorTorque = torqueF;
            frontwheel.motor = motorFront;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            motorBack.motorSpeed = speedB;
            motorBack.maxMotorTorque = torqueB;
            backwheel.motor = motorBack;

            motorFront.motorSpeed = speedB;
            motorFront.maxMotorTorque = torqueB;
            frontwheel.motor = motorFront;
        }*/

    }
}
