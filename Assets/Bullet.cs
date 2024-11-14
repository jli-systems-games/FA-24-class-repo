using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;

    private Vector2 spawnPoint;
    private float timer = 0f;


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);

    }

    //public void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //Debug.Log("Collision detected with: " + collision.gameObject.name);
    //    if (collision.gameObject.CompareTag("Walls"))
    //    {
    //        Destroy(gameObject);
    //    }

    //    if (gameObject.CompareTag("B1") && collision.gameObject.CompareTag("2"))
    //    {
    //        Debug.Log("hit p2");
    //        Destroy(gameObject);
    //    }


    //    if (gameObject.CompareTag("B2") && collision.gameObject.CompareTag("1"))
    //    {
    //        Debug.Log("hit p1");
    //        Destroy(gameObject);
    //    }
    //}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("B1") && collision.gameObject.CompareTag("2"))
        {
            Debug.Log("hit p2");
            Destroy(gameObject);

            GameObject.Find("player 2").GetComponent<PlayerOne>().p1_hp--;

        }


        if (gameObject.CompareTag("B2") && collision.gameObject.CompareTag("1"))
        {
            Debug.Log("hit p1");
            Destroy(gameObject);

            GameObject.Find("player 1").GetComponent<PlayerOne>().p1_hp--;
        }
    }



    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }
}