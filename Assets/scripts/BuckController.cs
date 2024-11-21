using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckController : MonoBehaviour
{
    public int id;
    public bool isActive = false;
    public bool canSwitch = false; 
    public float switchHoldTime = 1f; 

    private float switchTimer = 0f; 
    public Transform orientation;
    private Rigidbody rb;

    public GameObject HoldE;
    public GameObject Light;
    public GameObject view;

    [Header("Movement")]
    public float speed = 3f;
    public float drag = 4f;
    public float airMult = 4f;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float playerHeight;
    public bool isGrounded;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    private void Start()
    {
        HoldE.SetActive(false);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GroundCheck();

        
        if (isActive)
        {
            GetInput();
            SpeedControl();

            if (isGrounded)
                rb.drag = drag;
            else
                rb.drag = 0f;
        }
        
        HandleSwitch();

    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            PlayerMovement();
            Light.SetActive(true);
            view.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
            view.SetActive(false);
        }
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
    }

    private void PlayerMovement()
    {
        if (isGrounded)
            rb.AddForce(moveDirection * speed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection * speed * 10f * airMult, ForceMode.Force);
    }

    private void GroundCheck()
    {
        float distToGround = playerHeight * 0.5f + 0.2f;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround, groundLayer);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void HandleSwitch()
    {
        if (canSwitch && Input.GetKey(KeyCode.E))
        {
            switchTimer += Time.deltaTime;
            Debug.Log("E");
           Debug.Log($"Switch Timer: {switchTimer}");

            SwitchControl();

             if (switchTimer >= switchHoldTime)
             {
                 Debug.Log("switch");
                 SwitchControl();
                 switchTimer = 0f; 
             }
        }
         else
         {
             switchTimer = 0f; 
         }
    }

    private void SwitchControl()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        foreach (Collider col in colliders)
        {
            BuckController otherController = col.GetComponent<BuckController>();
            if (otherController != null && otherController.id != this.id && !otherController.isActive)
            {
                
                otherController.isActive = true;
                otherController.rb.velocity = Vector3.zero;
                this.isActive = false;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BuckController otherController = other.GetComponent<BuckController>();
            if (otherController != null && otherController.isActive != this.isActive)
            {
                canSwitch = true;
                HoldE.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSwitch = false;
            HoldE.SetActive(false);
        }
    }
}
