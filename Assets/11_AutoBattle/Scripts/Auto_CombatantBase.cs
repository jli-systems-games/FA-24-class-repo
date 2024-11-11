using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CombatantState
{
    Attacking,
    Moving,
    Charging,
    Dead
}
public class Auto_CombatantBase : MonoBehaviour
{
    public CombatantState state = CombatantState.Charging;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
