using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pong;
    public Rigidbody gnRb;
    public GameObject P1;
    public GameObject P2;

    public float speed = 3.5f;
    public bool isCaughtByPlayer, hasCaughtBall;
    float steps;
    Vector3 Move = Vector3.zero;
    
    Ray _ray;
    RaycastHit hit;
    
    // Start is called before the first frame update
    void Start()
    {
         
        isCaughtByPlayer = false;
        Move = new Vector3(Random.Range(-4f, 4f), Random.Range(-2, 1f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        steps = speed * Time.deltaTime;
        _ray = new Ray(transform.position, Move);
        //pongDist = Vector3.Distance(transform.position, pong.position);
       //Debug.Log(pongDist);
        if(hasCaughtBall == true)
        {
            //Debug.Log(Move);
            Avoid();
          
            
        }
        else
        {
                transform.position = Vector3.MoveTowards(transform.position, pong.position, steps);
        }

    }

    void Avoid()
    {
      

        if(Physics.Raycast(_ray,out hit , 4f))
        {
            Debug.Log(hit.transform.gameObject.name);
            
            Move = -Move;
        }
       
            gnRb.MovePosition(gnRb.position + Move * steps);
        

    }
   
    void gnMovement()
    {

    }

}
