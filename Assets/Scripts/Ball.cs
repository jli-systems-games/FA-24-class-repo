using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    private Vector3 ballPos;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
