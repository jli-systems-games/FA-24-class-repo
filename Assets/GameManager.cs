using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    Game1,  //customer
    Game2,  //match
    Game3,  //clean
    Transition
}
public class GameManager : MonoBehaviour
{
    public static int health = 3, score = 0;
    public float time;
    public static GameState state;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    //managers
    public ControllerMatch controllerMatch;
    public ControllerClean controllerClean;
    public ControllerCustomer controllerCustomer;

    public List<GameState> MicroGamePool = new List<GameState>();

    // void Start()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        healthText.text = "Health: " + health;
        scoreText.text = "Tips: " + score;
    }

    public void PlayGame()
    {
        ChooseRandomGame();
    }

    public void ChangeState(GameState newState)
    {
        state = newState;

        if(state == GameState.Game1)
        {
            controllerCustomer.StartMicroGame(health);
            SceneManager.LoadScene("CustomerScene");

        }
        
        else if(state == GameState.Game2)
        {
            controllerMatch.StartMicroGame(health);
            SceneManager.LoadScene("MatchScene");

        }

        else if(state == GameState.Game3)
        {
            controllerClean.StartMicroGame(health);
            SceneManager.LoadScene("CleanScene");

        }
        else
        {
            Debug.Log("No game state");
        }
    }

    public void ChooseRandomGame()
    {
        if (MicroGamePool.Count > 0)
        {
            int randomIndex = Random.Range(0, MicroGamePool.Count);
            GameState randomState = MicroGamePool[randomIndex];
            ChangeState(randomState);
        }
        else
        {
            Debug.Log("no state");
        }
}
    
}
