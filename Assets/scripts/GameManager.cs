/*using System.Collections;
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

        if (state == GameState.Game1)
        {
            //controllerBonk.ChangeState(state);
            controllerBonk.StartMicroGame();
        }
    }

   // public void ChooseRandomGame()
    {
        GameState randomState = MicroGamePool[Random.Range(0, MicroGamePool.Count + 1)];
        ChangeState(randomState);
    }
   

}*/

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
   

    public static GameManager Instance;
    public static GameState state;
    public float transitionTime = 3f; 
    public float gameTime = 10f; 
    public int currentGameIndex = 0; 
    private List<string> gameScenes = new List<string> { "Game1", "Game2", "Game3" };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    void Start()
    {
        //DontDestroyOnLoad(gameObject);
        StartCoroutine(TransitionPhase());

    }

    public void ChangeState(GameState newState)
    {
        state = newState;
        Debug.Log("State changed to: " + state);

        if (state == GameState.Transition)
        {
            SceneManager.LoadScene("mian");
            StopAllCoroutines();
            StartCoroutine(TransitionPhase());
        }
    }

    IEnumerator TransitionPhase()
    {
        state = GameState.Transition;
        Debug.Log("Transitioning...");

        yield return new WaitForSeconds(transitionTime);

        LoadGameScene();
    }


    void LoadGameScene()
    {
        if (currentGameIndex < gameScenes.Count)
        {
            state = (GameState)(currentGameIndex + 1); 
            SceneManager.LoadScene(gameScenes[currentGameIndex]); 
            StartCoroutine(StartMicroGame(gameTime));
        }
        else
        {
            Debug.Log("All games completed!");
            
        }
    }

    IEnumerator StartMicroGame(float duration)
    {
        Debug.Log("Starting Microgame: " + state);
        yield return new WaitForSeconds(duration); 

        //currentGameIndex++;
        SceneManager.LoadScene("mian"); 
        StartCoroutine(TransitionPhase());
    }

   /* public void StartMicroGameSub()
    {
        Debug.Log("Microgame started!");
        StartCoroutine(EndMicroGame());
    }


    IEnumerator EndMicroGame()
    {
        yield return new WaitForSeconds(10f);
        ChangeState(GameState.Transition);
    }*/

}
