using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rb;
    public float thrustY;
    public float thrustZ;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(0, 1, -1, ForceMode.Impulse);
            //thrustY = thrustY + 1f;
            //thrustZ = thrustZ - 1f;
        }
        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            //_rb.AddForce(0,thrustY,thrustZ,ForceMode.Impulse);
            thrustY = 0;
            thrustZ = 0;
        }
        if(transform.rotation.x < 0 || transform.rotation.x > 360)
        {
            Debug.Log("fucking died");
            //Destroy(GetComponent<FixedJoint>());
        }
    }
}
