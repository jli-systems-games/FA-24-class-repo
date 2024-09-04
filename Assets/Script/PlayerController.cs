using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator anim;
    public Rigidbody2D rb;
    public float jumpForce;
    public float playerSpeed;
    public Vector2 jumpHeight;
    public bool isOnGround;
    public float positionRadius;
    public LayerMask ground;
    public Transform playerPos;

    // Start is called before the first frame update
    void Start()
    {
     
        Collider2D[] colliders = transform.GetComponentsInChildren<Collider2D>();
        for (int i = 0; i < colliders.Length; i++)
        {
            for (int k = i + 1; k < colliders.Length; k++)
            {
                Physics2D.IgnoreCollision(colliders[i], colliders[k]);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D)) 
        {
            anim.Play("WALK");
            rb.AddForce(Vector2.right * playerSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A)) 
        {
            anim.Play("WALK BACK");
            rb.AddForce(Vector2.left * playerSpeed * Time.deltaTime);
        }
        else
        {
            anim.Play("IDLE");
        }

        isOnGround = Physics2D.OverlapCircle(playerPos.position, positionRadius, ground);
        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce* Time.deltaTime);
        }
    }

}
