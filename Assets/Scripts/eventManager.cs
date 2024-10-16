using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventManager : MonoBehaviour
{
    public static event Action<Transform> goFetch;
    public static event Action resetEnemyState;
    public static event Action <GameObject, string> decreaseBoredom;
    public static event Action<GameObject> switchItems;
    public static event Action<GameObject, string> manageHunger;
    public static event Action<GameObject, GameObject, string> chickenChecks;
    public static event Action<GameObject> triggerAttack;
    public static event Action resetAttack;
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
    public static void switchItem(GameObject obj)
    {
        switchItems?.Invoke(obj);
    }
    public static void calcHunger(GameObject obj, string sender)
    {
        manageHunger?.Invoke(obj, sender);
    }
    public static void countChicks(GameObject hObj, GameObject iObj, string message)
    {
        chickenChecks?.Invoke(hObj, iObj, message);
    }
    public static void startAttack(GameObject bar)
    {
        triggerAttack?.Invoke(bar);
    }
    public static void ResetAttk()
    {
        resetAttack?.Invoke();
    }
    
}
