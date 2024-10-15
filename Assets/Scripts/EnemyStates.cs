using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CryptidState {

    Roaming,Fetching,Attacking,Eating
}

public class EnemyStates : MonoBehaviour
{
    public CryptidState currentState;
  

    void Start()
    {
        currentState = CryptidState.Roaming;
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
                Debug.Log("it is fetching");
                break;
            case CryptidState.Attacking: 
                break;
        }
    }
    void ReturnToDefault()
    {
        currentState = CryptidState.Roaming;
    }
}
