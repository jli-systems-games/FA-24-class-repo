using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballRolling : MonoBehaviour
{
    public Rigidbody rb;

    public float rollingSpeed;
    public float sizeMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SnowballGetBigger()
    {

        //if (rb.velocity > Vector2.zero)
        //{
        //    sizeMultiplier = 1.1f;
        //    transform.localScale = sizeMultiplier * rb.velocity.magnitude;
        //}


    }
}
