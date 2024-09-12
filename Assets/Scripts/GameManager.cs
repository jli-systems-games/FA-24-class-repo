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
    public EggGame EggGame;

    public List<GameState> MicroGamePool = new List<GameState>();

    public GameObject transitionObj;

    //UI elements
    public TextMeshProUGUI outcome;
    public TextMeshProUGUI scoreOnScreen;
    public GameObject timer;
    public GameObject DragIndic;
    public GameObject qIndic;
    public GameObject spaceIndic;

    //Audio

    public AudioSource AudioSource;
    public AudioClip wompWomp;
    public AudioClip yippee;
    public AudioClip ding;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        outcome.enabled = false;
        timer.SetActive(false);
        DragIndic.SetActive(false);
        qIndic.SetActive(false);
        spaceIndic.SetActive(false);
        scoreOnScreen.enabled = false;

        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState) 
    {
        state = newState;
        Debug.Log(state);

        //CherryDrop.ChangeState(state);
        //WhippedCream.ChangeState(state);

        if(state == GameState.Game1)
        {
            timer.SetActive(true);
            DragIndic.SetActive(true);
            CherryGame.gameObject.SetActive(true);
            CherryGame.StartMicroGame(score);
        }

        if (state == GameState.Game2) 
        {
            spaceIndic.SetActive(true);
            timer.SetActive(true);
            WhippedCream.gameObject.SetActive(true);
            WhippedCream.StartMicroGame(score);
        }

        if(state == GameState.Game3)
        {
            qIndic.SetActive(true);
            EggGame.gameObject.SetActive(true);
            EggGame.StartMicroGame(score);
        }

        if(state == GameState.Transition)
        {
            StartCoroutine(gameTransition());
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
            score += 2;

            AudioSource.clip = yippee;
        }

        else if (didOkay)
        {
            outcome.text = "Okay!";
            score++;

            AudioSource.clip = ding;
        }

        else 
        {
            outcome.text = "You suck!";

            AudioSource.clip = wompWomp;
        }

        scoreOnScreen.text = "Score: " + score;
        AudioSource.Play();

        outcome.enabled = true;
        timer.SetActive(false);
        DragIndic.SetActive(false);
        qIndic.SetActive(false);
        spaceIndic.SetActive(false);

        yield return new WaitForSeconds(1);

        Debug.Log("starting new game");
        outcome.enabled = false;
        CherryGame.gameObject.SetActive(false);
        WhippedCream.gameObject.SetActive(false);
        EggGame.gameObject.SetActive(false);

        ChangeState(GameState.Transition);
    }

    public IEnumerator gameTransition()
    {
        transitionObj.SetActive(true);

        transitionObj.GetComponent<Animator>().Play("transition");

        yield return new WaitForSeconds(5);

        ChooseRandomGame();
        transitionObj.SetActive(false);
    }

    public void StartGame()
    {
        scoreOnScreen.enabled = true;
        transitionObj.SetActive(false);
    }
}
