using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 不倒翁 : MonoBehaviour
{
    Rigidbody pRb;
    public float bounceForce;
    [SerializeField] Transform COM;
    void Start()
    {  
        pRb = GetComponent<Rigidbody>();
        pRb.centerOfMass = COM.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Player"))
        {
            pRb.AddForce(collision.contacts[0].normal *  bounceForce, ForceMode.Impulse);
        }
        

    }
}
