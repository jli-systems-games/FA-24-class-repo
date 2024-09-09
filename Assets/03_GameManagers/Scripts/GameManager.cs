using System.Collections;
using System.Collections.Generic;
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
    public static int health, score;
    public float time;
    public static GameState state;

    //Managers
    public ControllerBonk controllerBonk;
    public ControllerJump controllerJump;

    public List<GameState> MicroGamePool = new List<GameState>();

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //GameObject, gameObject, and this.gameObject
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        //tell the other scripts that need to know
        //controllerBonk.ChangeState(state);
        //controllerJump.ChangeState(state);

        if(state == GameState.Game1)
        {
            //controllerBonk.ChangeState(state);
            controllerBonk.StartMicroGame();
        }
    }

    public void ChooseRandomGame()
    {
        GameState randomState = MicroGamePool[Random.Range(0,MicroGamePool.Count+1)];
        ChangeState(randomState);
    }

    
}
