using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    
    //public float acceleration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }

    // Update is called once per frame
    void Update()   
    {

    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);

    }

    public void IncreaseSpeed()
    {
        float tempX = rb.velocity.x;
        float tempY = rb.velocity.y;
        rb.velocity = new Vector2(tempX * 1.1f, tempY * 1.1f);
    }
}