using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    public Rigidbody rb;
    //int numbOfBounce = 1;
    public float xdirection = 2.5f;
    public float ydirection = -1f;
    Vector3 lastVelocity;
    float currSpeed;
    float maxspeed = 6.5f;
    float minspeed = 5.5f;
    Vector3 direction;
    Enemy gn;


    public Players p1;
    public player2 p2;
    public EventManager events;
    public GameObject gnome;
    public Camera cam;
    public GameObject flr, ceil, lft, rght;
    //int currBounce = 0;
    public HingeJoint hj;
    bool relocated;
    public bool start = false;
    private void Awake()
    {
        transform.position = new Vector3(0, Random.Range(-1f, 5f), 0);
        //Debug.Log(transform.position);
    }
    void Start()
    {
        if (events.numb != 0)
        {   
            rb.AddForce(xdirection,ydirection,0f, ForceMode.VelocityChange);

        }
        
        gn = gnome.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            rb.AddForce(xdirection, ydirection, 0f, ForceMode.VelocityChange);
            start = false;
        }
        lastVelocity = rb.velocity;
       
        Debug.Log(currSpeed);
        //control/clmap speed
      

       /*if((transform.position.x < lft.transform.position.x) || (transform.position.x > rght.transform.position.x))
        {
            relocated = true;
            relocate();

        }else if ((transform.position.y >= ceil.transform.position.y) || (transform.position.y <= flr.transform.position.y))
        {
            relocated = true;
            relocate();
        }*/

       
        //this if statement below is for determing when the game ends
       if(p1.gameEnd || p2.gameEnd)
        {
            rb.velocity = Vector3.zero;
        }

        

    }

    private void OnCollisionEnter(Collision collision)
    {
       
        //else if(!gn.hasCaughtBall)
        //{
           
          if(gnome.activeSelf == false)
        {
            currSpeed = lastVelocity.magnitude;

             direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * currSpeed;
        }
        else
        {
            //bool isAttached = false;
            
            
            if (collision.collider.CompareTag("Enemy"))
            {
                    gn.hasCaughtBall = true;
                    
                    hj = gameObject.AddComponent<HingeJoint>();
                    hj.connectedBody = collision.rigidbody;
                    rb.velocity = Vector3.zero;
            }else if (collision.collider.CompareTag("Player"))
            {
                if ( gn.isCaughtByPlayer == true && !gn.hasCaughtBall)
                    {
                        Reset();
                        }

            }

            if (!gn.hasCaughtBall)
            {

                currSpeed = lastVelocity.magnitude;

                direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

                rb.velocity = direction * currSpeed;
            }

        }
            

            if(currSpeed > maxspeed)
        {
            
            lastVelocity = lastVelocity.normalized * maxspeed;
            }
            else if(currSpeed < minspeed)
        {
            lastVelocity = lastVelocity.normalized * minspeed;
            }
        //}
/*       this is was for something else will need to fix it later.
        if (collision.collider.CompareTag("Wall"))
        {
            float newxdirection = Random.Range(-2.5f, 2.5f);
             rb.velocity = Vector3.zero;
            transform.position = new Vector3(0, Random.Range(-2f, 2f), 0);
            rb.AddForce(newxdirection, ydirection, 0f, ForceMode.VelocityChange);
           


        }*/
        
    }

    public void Reset()
    {   Destroy(hj);
        float x;
           if(cam.WorldToScreenPoint(transform.position).x > Screen.width / 2)
        {
             x = transform.position.x + 4f;
        }
        else
        {
             x = transform.position.x + -4f;
        }
            
            transform.position = new Vector3(x, Random.Range(0, 2f), 0);
            rb.AddForce(xdirection, ydirection, 0f, ForceMode.Acceleration);
        
    }
    void relocate() {
        if (!relocated)
        {
            rb.velocity = Vector3.zero;
        transform.position = new Vector3(0, Random.Range(-1f, 5f), 0);
        rb.AddForce(transform.position.x, ydirection, 0f, ForceMode.VelocityChange);
        }
        else
        {
            relocated = false;
            return;
        }
        
    }
}
