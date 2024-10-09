using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrack : MonoBehaviour
{
    [SerializeField] int targetChildCount;
    bool havechanged;
    //[SerializeField] PickUpNRotate knobTurn;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount >= targetChildCount)
        {
            //go through children;
            if(!havechanged)
            {
                EventManager.KnonEnabled();
                havechanged = true;
            }
            
        }
    }
    
}
