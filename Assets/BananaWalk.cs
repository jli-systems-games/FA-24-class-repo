using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaWalk : MonoBehaviour
{
    public Rigidbody rb;
    public Animator animator;

    public GameObject player;
    public float speed = 1f;
    public float rotation_speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        player = (GameObject)this.gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void BananaMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.position -= player.transform.forward * speed * Time.deltaTime;
            animator.Play("walking");
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position += player.transform.forward * speed * Time.deltaTime;
            animator.Play("walking");
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.Rotate(Vector3.down * rotation_speed);
            animator.Play("walking");
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.Rotate(Vector3.up * rotation_speed);
            animator.Play("walking");
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
