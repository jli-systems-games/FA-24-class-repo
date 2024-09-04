using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Launch();
    }

    private void Launch()
    {
        float xVecocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVecocity = Random.Range(0, 2) == 0 ? 1 : -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
