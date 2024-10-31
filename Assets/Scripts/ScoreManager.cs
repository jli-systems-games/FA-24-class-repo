using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;      // Singleton instance for easy access
    public TextMeshProUGUI scoreText;         // TMPro Text element to display the score

    private int score = 0;

    private void Awake()
    {
        // Set up the singleton instance
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

    private void Start()
    {
        UpdateScoreText(); // Initialize the score display
    }

    public void AddScore(int points)
    {
        score += points;    // Increment score
        UpdateScoreText();  // Update the score UI
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Precision Points: " + score.ToString();
    }
}
