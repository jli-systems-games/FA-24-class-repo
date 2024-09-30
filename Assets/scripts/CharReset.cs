using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharReset : MonoBehaviour
{

    public Vector3 startPosition;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -40)
        {
            transform.position = startPosition;
        }
    }
}
