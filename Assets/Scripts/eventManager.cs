using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public static event Action<Transform> goFetch;
    public static event Action resetEnemyState;
    public static event Action <GameObject, string> decreaseBoredom;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void startFetch(Transform t)
    {
        goFetch?.Invoke(t);
    }
    public static void resetEnemy()
    {
        resetEnemyState?.Invoke();
    }
    public static void decreaseB(GameObject obj, string sender)
    {
        decreaseBoredom?.Invoke(obj, sender);
    }
}
