using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{

	public float speed = 3f;
	public Rigidbody2D playerRb;
	Animator animator;
	public float input;
	public SpriteRenderer spriteRenderer;
	public float jumpForce = 5f;

	public LayerMask groundLayer;
	private bool isGrounded;
	public Transform feetPosition;
	public float groundCheckCircle;

	public AudioSource jumpSFX;

	void Start()
	    {
	        animator = gameObject.GetComponent<Animator>();
	    }


    void Update()
    {
		input = Input.GetAxisRaw("Horizontal");
		if (input < 0)
		{
			spriteRenderer.flipX = true;
		}
		else if(input > 0)
		{
			spriteRenderer.flipX = false;
		}
		

		
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			
			animator.SetBool("isWalking", true);
		}
		
		if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
			animator.SetBool("isWalking", false);
		}
		
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			
			animator.SetBool("isWalking", true);
		}
		
		if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
			animator.SetBool("isWalking", false);
		}
        

        if (Input.GetKeyUp(KeyCode.E)) {
			animator.SetTrigger("kicked");
		}
		
		isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);


		if (isGrounded == true && Input.GetButtonDown("Jump"))
		{
			jumpSFX.Play();
			playerRb.velocity = Vector2.up * jumpForce;
		}

		/*
		if (animator.getBool("isWalking") == true && isGrounded == false)
		{
			animator.SetBool("isWalking", false);
		}
		*/

	}

	void FixedUpdate()
	{
		playerRb.velocity = new Vector2 (input * speed, playerRb.velocity.y);
	}
}