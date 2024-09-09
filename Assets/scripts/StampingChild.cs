using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StampingChild : MonoBehaviour, IPointerDownHandler
{
    public StampingParent Grandparent;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("CLicked!");
            if (gameObject.tag == "Hold")
            {
                Grandparent.Score.Add(1);
            }
            else
            {
                Grandparent.Score.Add(0);
            }
        }
        
        if(eventData.button == PointerEventData.InputButton.Left)
        {
           // Debug.Log("leftClicked!");
            if (gameObject.tag == "Return")
            {
                Debug.Log("correct");
                Grandparent.Score.Add(1);
            }
            else
            {
                Grandparent.Score.Add(0);
            }
        }
        parent.position = new Vector3( transform.position.x - 325f, transform.position.y,0);
    }
}
