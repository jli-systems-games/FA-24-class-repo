using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action loadNextLvl;

    public static void load()
    {
        loadNextLvl?.Invoke();
    }

    
}
