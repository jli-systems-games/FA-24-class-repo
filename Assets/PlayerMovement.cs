using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //movement
    private float horizontal;
    private float vertical;

    private float speed = 8f;
    private float jumpingPower = 10f;
    private bool isFacingRight = true;
    private float groundCheckRadius = 0.2f;

    //dashing
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 50f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.3f;

    //wallsliding
    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    //walljumping
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private SpriteRenderer sr2;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform ceilingCheck;

    public bool orange; //dash
    public bool blue; //gravity switch
    public bool green; //wallslide + jump
    public bool pink; //super speed

    //i hate everything bools
    private bool oneOrange;
    private bool oneGreen;
    private bool oneBlue;
    private bool onePink;
    private bool twoOrange;
    private bool twoGreen;
    private bool twoBlue;
    private bool twoPink;

    private bool colorOne;
    private bool colorTwo;


    private void Start()
    {
        Begin();
    }

    private void Begin()
    {
        orange = false;
        oneOrange = false;
        twoOrange = false;
        green = false;
        oneGreen = false;
        twoGreen = false;
        blue = false;
        oneBlue = false;
        twoBlue = false;
        pink = false;
        onePink = false;
        twoPink = false;

        rb.gravityScale = 3f;
        speed = 8f;

        sr.color = new Color(1, 1, 1);
        sr2.color = new Color(1, 1, 1);
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButton("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }
        if (Input.GetButton("Jump") && IsCeilinged())
        {
            rb.velocity = new Vector2(rb.velocity.x, -jumpingPower);
        }

        //if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        //}

        //dashing
        if (orange)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
            }
        }

        //wallsliding + jumping
        if (green)
        {
            WallSlide();
            WallJump();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ActivateColorOne();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ActivateColorTwo();

        }

        Flip();



    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red; // Set the color of the gizmo
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

    public void ActivateColorOne()
    {
        if (oneOrange)
        {
            orange = true;
            green = false;
            blue = false;
            pink = false;

            rb.gravityScale = 3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(1f, 0.627f, 0.063f);

            Debug.Log("1 orange");
        }
        else if (oneGreen)
        {

            orange = false;
            green = true;
            blue = false;
            pink = false;

            rb.gravityScale = 3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(0.612f, 1f, 0.016f);

            Debug.Log("1 green");
        }
        else if (oneBlue)
        {
            orange = false;
            green = false;
            blue = true;
            pink = false;

            rb.gravityScale = -3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(0.106f, 0.631f, 0.82f);

            Debug.Log("1 blue");
        }
        else if (onePink)
        {
            orange = false;
            green = false;
            blue = false;
            pink = true;

            rb.gravityScale = 3f;
            speed = 12f;
            jumpingPower = 14f;

            sr.color = new Color(0.898f, 0.478f, 0.961f);

            Debug.Log("1 pink");
        }
        else
        {
            orange = false;
            green = false;
            blue = false;
            pink = false;

            rb.gravityScale = 3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(1, 1, 1);
        }

        if (twoOrange)
        {
            sr2.color = new Color(1f, 0.627f, 0.063f);
        }
        else if (twoGreen)
        {
            sr2.color = new Color(0.612f, 1f, 0.016f);
        }
        else if (twoBlue)
        {
            sr2.color = new Color(0.106f, 0.631f, 0.82f);
        }
        else if (twoPink)
        {
            sr2.color = new Color(0.898f, 0.478f, 0.961f);
        }
        else
        {
            sr2.color = new Color(1, 1, 1);
        }
    }

    public void ActivateColorTwo()
    {
        if (twoOrange)
        {
            orange = true;
            green = false;
            blue = false;
            pink = false;

            rb.gravityScale = 3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(1f, 0.627f, 0.063f);

            Debug.Log("2 orange");
        }
        else if (twoGreen)
        {

            orange = false;
            green = true;
            blue = false;
            pink = false;

            rb.gravityScale = 3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(0.612f, 1f, 0.016f);

            Debug.Log("2 green");
        }
        else if (twoBlue)
        {
            orange = false;
            green = false;
            blue = true;
            pink = false;

            rb.gravityScale = -3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(0.106f, 0.631f, 0.82f);

            Debug.Log("2 blue");
        }
        else if (twoPink)
        {
            orange = false;
            green = false;
            blue = false;
            pink = true;

            rb.gravityScale = 3f;
            speed = 16f;
            jumpingPower = 15f;

            sr.color = new Color(0.898f, 0.478f, 0.961f);

            Debug.Log("2 pink");
        }
        else
        {
            orange = false;
            green = false;
            blue = false;
            pink = false;

            rb.gravityScale = 3f;
            speed = 8f;
            jumpingPower = 10f;

            sr.color = new Color(1, 1, 1);
        }

        if (oneOrange)
        {
            sr2.color = new Color(1f, 0.627f, 0.063f);
        }
        else if (oneGreen)
        {
            sr2.color = new Color(0.612f, 1f, 0.016f);
        }
        else if (oneBlue)
        {
            sr2.color = new Color(0.106f, 0.631f, 0.82f);
        }
        else if (onePink)
        {
            sr2.color = new Color(0.898f, 0.478f, 0.961f);
        }
        else
        {
            sr2.color = new Color(1, 1, 1);
        }
    }

    //public void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("white"))
    //    {
    //        orange = false;
    //        green = false;
    //        blue = false;
    //        pink = false;

    //        rb.gravityScale = 3f;
    //        speed = 8f;

    //        sr.color = new Color(1, 1, 1);

    //    }

    //    if (other.CompareTag("orange"))
    //    {
    //        orange = true;
    //        green = false;
    //        blue = false;
    //        pink = false;

    //        rb.gravityScale = 3f;
    //        speed = 8f;

    //        sr.color = new Color(1f, 0.627f, 0.063f);

    //    }

    //    if (other.CompareTag("green"))
    //    {
    //        orange = false;
    //        green = true;
    //        blue = false;
    //        pink = false;

    //        rb.gravityScale = 3f;
    //        speed = 8f;

    //        sr.color = new Color(0.612f, 1f, 0.016f);

    //    }

    //    if (other.CompareTag("blue"))
    //    {
    //        orange = false;
    //        green = false;
    //        blue = true;
    //        pink = false;

    //        rb.gravityScale = -3f;
    //        speed = 8f;

    //        sr.color = new Color(0.106f, 0.631f, 0.82f);

    //    }

    //    if (other.CompareTag("pink"))
    //    {
    //        orange = false;
    //        green = false;
    //        blue = false;
    //        pink = true;

    //        rb.gravityScale = 3f;
    //        speed = 16f;

    //        sr.color = new Color(0.898f, 0.478f, 0.961f);
    //    }
    //}

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private bool IsCeilinged()
    {
        return Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, groundCheckRadius, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("dash");
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    //BUTTONS

    //color bools
    public void OneOrange()
    {
        oneOrange = true;
        oneGreen = false;
        oneBlue = false;
        onePink = false;
    }
    public void OneGreen()
    {
        oneOrange = false;
        oneGreen = true;
        oneBlue = false;
        onePink = false;
    }
    public void OneBlue()
    {
        oneOrange = false;
        oneGreen = false;
        oneBlue = true;
        onePink = false;
    }
    public void OnePink()
    {
        oneOrange = false;
        oneGreen = false;
        oneBlue = false;
        onePink = true;
    }
    public void TwoOrange()
    {
        twoOrange = true;
        twoGreen = false;
        twoBlue = false;
        twoPink = false;
    }
    public void TwoGreen()
    {
        twoOrange = false;
        twoGreen = true;
        twoBlue = false;
        twoPink = false;
    }
    public void TwoBlue()
    {
        twoOrange = false;
        twoGreen = false;
        twoBlue = true;
        twoPink = false;
    }
    public void TwoPink()
    {
        twoOrange = false;
        twoGreen = false;
        twoBlue = false;
        twoPink = true;
    }

}