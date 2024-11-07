using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limbCollision : MonoBehaviour
{
    public PlayerController controller;
    public bool isFeet;
    public bool detectable;
    [SerializeField] Rigidbody hip;
    [SerializeField] AudioSource bounce;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (detectable && !collision.collider.CompareTag("Environment"))
        {
            controller.isGrounded = true;
        }

        //Invoke("resetJump", 1f);
        if (isFeet)
        {
            if (collision.collider.CompareTag("trampline"))
            {
                hip.AddForce(Vector3.up * 2650f, ForceMode.Impulse);
               // bounce.Play();
            }
        }

        if (collision.collider.CompareTag("Respawn"))
        {
            //execute reload event;
            EventManager.reload();
        }
    }
    void resetJump()
    {
        controller.isGrounded = true;
    }
}
