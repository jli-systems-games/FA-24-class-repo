using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState
{
    moveable,
    matchOut,
    burning,
    dead
}
public class GameManager : MonoBehaviour
{
    public static GameState state;

    public GameObject match;

    // Start is called before the first frame update
    void Start()
    {
        match.SetActive(false);
        state = GameState.moveable;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && state == GameState.moveable)
        {
            match.SetActive(true);
            ChangeState(GameState.matchOut);
        }
    }

    public void ChangeState(GameState newState)
    {
        state = newState;
    }
}
