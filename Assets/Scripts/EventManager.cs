using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<int> Climb;
    public static event Action<int> stopClimb;
    public static event Action Reset;

    void Start()
    {
        
    }

    public static void climbing(int mouse)
    {
        Climb?.Invoke(mouse);
    }
    public static void stopClimbing(int mouse)
    {
        stopClimb?.Invoke(mouse);
    }

    public static void reload()
    {
        Reset?.Invoke();
    }
}
