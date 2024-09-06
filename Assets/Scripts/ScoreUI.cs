using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText; // Reference to the player score UI text
    public TextMeshProUGUI gameScoreText;   // Reference to the game score UI text

    private int playerScore = 0;
    private int gameScore = 0;

    public float negativeBoundaryX = -8.5f;
    public float positiveBoundaryX = 8.5f;

    // Start is called before the first frame update
    void Start()
    {
        ScoreUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        // Win condition for player
        if (tag == "Player" && (transform.position.x < negativeBoundaryX || transform.position.x > positiveBoundaryX))
        {
            Debug.Log("PlayerPoint");
            playerScore += 1;
            ScoreUpdate();
            //ResetGame();
            transform.position = Vector3.zero;
        }
    }

    // Lose condition for player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("GamePoint");
        gameScore += 1;
        ScoreUpdate();
        //ResetGame();
        transform.position = Vector3.zero;
    }

    void ScoreUpdate()
    {
        playerScoreText.text = playerScore.ToString();
        gameScoreText.text = gameScore.ToString();
    }

    //void ResetGame()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}
}
