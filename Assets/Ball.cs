using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public float horizontalSpeed = 30F;
    public float verticalSpeed = 30F;

    public float speed = 10f;
    public Rigidbody rb;

    public Vector3 startPosition;
    public Vector3 bouncePosition;

    public float xdirection = 2.5f;
    public float zdirection = -1f;

    Vector3 lastVelocity;
    float currSpeed;
    float maxspeed = 40f;
    float minspeed = 20f;
    Vector3 direction;


    public GameObject DVD;

    public byte RR;
    public float R;
    public byte GG;
    public float G;
    public byte BB;
    public float B;

    public float C;
    public float S;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        rb.AddForce(xdirection, zdirection, 0f, ForceMode.VelocityChange);

        Launch();
    }

    void Update()
    {
        lastVelocity = rb.velocity;

        //control/clmap speed
        if (currSpeed > maxspeed)
        {
            lastVelocity = lastVelocity.normalized * maxspeed;
        }
        else if (currSpeed < minspeed)
        {
            lastVelocity = lastVelocity.normalized * minspeed;
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(v, h, 0);
    }


    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float z = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector3(speed * x, 0, speed * z);
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        transform.position = startPosition;
        Launch();
    }

    private void OnCollisionEnter(Collision collision)
    {
        currSpeed = lastVelocity.magnitude;

        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * (currSpeed + 2f);

        ColorChange();
    }

    public void ColorChange()
    {
        Paint();
        DVD.GetComponent<Renderer>().material.color = new Color32(RR, GG, BB, 255);
    }

    private void Paint()
    {
        C = Random.Range(1, 4);

        if (C == 1)
        {
            //no red
            R = 0;

            S = Random.Range(1, 3);
            //green
            //blue

            if (S == 1)
            {
                G = Random.Range(0, 255);
                B = 255;
            }
            else if (S == 2)
            {
                G = 255;
                B = Random.Range(0, 255);
            }

        }
        else if (C == 2)
        {
            //no green
            G = 0;

            S = Random.Range(1, 3);
            //red
            //blue

            if (S == 1)
            {
                R = Random.Range(0, 255);
                B = 255;
            }
            else if (S == 2)
            {
                R = 255;
                B = Random.Range(0, 255);
            }

        }
        else if (C == 3)
        {
            //no blue
            B = 0;

            S = Random.Range(1, 3);
            //red
            //green

            if (S == 1)
            {
                R = Random.Range(0, 255);
                G = 255;
            }
            else if (S == 2)
            {
                R = 255;
                G = Random.Range(0, 255);
            }

        }

        RR = (byte)R;
        GG = (byte)G;
        BB = (byte)B;

    }
}
