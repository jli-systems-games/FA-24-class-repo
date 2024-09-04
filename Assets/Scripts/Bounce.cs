using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Rigidbody rb;
    //int numbOfBounce = 1;
    public float xdirection = 2.5f;
    public float ydirection = -1f;
    Vector3 lastVelocity;
    float currSpeed;
    float maxspeed = 5.5f;
    float minspeed = 4.5f;
    Vector3 direction;
    //int currBounce = 0;

    private void Awake()
    {
        transform.position = new Vector3(0, Random.Range(-1f, 8f), 0);
        //Debug.Log(transform.position);
    }
    void Start()
    {
        
        rb.AddForce(xdirection,ydirection,0f, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        lastVelocity = rb.velocity;
       
        //Debug.Log(currSpeed);
        if(currSpeed > maxspeed)
        {
            
            lastVelocity = lastVelocity.normalized * maxspeed;
        }
        else if(currSpeed < minspeed)
        {
            lastVelocity = lastVelocity.normalized * minspeed;
        }
        Debug.Log(direction);
        

    }

    private void OnCollisionEnter(Collision collision)
    {   
        
        //FixedSpeed(lastVelocity, minspeed, maxspeed);
        currSpeed = lastVelocity.magnitude;

        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * currSpeed;
        

        if (collision.collider.CompareTag("Wall"))
        {
            
            transform.position = new Vector3(0, Random.Range(-2f, 2f), 0);
            rb.AddForce(xdirection, ydirection, 0f, ForceMode.Force);


        }
        
    }
    
}
