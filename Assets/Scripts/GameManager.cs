using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action loadNextLvl;
    public static event Action cutaway;

    public static void load()
    {   
        
        loadNextLvl?.Invoke();
    }
    public static void clearLevel()
    {   
        InputManager.points.Clear();
        cutaway?.Invoke();

    }
    
}
