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

    public List<GameObject> thrownMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        match.SetActive(false);
        state = GameState.moveable;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.M)) && state == GameState.moveable)
        {
            match.SetActive(true);
            ChangeState(GameState.matchOut);
        }
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        if(state != GameState.matchOut && state != GameState.burning)
        {
            match.SetActive(false );
        }
        
    }

    public void CheckMatchLimit()
    {
        if(thrownMatches.Count > 30)
        {
            Destroy(thrownMatches[0]);
            thrownMatches.RemoveAt(0);
            Debug.Log("destroyed first match");
        }
    }
}
