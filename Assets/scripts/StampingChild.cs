using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StampingChild : MonoBehaviour //, IPointerDownHandler
{

    /* public Transform parent;
     public PlayerHit plyr;
     public GameObject holdStamp;
     public GameObject returnStamp;*/

    Vector3 ogPos;
    public BoxCollider collide;
    void Start()
    {
        
        if (!collide.enabled)
        {
            collide.enabled = true;
        }
        
    }
    private void OnEnable()
    {   
        if (!collide.enabled)
        {
            collide.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            if(transform.position.x <= -7f) {
            collide.enabled = false;
            //Debug.Log(transform.position.x);
            }
        }
        
    }

    private void OnDisable()
    {
        //Debug.Log("detroying");
        foreach(Transform t in transform)
        {
            if (t.CompareTag("stamp"))
            {
                
                Destroy(t.gameObject);
            }
        }

        collide.enabled = true;
        
    }



}
