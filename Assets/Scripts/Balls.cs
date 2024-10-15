using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balls : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject boredomBar;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //invoke fetching events;
       
        if (collision.transform.CompareTag("ground"))
        {
             Debug.Log("start fethcing");
            eventManager.startFetch(this.transform);
            rb.velocity = Vector3.zero;

            Invoke("ResetSelf", 4f);
        }else if (collision.transform.CompareTag("cryptid"))
        { 
            eventManager.decreaseB(boredomBar, "decrease");
            eventManager.resetEnemy();
           
        }
        //if the the entering object is Cryptids
        //Despawn itself;
    }
    void ResetSelf()
    {
        Destroy(this.gameObject);
    }
}
