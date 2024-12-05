using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuckController : MonoBehaviour
{
    public int id;
    public bool isActive = false;
    public bool canSwitch = false;
    public float switchHoldTime = 1f;

    //private float switchTimer = 0f;
    public static float globalSwitchCooldown = 1f;
    public static float lastSwitchTime = -Mathf.Infinity;

    public Transform orientation;
    private Rigidbody rb;

    public GameObject HoldE;
    public GameObject Light;
    public GameObject spark;

    [Header("Movement")]
    public float speed = 3f;
    public float drag = 4f;
    public float airMult = 4f;
    //always flase can switch
    [Header("Ground Check")]
    public LayerMask groundLayer;
    public float playerHeight;
    public bool isGrounded;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;

    [Header("Shrink Effect")]
    public float shrinkRate = 0.1f;
    public float minScaleY = 0.5f;
    public float shrinkHeightRate = 0.05f;
    private float maxScaleY; 

    public Image bar;
    public Image timer;
    public GameObject eTimer;
    public GameObject playerCanvas;

    //public GameManager gameManager;
    // public CinemachineVirtualCamera buckCamera;

    private void Start()
    {
        HoldE.SetActive(false);
        eTimer.SetActive(false);
        maxScaleY = transform.localScale.y;

        GameManager.Instance.RegisterController(this);
        //gameManager = 
    }

    private void OnDestroy()
    {
        GameManager.Instance.UnregisterController(this); 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver == false)
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

                ApplyShrinkEffect();

                HandleSwitch();
            }

            

        }
    }

    private void FixedUpdate()
    {
        //if (GameManager.Instance.isGameOver == false)
       // {
            if (isActive)
            {
                PlayerMovement();
                Light.SetActive(true);
                spark.SetActive(true);
                playerCanvas.SetActive(true);
            }
            else
            {

                Light.SetActive(false);
                spark.SetActive(false);
                playerCanvas.SetActive(false);
            }
        //}
    }

    private void ApplyShrinkEffect()
    {
        Vector3 scale = transform.localScale;
        Vector3 position = transform.position;

        if (scale.y > minScaleY)
        {
            scale.y -= shrinkRate * Time.deltaTime;
            position.y -= shrinkHeightRate * Time.deltaTime;

            transform.localScale = scale;
            transform.position = position;
        }

        float shrinkProgress = Mathf.Clamp01((scale.y - minScaleY) / (maxScaleY - minScaleY));
        if (bar != null)
        {
            bar.fillAmount = shrinkProgress;
        }

        if (scale.y <= minScaleY && isActive) 
        {
            isActive = false;
            canSwitch = false;
            GameManager.Instance.CheckGameOver();
           // Debug.Log("Controller has shrunk to minimum Y scale and is now inactive.");
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
        if (Time.time - lastSwitchTime < globalSwitchCooldown) return;
        //Debug.Log(":)");

        if (canSwitch && Input.GetKey(KeyCode.E))
        {
            
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
            eTimer.SetActive(true);

            GameManager.Instance.switchTimer += Time.deltaTime;

            float progress = GameManager.Instance.switchTimer / switchHoldTime;
            timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);

            if (GameManager.Instance.switchTimer >= switchHoldTime)
            {
                //eTimer.GetComponent<Image>().alpha = 0f;
                eTimer.SetActive(false);

                HoldE.GetComponent<TextMeshProUGUI>().alpha = 1f;
                timer.GetComponent<Image>().fillAmount = 0f;

                SwitchControl();
                GameManager.Instance.switchTimer = 0f;
                lastSwitchTime = Time.time;      
            }
        }
        else if (isActive == true)
        {
            GameManager.Instance.switchTimer = 0f;
            eTimer.SetActive(false);
          
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 1f;
            timer.GetComponent<Image>().fillAmount = 0f;
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
