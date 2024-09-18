using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Idle, Squeeze, Shake, Smash
}
public class GameManager : MonoBehaviour
{
    public GameStates State;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeState(GameStates state)
    {
        State = state;
    }
}
