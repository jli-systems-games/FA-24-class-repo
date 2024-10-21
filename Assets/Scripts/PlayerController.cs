using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Basic settings")]
    [SerializeField] float speed;
    [SerializeField] float strafeSpeed; //speed when to move left and right
    [SerializeField] float jumpForce;

    [Header("Others")]
    [SerializeField] Rigidbody hip;

    public bool isGrounded;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                hip.AddForce(hip.transform.forward * speed * 1.5f);
            }
            else
            {
                hip.AddForce(hip.transform.forward * speed);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            hip.AddForce(-hip.transform.forward * speed);

        }

        if (Input.GetKey(KeyCode.D))
        {
            hip.AddForce(hip.transform.right * strafeSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            hip.AddForce(-hip.transform.right * strafeSpeed);
        }

        if (Input.GetAxis("Jump") >0)
        {
            if (isGrounded)
            {
                 hip.AddForce(new Vector3(0, jumpForce, 0));
                 isGrounded = false;
            }
           

        }
    }
}
