using UnityEngine;
using System.Collections;
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

    public ControllerBonk controllerBonk;
    public ControllerJump controllerJump;

    public List<GameState> MicroGamePool = new List<GameState>();

    void Start()
    {
        DontDestroyOnLoad(gameObject);
       
    }

   
   public void ChangeState(GameState newState)
    {
        state = newState;

        //tell the other scripts that need to know
        /*controllerBonk.ChangeState(state);
        controllerJump.ChangeState(state);*/

        if (state == GameState.Game1)
        {
            ControllerBonk.StartMicroGame();
        }

    }

    public void ChooseRandomGame()
    {
        GameState randomState = McrioGamePool[Random.Range(0, McrioGamePool.Count+1)];//GameState.Idle;
        
        ChangeState(randomState);
    }
}
