using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    private Vector3 direction;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        this.direction = new Vector3(1f, 0f, 1f); 

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * speed;
    }

    private void OnCollisionEnter(Collision col)
    {
        Vector3 normal = col.contacts[0].normal;
        direction = Vector3.Reflect(direction, normal);
    }
}
