using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBController : MonoBehaviour
{

    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector3(0, 0, 5);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector3(0, 0, -5);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
}
