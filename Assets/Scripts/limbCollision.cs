using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limbCollision : MonoBehaviour
{
    public PlayerController controller;

    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        
        controller.isGrounded = true;
    }
}
