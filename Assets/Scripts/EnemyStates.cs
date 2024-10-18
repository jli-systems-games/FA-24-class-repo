using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CryptidState {

    Roaming,Fetching,Attacking,Eating,Tutorial
}

public class EnemyStates : MonoBehaviour
{
    public static CryptidState currentState;
  

    void Start()
    {
        currentState = CryptidState.Tutorial;
        eventManager.resetEnemyState += ReturnToDefault;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeCryState(CryptidState state)
    {
        currentState = state;
        switch (currentState)
        {
            case CryptidState.Roaming:
                break;
            case CryptidState.Fetching:
                
                break;
            case CryptidState.Attacking:
                //disable plyr turn and throw controll;

                break;
        }
    }
    void ReturnToDefault()
    {
        currentState = CryptidState.Roaming;
    }
}
