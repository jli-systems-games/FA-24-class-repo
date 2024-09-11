using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 3f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public GameObject cat;
    public TextMeshProUGUI uiText;
    public string victory = "Victory!";
    public string failed = "You got caught!";
    public Collider2D goalCollider;

    private bool isGameOver = false; // to help with the stopmovement function

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (isGameOver) return;

        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        if (!isGameOver)
        {
            rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == cat)
        {
            // cat wins
            uiText.text = failed;
            isGameOver = true;
            StopMovement();
        }
        else if (other == goalCollider)
        {
            // mouse wins
            uiText.text = victory;
            isGameOver = true;
            StopMovement();
        }
    }

    private void StopMovement()
    {
        // stops mouse
        rb.velocity = Vector2.zero;

        // stops cat
        if (cat != null)
        {
            Animation catAnimation = cat.GetComponent<Animation>();
            if (catAnimation != null)
            {
                catAnimation.Stop();
            }
        }
    }
}
