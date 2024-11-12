using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string> gotHit;
    public static event Action changeTarget;
    public static event Action<LevelState> killedOff;

    void Start()
    {
        
    }

    public static void harming(string id)
    {
        gotHit?.Invoke(id);
    }
    public static void newTarget()
    {
        changeTarget?.Invoke();
    }
    public static void ChangeState(LevelState state)
    {
        killedOff?.Invoke(state);
    }
}
