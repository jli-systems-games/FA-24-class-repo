using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limbCollision : MonoBehaviour
{
    public PlayerController controller;
    public bool isFeet;

    [SerializeField] Rigidbody hip;
    [SerializeField] AudioSource bounce;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        
        controller.isGrounded = true;
        if(isFeet)
        {
            if (collision.collider.CompareTag("trampline"))
            {
                hip.AddForce(Vector3.up * 1050f, ForceMode.Impulse);
                bounce.Play();
            }
        }
    }
}
