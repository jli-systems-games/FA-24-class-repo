using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float thrust;
    private Vector3 ballPos;
    private Rigidbody2D rb;

    private int initialDir;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = transform.position;
        rb = GetComponent<Rigidbody2D>();

        initialDir = Random.Range(0, 1);

        if(initialDir == 0)
        {
            rb.AddForce(transform.right * thrust);
        }
        if (initialDir == 1) 
        {
            rb.AddForce(transform.right * -thrust);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("top"))
        {
            rb.AddForce(transform.up * - thrust);
        }
        if (other.gameObject.CompareTag("bottom")) 
        {
            rb.AddForce(transform.up * thrust);
        }

        if (other.gameObject.CompareTag("P1"))
        {
            rb.AddForce(transform.right * thrust);
        }
        if (other.gameObject.CompareTag("P2"))
        {
            rb.AddForce(transform.up * -thrust);
        }

        else
        {
            Instantiate(gameObject, ballPos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
