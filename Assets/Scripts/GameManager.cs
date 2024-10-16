using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    begin, Hunger, Fetch, Irritable, Game, End
}

public class GameManager : MonoBehaviour
{
    [SerializeField] EnemyStates enemyStates;
    [SerializeField] PlayerMovement plyr;
    public GameState currentState;

    void Start()
    {
        currentState = GameState.begin;
        StartCoroutine(_begin());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeGState(GameState state)
    {
        currentState = state;
        switch (currentState)
        {
            case GameState.Hunger:
                Debug.Log("go feeding the beast");
                plyr.tuH = false;
                break;
            case GameState.Fetch:
                Debug.Log("Entertain it");
                plyr.tuF = false;
                break;
            case GameState.Irritable:
                //disable plyr turn and throw controll;
                Debug.Log("fight");
                plyr.tuR = false;
                break;
        }
    }
    IEnumerator _begin()
    {
        Debug.Log("shatter glass");
        yield return new WaitForSeconds(4f);

        ChangeGState(GameState.Hunger);
    }
}
