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
    private Rigidbody rb;
    Vector3 ogPosition;
    Quaternion ogRotation;
    bool parentChange = false;
    GameManager gManage;
    public objectState currentState = objectState.setDown;
    void Start()
    {
         //rb = GetComponent<Rigidbody>();
         ogPosition = transform.position;
         ogRotation = transform.rotation;
        newParent = transform.parent;
        specialParent = GameObject.Find("Event1Object");
        gManage = GameObject.FindGameObjectWithTag("gamerManager").GetComponent<GameManager>();
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
        //rb.useGravity = false;
    }

    public void Drop()
    {
        bool attached = false;
        if (!parentChange)
        {
            if (transform.tag == "Untagged")
            {
                newParent = specialParent.transform;
            }
            transform.SetParent(newParent);
            transform.position = ogPosition;
            transform.rotation = ogRotation;
        }
        else if (transform.tag == "organs" || parentChange)
        {
            transform.SetParent(newParent);
            transform.position = newParent.position;
            transform.rotation = newParent.rotation;
            parentChange = false;
            //gManage.checkJarStatus();
        }
        else if (transform.tag == "jar" || attached)
        {
            transform.SetParent(null);
            transform.position = ogPosition;
            transform.rotation = ogRotation;
            attached = false;
        }
        
        ChangeState(objectState.setDown);
    }

    public void ChangeState(objectState state)
    {
        currentState = state;
        //notifyEvents

    }
    private void assignParent(Transform _nparent)
    {
        if (!parentChange)
        {
            newParent = _nparent;
            parentChange = true;
        }
        
    }
}
