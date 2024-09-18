using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontal;
    float vertical;

    public float runSpeed;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        //body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);


        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * runSpeed, Input.GetAxis("Vertical") * runSpeed);

        body.velocity = transform.rotation * new Vector3(targetVelocity.x, body.velocity.y, targetVelocity.y);

    }
}
