using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameUI gameUI;
    public int scorePlayer1, scorePlayer2;
    public System.Action onReset;
    public int maxScore = 7;

    public bool isGameStarted = false;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OnScoreZoneReached(int id)
    {
        onReset?.Invoke();

        if (id == 1)
            scorePlayer1++;

        if (id == 2)
            scorePlayer2++;

        gameUI.UpdateScores(scorePlayer1, scorePlayer2);
        gameUI.HighlightScore(id);
        CheckWin();
    }

    public void StartGame()
    {
        isGameStarted = true;  // Setting the game to started
        ResetScores();  // Reset scores when starting a new game
        gameUI.UpdateScores(scorePlayer1, scorePlayer2); 
        Time.timeScale = 1f;  // Resume the game
        onReset?.Invoke();  // Reset the ball and paddles
    }

    private void ResetScores()
    {
        scorePlayer1 = 0;
        scorePlayer2 = 0;
    }

    private void CheckWin()
    {
        int winnerId = scorePlayer1 == maxScore ? 1 : scorePlayer2 == maxScore ? 2 : 0;

        if (winnerId != 0)
        {
            // We have a winner!
            isGameStarted = false;  // Stop the game
            Time.timeScale = 0f;  // Pause the game
            gameUI.OnGameEnds(winnerId);
        }
    }
}
