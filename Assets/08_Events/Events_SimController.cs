using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events_SimController : MonoBehaviour
{
    //public UnityEvent onSimDeath;

    //public delegate void SimDeath();
    //public event SimDeath onSimDeath;

    public static event Action onSimDeath;
    public static event Action<int> onSimTakeDamage;



    // Start is called before the first frame update
    void Start()
    {
        //onSimDeath.Invoke();

        onSimDeath?.Invoke();
        onSimTakeDamage?.Invoke(2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
