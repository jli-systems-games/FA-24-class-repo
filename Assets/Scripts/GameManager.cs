using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer;
    public float maxTimer;
    public int lives;
    public int score;

    private int currentGameIndex;

    public GameObject uiCanvas;
    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public float timerDecrease = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        maxTimer = 10f;
        lives = 3;
        score = 0;
        currentGameIndex = 1;

        UpdateUI();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            Debug.Log("DeltaTime: " + Time.deltaTime); // Check the value of Time.deltaTime
            timer -= Time.deltaTime; // Timer counting down
            UpdateUI();
        }
        else
        {
            OnGameComplete(false);
        }
    }

    // Load the current mini-game scene based on currentGameIndex
    void StartGame()
    {
        SceneManager.LoadScene("MiniGame" + currentGameIndex);
        ResetTimer();
        UpdateUI();
    }

    void GameOver()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene("EndScene");
            Destroy(gameObject);
        }
        else
        {
            MoveToNextGame();
        }
        UpdateUI();
    }

    public void OnGameComplete(bool success)
    {
        if (success)
        {
            score++;
        }
        else
        {
            GameOver();
            return;
        }

        MoveToNextGame();
        UpdateUI();
    }

    void MoveToNextGame()
    {
        currentGameIndex++;
        if (currentGameIndex > 3)
        {
            currentGameIndex = 1;
        }

        maxTimer = Mathf.Max(3f, maxTimer - timerDecrease); //Decrease timer at game switch
        StartGame();
    }

    void ResetTimer()
    {
        timer = maxTimer;
    }

    void UpdateUI()
    {
        timerText.text = "Timer: " + timer.ToString("F1");
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
    }
}
