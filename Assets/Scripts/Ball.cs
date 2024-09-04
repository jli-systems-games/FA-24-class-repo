using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float thrust;
    private Vector3 ballPos;
    private Rigidbody2D rb;

    private int initialDir;
    private float initialThrust;

    private int upDown;

    private Vector3 currentDirection;
    private Vector3 prevPos;
    private Vector3 currentPos;

    [SerializeField] private GameObject P1;
    [SerializeField] private GameObject P2;

    [SerializeField] private GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = transform.position;
        prevPos = transform.position;

        rb = GetComponent<Rigidbody2D>();

        initialDir = Random.Range(0, 2);
        initialThrust = thrust;

        if(initialDir == 0)
        {
            rb.AddForce(transform.right * thrust);
            rb.AddForce(transform.up * thrust);
        }
        if (initialDir == 1) 
        {
            rb.AddForce(transform.right * -thrust);
            rb.AddForce(transform.up * -thrust);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        currentPos = transform.position;

        currentDirection = currentPos - prevPos;

        //Debug.Log("current direction: " + currentDirection);

        prevPos = transform.position;

        rb.velocity = new Vector2 (0,0);

        if (other.gameObject.CompareTag("P1") || other.gameObject.CompareTag("P2"))
        {
            thrust = (float)(thrust * 1.2);
            if (transform.position.y <= 0)
            {
                rb.AddForce(transform.up * thrust);
            }

            else
            {
                rb.AddForce(transform.up * -thrust);
            }
        }
        if (other.gameObject.CompareTag("top"))
        {
            rb.AddForce(Vector3.up * -initialThrust);

            if (currentDirection.x >= 0) 
            {
                rb.AddForce(Vector3.right * thrust);
            }

            if(currentDirection.x < 0)
            {
                rb.AddForce(Vector3.right * -thrust);
            }
        }
        else if (other.gameObject.CompareTag("bottom")) 
        {
            rb.AddForce(Vector3.up * initialThrust);

            if (currentDirection.x >= 0)
            {
                rb.AddForce(Vector3.right * thrust);
            }

            if (currentDirection.x < 0)
            {
                rb.AddForce(Vector3.right * -thrust);
            }
        }

        else if (other.gameObject.CompareTag("P1"))
        {
            rb.AddForce(transform.right * thrust);

        }
        else if (other.gameObject.CompareTag("P2"))
        {
            rb.AddForce(transform.right * -thrust);
        }

        else
        {
            if (other.gameObject.CompareTag("left"))
            {
                P2.GetComponent<Player>().score++;
            }

            if (other.gameObject.CompareTag("right")) 
            {
                P1.GetComponent<Player>().score++;
            }

            Debug.Log("out of bounds");
            thrust = initialThrust;
            StartCoroutine(changeBG());
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        //Debug.Log("ball velocity: " + rb.velocity);
    }

    private IEnumerator changeBG() 
    {
        background.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1.5f);

        background.GetComponent <SpriteRenderer>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Instantiate(gameObject, ballPos, Quaternion.identity);
        Destroy(gameObject);
    }
}
