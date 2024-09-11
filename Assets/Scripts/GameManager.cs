using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum GameState
{
    Idle,
    Game1,
    Game2,
    Game3,
    Transition
}

public class GameManager : MonoBehaviour
{
    public static int score;
    public static GameState state;

    //Managers
    public CherryGame CherryGame;
    public WhippedCream WhippedCream;

    public List<GameState> MicroGamePool = new List<GameState>();

    public TextMeshProUGUI outcome;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        outcome.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState) 
    {
        state = newState;

        //CherryDrop.ChangeState(state);
        //WhippedCream.ChangeState(state);

        if(state == GameState.Game1)
        {
            CherryGame.StartMicroGame(score);
        }
    }

    public void ChooseRandomGame()
    {
        GameState randomState = MicroGamePool[Random.Range(0,MicroGamePool.Count)];
        ChangeState(randomState);
    }

    public IEnumerator Result(bool didGreat, bool didOkay, bool failed)
    {
        if (didGreat)
        {
            outcome.text = "Great!";
        }

        else if (didOkay)
        {
            outcome.text = "Okay!";
        }

        else 
        {
            outcome.text = "You suck!";
        }

        outcome.enabled = true;

        yield return new WaitForSeconds(2);

        outcome.enabled = false;
    }
}
