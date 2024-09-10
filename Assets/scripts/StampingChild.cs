using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StampingChild : MonoBehaviour //, IPointerDownHandler
{
    public StampingParent Grandparent;
    public Transform parent;
    public PlayerHit plyr;
    
    /*RaycastHit hit;
    Ray ray;*/
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if (plyr.isLeftMouseClick)
        {
               /* if (plyr.rayCastSuccessful && plyr.hit.collider.gameObject.tag == "Hold")
                {
                    Grandparent.Score.Add(1);
                }
                else
                {
                    Grandparent.Score.Add(0);
                }*/
            
        }
        else 
        {
            
                /*if (plyr.rayCastSuccessful && plyr.hit.collider.gameObject.tag == "Return")
                {
                    Debug.Log("correct");
                    Grandparent.Score.Add(1);
                }
                else
                {
                    Grandparent.Score.Add(0);
                }*/
            
        }

    }
   
   
   
}
