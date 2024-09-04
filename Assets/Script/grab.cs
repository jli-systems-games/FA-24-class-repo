using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{

    //private bool hold;
    private FixedJoint2D currentJoint;
    public KeyCode grabKey = KeyCode.E; 
    public KeyCode releaseKey = KeyCode.R;

    [HideInInspector]
    public bool isHoldingBall = false;


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey(mouseButton))
        //{
        //    hold = true;
        //}
        //else
        //{
        //    hold = false;
        //    Destroy(GetComponent<FixedJoint2D>());
        //}

        if (Input.GetKeyDown(releaseKey) && currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;

            isHoldingBall = false;
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        //if (hold)
        //{
        //    Rigidbody2D rb = col.transform.GetComponent<Rigidbody2D>();
        //    if (rb != null)
        //    {
        //        FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
        //        fj.connectedBody = rb;
        //    }
        //    else
        //    {
        //        FixedJoint2D fj = transform.gameObject.AddComponent(typeof(FixedJoint2D)) as FixedJoint2D;
        //    }

        //}

        if (Input.GetKey(grabKey) && currentJoint == null)
        {
            Rigidbody2D rb = col.collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                currentJoint = gameObject.AddComponent<FixedJoint2D>();
                currentJoint.connectedBody = rb;

                isHoldingBall = true;
            }
        }
    }
}