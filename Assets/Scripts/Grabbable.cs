using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum objectState
{
    pickedUp,
    setDown,
}
public class Grabbable : MonoBehaviour
{
    Transform newParent;
    private Transform holdPointTransform;
    private GameObject specialParent;
    private SphereCollider col;
    Vector3 ogPosition, newScale,ogScale,holdingObj;
    Quaternion ogRotation;
    bool parentChange = false;
    GameManager gManage;
    public objectState currentState = objectState.setDown;
    void Start()
    {
         //rb = GetComponent<Rigidbody>();
         ogPosition = transform.position;
         ogRotation = transform.rotation;
        ogScale = transform.localScale;
        newParent = transform.parent;
        specialParent = GameObject.Find("Event1Object");
        gManage = GameObject.FindGameObjectWithTag("gamerManager").GetComponent<GameManager>();
        col = GetComponent<SphereCollider>();
        EventManager.attachingObject += assignParent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {   

    }
    public void Grab(Transform holdPoint, Transform camView)
    {
        this.holdPointTransform = holdPoint;
        transform.SetParent(holdPoint);
        transform.localPosition = holdPoint.localPosition;
        transform.position = holdPoint.position;
        if(newScale != null)
        {
            transform.localScale = ogScale;
            holdingObj = transform.localScale;
        }
        //rb.useGravity = false;
    }

    public bool Drop(Transform hitted)
    {
        bool attached = false;
        Debug.Log(transform.tag);
      
        if (!parentChange)
        {
            if (hitted.tag == "Untagged")
            {
                newParent = specialParent.transform;
            }
            hitted.SetParent(newParent);
            Debug.Log("nP" + newParent);
            hitted.position = ogPosition;
            hitted.rotation = ogRotation;
            return attached;
        }
       if (hitted.tag == "organs")
        {
            hitted.SetParent(newParent);
            Debug.Log("nP" + newParent);
            hitted.localScale = newScale;
            hitted.localPosition = Vector3.zero;
            hitted.localPosition = new Vector3(0, hitted.localPosition.y + 2.5f, 0);
            hitted.rotation = newParent.rotation;

           
            parentChange = false;
            return attached;
        }
        else if (hitted.tag == "jar" || attached)
        {
            hitted.SetParent(null);
            hitted.position = ogPosition;
            hitted.rotation = ogRotation;
            attached = false;
            return attached;
        }
        return true;

    }

    public void ChangeState(objectState state)
    {
        currentState = state;
        //notifyEvents

    }
    private void assignParent(Transform _nparent)
    {
       
          newParent = _nparent;
       
        float desiredScale = 0.5f;
      
        Debug.Log(desiredScale);
          newScale = new Vector3(
            desiredScale / newParent.lossyScale.x,
            desiredScale / newParent.lossyScale.y,
            desiredScale / newParent.lossyScale.z
            );
          parentChange = true;
     
        
    }
}
