using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tpdash : MonoBehaviour
{
    thirdPersonMovement moveScript;

    public float dashSpeed;
    public float dashTime;

    void Start()
    {
        moveScript = GetComponent<thirdPersonMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        Debug.Log("dashed");
        
        float startTime = Time.time;

        while(Time.time < startTime + dashTime)
        {
            moveScript.controller.Move(moveScript.moveDir * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }


}
