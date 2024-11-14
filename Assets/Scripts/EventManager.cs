using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static event Action<string,int> gotHit;
    public static event Action changeTarget;
    public static event Action<LevelState> killedOff;
    public static event Action<Level> decideCount;

    void Start()
    {
        //DontDestroyOnLoad(gameObject);

    }

    public static void harming(string id, int damage)
    {
        gotHit?.Invoke(id,damage);
    }
    public static void newTarget()
    {
        changeTarget?.Invoke();
    }
    public static void ChangeState(LevelState state)
    {
        killedOff?.Invoke(state);
    }
    public static void fetchTools(Level L)
    {

        if(decideCount != null)
        {
            decideCount(L);
        }
        else
        {
            Debug.Log("0");
        }

    }
  
}
