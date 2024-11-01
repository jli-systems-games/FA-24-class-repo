using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float initialSpeed;
    public float speedIncreaseFactor;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    public List<Sprite> images;
    private SpriteRenderer spriteRenderer;

    private float currentSpeed;
    private float timeElapsed;
    private int triggerCollisionCount;
    private int resetCount;

    void Start()
    {
        startPosition = transform.position;
        currentSpeed = initialSpeed;
        timeElapsed = 0;
        triggerCollisionCount = 0;
        resetCount = 0;
        spriteRenderer = GetComponent<SpriteRenderer>();

        
        spriteRenderer.sprite = images[0];
        StartCoroutine(LaunchAfterDelay());
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        currentSpeed = initialSpeed;
        timeElapsed = 0;
        triggerCollisionCount = 0;
        resetCount++;
        spriteRenderer.sprite = images[0]; 
        StartCoroutine(LaunchAfterDelay());
    }

    public int GetResetCount()
    {
        return resetCount;
    }

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(0.6f);
        Launch();
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(currentSpeed * x, currentSpeed * y);
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        currentSpeed = initialSpeed + speedIncreaseFactor * timeElapsed;
        rb.velocity = rb.velocity.normalized * currentSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Trigger"))
        {
            triggerCollisionCount++;

            
            if (triggerCollisionCount < images.Count)
            {
                spriteRenderer.sprite = images[triggerCollisionCount];
            }

            
            if (triggerCollisionCount >= 3)
            {
                Reset();
                GameObject.Find("GameManager").GetComponent<GameManager>().ResetBallPrice();
            }
        }
    }
}
