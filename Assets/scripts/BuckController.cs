using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckController : MonoBehaviour
{

    public Transform orientation;
    private Rigidbody rb;

    [Header("Movement")]
    public float speed = 3f;
    public float jumpForce = 10f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float playerHeight;
    public bool isGrounded;

    float horizontalInput;
    float verticalInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    private void PlayerMovement()
    {

    }
}
