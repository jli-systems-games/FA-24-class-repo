using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform pong;
    public float speed = 1.5f;
    float steps;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        steps = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, pong.position, steps);

    }
}
