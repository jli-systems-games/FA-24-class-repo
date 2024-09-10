using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StampingChild : MonoBehaviour //, IPointerDownHandler
{
    
    public Transform parent;
    public PlayerHit plyr;
    public GameObject holdStamp;
    public GameObject returnStamp;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (plyr.isLeftMouseClick && plyr.hitObject == parent.name)
        {
               holdStamp.SetActive(true);
            returnStamp.SetActive(false);
        }
        else if(!plyr.isLeftMouseClick && plyr.hitObject == parent.name)
        {
            
               returnStamp.SetActive(true);
                holdStamp.SetActive(false);
        }

    }

    private void OnDisable()
    {
        if (holdStamp.activeSelf)
        {
            holdStamp.SetActive(false);
        }

        if (returnStamp.activeSelf)
        {
            returnStamp.SetActive(false);
        }
        
    }



}
