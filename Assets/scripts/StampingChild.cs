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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        

    }

    private void OnDisable()
    {
        Debug.Log("detroying");
        foreach(Transform t in transform)
        {
            if (t.CompareTag("stamp"))
            {
                Debug.Log(t);
                Destroy(t.gameObject);
            }
        }
        
    }



}
