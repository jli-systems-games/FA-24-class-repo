using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action enableThermoStat;
    public static event Action<Transform> attachingObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void KnonEnabled()
    {
        //enableThermoStat?.Invoke();
        if(enableThermoStat != null)
        {
            enableThermoStat();
            //Debug.Log("knob can be turn now");
        }
        else
        {
            return;
        }
    }
    public static void assignParent(Transform parent)
    {
        if(attachingObject != null)
        {
            attachingObject(parent);
        }
    }
}
