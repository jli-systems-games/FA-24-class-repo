using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Events_SimController : MonoBehaviour
{
    //method 1
    //public UnityEvent onSimDeath;

    //method 2
    //public delegate void SimDeath();
    //public static event SimDeath onSimDeath;

    //method 3
    public static event Action onSimDeath;

    // Start is called before the first frame update
    void Start()
    {
        //method 1
        //onSimDeath.Invoke();

        //method 2
        onSimDeath?.Invoke();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
