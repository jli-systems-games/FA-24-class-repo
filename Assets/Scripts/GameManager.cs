using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{ 
    beginning, winter, rotting, summer, end,
}

public class GameManager : MonoBehaviour
{
    public GameState current;
    void Start()
    {
        current = GameState.beginning;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState state)
    {
        current = state;

        switch (current)
        {
            case GameState.summer:
                //allow 
                Debug.Log("turnning off fridge");
                break;
            case GameState.winter:
                break;
            case GameState.rotting:
                //if objects get reseted with parent and it is summer.
                //turn on fail scene.
                break;
            case GameState.end:
                break;
        }
    }
}
