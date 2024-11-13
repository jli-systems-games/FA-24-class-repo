using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beybladeMovement : MonoBehaviour
{
    public float initialSpinSpeed = 500f;
    public float spinDecayRate = 5f;
    public float stamina = 200f;
    public float collisionKnockback = 5f;
    public float initialCircleRadius = 1f;
    public float circleSpeed = 2f;
    public float radiusDecayRate = 0.1f;
    public float centerRandomRange = 0.5f;
    public float centerChangeSpeed = 0.5f;

    private Rigidbody2D rb;
    private float currentSpinSpeed;
    private float currentCircleRadius;
    private Vector2 centerPosition;
    private Vector2 targetCenterPosition;
    private float angle;
    private bool isCenterMoving = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpinSpeed = initialSpinSpeed;
        currentCircleRadius = initialCircleRadius;

        centerPosition = rb.position + new Vector2(
            Random.Range(-centerRandomRange, centerRandomRange),
            Random.Range(-centerRandomRange, centerRandomRange)
        );
        targetCenterPosition = centerPosition;

        rb.AddTorque(initialSpinSpeed, ForceMode2D.Impulse);
    }

    void Update()
    {
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

        // If the radius is 0, stop moving the center
        if (currentCircleRadius <= 1)
        {
            isCenterMoving = false;
            targetCenterPosition = new Vector2(0,0);
            centerPosition = Vector2.Lerp(centerPosition, targetCenterPosition, centerChangeSpeed * Time.deltaTime);

        }

        angle += circleSpeed * Time.deltaTime;

        if (isCenterMoving)
        {
            MoveCenterOverTime();
        }

        ApplyCircularMovement();

        if (stamina <= 0 && currentCircleRadius > 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
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
        if (collision.gameObject.CompareTag("Beyblade") && stamina > 0)
        {
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            float increasedKnockback = collisionKnockback * 5f;
            rb.AddForce(-knockbackDirection * increasedKnockback, ForceMode2D.Impulse);

            
            stamina -= Random.Range(5f,10f);
        }
    }
}