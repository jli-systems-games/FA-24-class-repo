using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddles : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private bool isPaddle1;
    private float yBound = 6f;
    
    // Update is called once per frame
    void Update()
    {
        float movement;

        if (isPaddle1)
        {
            movement = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movement = Input.GetAxisRaw("Vertical2");
        }

        //float movement = Input.GetAxisRaw("Vertical");
        //transform.position += new Vector3(0, movement * speed * Time.deltaTime);

        Vector2 paddlePositon = transform.position;
        paddlePositon.y = Mathf.Clamp(paddlePositon.y + movement * speed * Time.deltaTime, -yBound, yBound);
        transform.position = paddlePositon;

    }
}
