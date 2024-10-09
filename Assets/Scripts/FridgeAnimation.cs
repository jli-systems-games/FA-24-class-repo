using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeAnimation : MonoBehaviour
{
    
    Animator doorAnimate;

    void Start()
    {
        doorAnimate = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void doorOpens(bool tryingtoOpen)
    {
        if (tryingtoOpen)
        {
            //play door animation if the state is not closing 
            Debug.Log("trying to open");
            if (doorAnimate.GetCurrentAnimatorStateInfo(0).IsName("close"))
            {
                doorAnimate.ResetTrigger("close");
            }
            doorAnimate.SetTrigger("open");
        }
        else
        {
            //play door close animation if the state is not opening,
            if (doorAnimate.GetCurrentAnimatorStateInfo(0).IsName("open"))
            {
                doorAnimate.ResetTrigger("open");
            }
            doorAnimate.SetTrigger("close");
        }
    }
}
