using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Animator cameraAnim;
    public Animator phoneAnim;
    public Canvas menuCanvas;
    public Canvas endCanvas;

    private InputManager inputManager;
    private EnemySpawner enemySpawner;
    private AudioManager audioManager;
    private MiniGameLevelController miniGameLevelController;
    private bool gameStarted = false;

    public GameStatus gameStatus = GameStatus.Menu;
    public enum GameStatus
    { 
        Menu,
        Intro,
        GameStarted,
        GameEnded,    
    }

    void Start()
    {
        menuCanvas.enabled = true;
        endCanvas.enabled = false;

        inputManager = GetComponent<InputManager>();
        enemySpawner = GetComponent<EnemySpawner>();
        audioManager = GetComponent<AudioManager>();
        miniGameLevelController = GetComponent<MiniGameLevelController>();
    }

    
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !gameStarted)
        {
            if (gameStatus == GameStatus.Menu)
            {
                ShowIntro();
                gameStatus = GameStatus.Intro;               
            }
            else if (gameStatus == GameStatus.Intro)
            {
                gameStarted = true;
                phoneAnim.SetTrigger("Wait");
                Invoke("StartGame", 1f);                
                gameStatus = GameStatus.GameStarted;
            }
        }
        if (Input.GetKeyUp(KeyCode.R) && !gameStarted)
        {
            if (gameStatus == GameStatus.GameEnded)
            {
                string currentSceneName = SceneManager.GetActiveScene().name;

                SceneManager.LoadScene(currentSceneName);
            }
        }
    }

    private void StartGame()
    {        
        inputManager.StartGame();
        enemySpawner.GameStart();
        audioManager.PlayGameStart();
        miniGameLevelController.StartGame();
    }
    public void EndGame()
    {
        enemySpawner.EndGame();
        gameStatus = GameStatus.GameEnded;
        gameStarted = false;
        endCanvas.enabled = true;
        inputManager.EndGame();
    }
    void ShowIntro()
    {
        Invoke("PhoneAnim",1f);
        cameraAnim.SetTrigger("Start");
        menuCanvas.enabled = false;
        miniGameLevelController.ShowIntro();
    }

    void PhoneAnim()
    {
        phoneAnim.SetTrigger("Trigger");
        audioManager.PlayPhoneVibrate();
    }
}
