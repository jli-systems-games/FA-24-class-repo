using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Trade2SceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI Player1ScoreText;
    public TextMeshProUGUI Player2ScoreText;

    [Header("Player 1 Settings")]
    public GameObject Player1Paddle;
    public KeyCode Player1LeftKey = KeyCode.A;
    public KeyCode Player1RightKey = KeyCode.D;
    public KeyCode Player1DownKey = KeyCode.S;

    [Header("Player 2 Settings")]
    public GameObject Player2Paddle;
    public KeyCode Player2LeftKey = KeyCode.LeftArrow;
    public KeyCode Player2RightKey = KeyCode.RightArrow;
    public KeyCode Player2DownKey = KeyCode.DownArrow;

    private float paddleAdjustment = 1f;
    private int minPaddleWidth = 1;
    private int maxPaddleWidth = 10;

    // Flags to track each player's "down" key press
    private bool isPlayer1Ready = false;
    private bool isPlayer2Ready = false;

    private void Start()
    {
        // Initialize scores from Data script
        Player1ScoreText.text = "$" + Data.Player1Score.ToString();
        Player2ScoreText.text = "$" + Data.Player2Score.ToString();

        Player1Paddle.transform.localScale = new Vector3(Player1Paddle.transform.localScale.x, Data.Player1Paddle, Player1Paddle.transform.localScale.z);
        Player2Paddle.transform.localScale = new Vector3(Player2Paddle.transform.localScale.x, Data.Player2Paddle, Player2Paddle.transform.localScale.z);

    }

    private void Update()
    {
        // Handle player input and adjust paddles and scores
        HandlePlayerInput(Player1Paddle, ref Data.Player1Score, Player1LeftKey, Player1RightKey, Player1ScoreText);
        HandlePlayerInput(Player2Paddle, ref Data.Player2Score, Player2LeftKey, Player2RightKey, Player2ScoreText);

        // Check if both players are ready to move to the next level
        if (Input.GetKeyDown(Player1DownKey))
        {
            isPlayer1Ready = true;
        }

        if (Input.GetKeyDown(Player2DownKey))
        {
            isPlayer2Ready = true;
        }
       
        if (isPlayer1Ready && isPlayer2Ready)
        {
            Debug.Log("Both players are ready to proceed to the next level.");
            SceneManager.LoadScene("Level3");
            // Trigger the transition to the next level (to be implemented based on your project needs)

            // Reset flags after confirming readiness
            isPlayer1Ready = false;
            isPlayer2Ready = false;
        }
    }

    private void HandlePlayerInput(GameObject paddle, ref int playerScore, KeyCode leftKey, KeyCode rightKey, TextMeshProUGUI scoreText)
    {
        Vector3 currentScale = paddle.transform.localScale;

        
        if (Input.GetKeyDown(leftKey))
        {
            if (currentScale.y > minPaddleWidth)  
            {
                paddle.transform.localScale = new Vector3(currentScale.x, currentScale.y - paddleAdjustment, currentScale.z);
                playerScore++;
                UpdateScoreText(scoreText, playerScore);

                
                if (paddle == Player1Paddle)
                {
                    Data.Player1Paddle = (int)paddle.transform.localScale.y;
                }
                else if (paddle == Player2Paddle)
                {
                    Data.Player2Paddle = (int)paddle.transform.localScale.y;
                }
            }
        }

        
        if (Input.GetKeyDown(rightKey))
        {
            if (currentScale.y < maxPaddleWidth && playerScore > 0)  
            {
                paddle.transform.localScale = new Vector3(currentScale.x, currentScale.y + paddleAdjustment, currentScale.z);
                playerScore--;
                UpdateScoreText(scoreText, playerScore);

                
                if (paddle == Player1Paddle)
                {
                    Data.Player1Paddle = (int)paddle.transform.localScale.y;
                }
                else if (paddle == Player2Paddle)
                {
                    Data.Player2Paddle = (int)paddle.transform.localScale.y;
                }
            }
        }
    }



    private void UpdateScoreText(TextMeshProUGUI scoreText, int score)
    {
        scoreText.text = "$" + score;
    }
}
