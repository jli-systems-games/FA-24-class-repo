using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timer;
    public int lives;
    public int score;

    public TMP_Text timerText;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public float timerDecrease = 1f;
    private int gamesPlayed = 0;

    private int currentGameIndex;

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
        timer = 10f;
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
            timer -= Time.deltaTime;
            UpdateUI(); // Update UI every frame
        }
        else
        {
            GameOver();
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("MiniGame" + currentGameIndex);
        gamesPlayed++;

        // Every 3 games, decrease the timer
        if (gamesPlayed >= 3)
        {
            timer = Mathf.Max(5f, timer - timerDecrease); // Ensure timer doesn't go below 5 seconds
            gamesPlayed = 0; // Reset the counter
        }

        UpdateUI();
    }

    void GameOver()
    {
        lives--;
        if (lives <= 0)
        {
            Debug.Log("Game Over!");
            // Implement Game Over logic here
        }
        else
        {
            currentGameIndex = (currentGameIndex + 1) % 3;
            StartGame();
        }
        UpdateUI();
    }

    public void OnGameComplete(bool success)
    {
        if (success)
        {
            score++;
            currentGameIndex = (currentGameIndex + 1) % 3;
            StartGame();
        }
        else
        {
            GameOver();
        }
        UpdateUI();
    }

    // Update the UI text elements
    void UpdateUI()
    {
        timerText.text = "Timer: " + Mathf.Ceil(timer);
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }
}