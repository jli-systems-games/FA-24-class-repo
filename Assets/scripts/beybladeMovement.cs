using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class beybladeMovement : MonoBehaviour
{
    public float initialSpinSpeed = 1000f;
    public float spinDecayRate = 5f;
    public float stamina = 200f;
    public float collisionKnockback = 1f;
    public float initialCircleRadius = 1f;
    public float circleSpeed = 2f;
    public float radiusDecayRate = 0.1f;
    public float centerRandomRange = 0.5f;
    public float centerChangeSpeed = 0.5f;

    private Rigidbody2D rb;
    private float currentSpinSpeed;
    private float currentCircleRadius;
    private Vector2 startPosition;
    private Vector2 centerPosition;
    private Vector2 targetCenterPosition;
    private float angle;
    private bool isCenterMoving = true;

    public TMP_Text staminaText;
    public float minSize = 10f;
    public float maxSize = 50f;
    public float increaseSpeed = 5f;
    private float currentSize;
    public beybladeMovement2 opponentStamina;

    public float collisionPauseDuration = 0.2f;
    private float collisionPauseTimer = 0f;
    private bool isKnockedBack = false; 

    

    void Start()
    {
        GameObject stamtext = GameObject.FindWithTag("stamina1");
        staminaText = stamtext.GetComponent<TextMeshProUGUI>();
        GameObject beyblade2 = GameObject.FindWithTag("Beyblade2");
        opponentStamina = beyblade2.GetComponent<beybladeMovement2>();

        rb = GetComponent<Rigidbody2D>();
        currentSpinSpeed = initialSpinSpeed;

        // centerPosition = rb.position + new Vector2(
        //     Random.Range(-centerRandomRange, centerRandomRange),
        //     Random.Range(-centerRandomRange, centerRandomRange)
        // );
        startPosition = rb.position;
        targetCenterPosition = new Vector2(0,0);
        currentCircleRadius = Vector2.Distance(targetCenterPosition, startPosition);

        rb.position = new Vector2(transform.position.x, transform.position.y);

        rb.AddTorque(initialSpinSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
        //staminaText.text = "green stamina: " + stamina;

        //decay spin speed over time
        if (stamina > 0)
        {
            if (currentSpinSpeed > 0)
            {
                currentSpinSpeed -= spinDecayRate * Time.deltaTime;
                rb.AddTorque(-spinDecayRate * Time.deltaTime, ForceMode2D.Impulse);
            }
            

            stamina -= Time.deltaTime;

            if (currentCircleRadius > 0)
            {
                currentCircleRadius -= radiusDecayRate * Time.deltaTime;
            }
        }

        else
        {
            //when stamina is exhausted stop beyblade
            currentSpinSpeed = 0;
            rb.angularDrag = 5f;
        }

        if (currentCircleRadius <= 1)
        {
            isCenterMoving = false;
            // targetCenterPosition = new Vector2(0,0);
            // centerPosition = Vector2.Lerp(centerPosition, targetCenterPosition, centerChangeSpeed * Time.deltaTime);

        }

        if (isKnockedBack)
        {
            collisionPauseTimer -= Time.deltaTime;
            if (collisionPauseTimer <= 0f)
            {
                //rb.isKinematic = false;
                isKnockedBack = false;
            }
        }
        else
        {
            angle += circleSpeed * Time.deltaTime;
            // if (isCenterMoving)
            // {
            //     MoveCenterOverTime();
            // }
            ApplyCircularMovement();
        }

        if (stamina <= 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            //rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        if (stamina > 0 && stamina > opponentStamina.stamina && opponentStamina.stamina > 0)
        {
            currentSize += increaseSpeed * Time.deltaTime;
            currentSize = Mathf.Clamp(currentSize, minSize, maxSize);

            staminaText.fontSize = Mathf.RoundToInt(currentSize);
        }

        if (stamina > 0 && stamina < opponentStamina.stamina && opponentStamina.stamina > 0)
        {
            currentSize -= increaseSpeed * Time.deltaTime;
            currentSize = Mathf.Clamp(currentSize, minSize, maxSize);

            staminaText.fontSize = Mathf.RoundToInt(currentSize);
        }
        
    }

    void MoveCenterOverTime()
    {
        if (Vector2.Distance(centerPosition, targetCenterPosition) < 0.1f)
        {
            //pick a new target when center is close enough to target
            targetCenterPosition = centerPosition + new Vector2(
                Random.Range(-centerRandomRange, centerRandomRange),
                Random.Range(-centerRandomRange, centerRandomRange));
        }

        centerPosition = Vector2.Lerp(centerPosition, targetCenterPosition, centerChangeSpeed * Time.deltaTime);
    }

    void ApplyCircularMovement()
    {
        //calculate the new position
        float offsetX = Mathf.Cos(angle) * currentCircleRadius;
        float offsetY = Mathf.Sin(angle) * currentCircleRadius;

        //set the position around the changing center
        Vector2 newPosition = centerPosition + new Vector2(offsetX, offsetY);
        rb.MovePosition(newPosition);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Beyblade2") && stamina > 0)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            float increasedKnockback = collisionKnockback * Random.Range(1f,2f);
            rb.AddForce(-knockbackDirection * increasedKnockback, ForceMode2D.Impulse);

            currentCircleRadius += Random.Range(0.05f, 0.25f);
            
            stamina -= Random.Range(1f,10f);
        }
    }
}