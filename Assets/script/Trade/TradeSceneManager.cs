using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TradeSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI Player1ScoreText;
    public TextMeshProUGUI Player2ScoreText;
    public TextMeshProUGUI Player1ScoreChangeText; 
    public TextMeshProUGUI Player2ScoreChangeText; 

    [Header("Player 1 Settings")]
    public GameObject Player1Paddle;
    public KeyCode Player1LeftKey = KeyCode.A;
    public KeyCode Player1RightKey = KeyCode.D;
    public KeyCode Player1DownKey = KeyCode.S;
    public KeyCode Player1UpKey = KeyCode.W;
    public GameObject Player1SelectEffect;
    public GameObject Player1Ready;
    public GameObject Player1Confirm;

    [Header("Player 2 Settings")]
    public GameObject Player2Paddle;
    public KeyCode Player2LeftKey = KeyCode.LeftArrow;
    public KeyCode Player2RightKey = KeyCode.RightArrow;
    public KeyCode Player2DownKey = KeyCode.DownArrow;
    public KeyCode Player2UpKey = KeyCode.UpArrow;
    public GameObject Player2SelectEffect;
    public GameObject Player2Ready;
    public GameObject Player2Confirm;

    private float paddleAdjustment = 1f;
    private int minPaddleWidth = 1;
    private int maxPaddleWidth = 10;

    private bool isPlayer1Ready = false;
    private bool isPlayer2Ready = false;

    private bool countdownStarted = false;
    private float countdownTime = 3f; // 3 seconds countdown
    private float countdownTimer;

    private int initialPlayer1Score;
    private int initialPlayer2Score;

    private void Start()
    {
        Player1SelectEffect.SetActive(false);
        Player2SelectEffect.SetActive(false);

        Player1Confirm.SetActive(true);
        Player2Confirm.SetActive(true);

        Player1Ready.SetActive(false);
        Player2Ready.SetActive(false);

        initialPlayer1Score = Data.Player1Score;
        initialPlayer2Score = Data.Player2Score;

        Player1ScoreText.text = "$" + initialPlayer1Score.ToString();
        Player2ScoreText.text = "$" + initialPlayer2Score.ToString();

        Player1ScoreChangeText.gameObject.SetActive(false);
        Player2ScoreChangeText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Handle player input and adjust paddles and scores only if not ready
        if (!isPlayer1Ready)
        {
            HandlePlayerInput(Player1Paddle, ref Data.Player1Score, Player1LeftKey, Player1RightKey, Player1ScoreText, Player1ScoreChangeText, initialPlayer1Score);
        }

        if (!isPlayer2Ready)
        {
            HandlePlayerInput(Player2Paddle, ref Data.Player2Score, Player2LeftKey, Player2RightKey, Player2ScoreText, Player2ScoreChangeText, initialPlayer2Score);
        }

        // Player 1 ready or cancel ready state
        if (Input.GetKeyDown(Player1DownKey))
        {
            Player1SelectEffect.SetActive(true);
            Player1Ready.SetActive(true);

            Player1Confirm.SetActive(false);

            isPlayer1Ready = true;
            CheckBothPlayersReady();
        }
        else if (Input.GetKeyDown(Player1UpKey))
        {
            Player1SelectEffect.SetActive(false);
            Player1Ready.SetActive(false);

            Player1Confirm.SetActive(true);

            isPlayer1Ready = false;
            countdownStarted = false;
        }

        // Player 2 ready or cancel ready state
        if (Input.GetKeyDown(Player2DownKey))
        {
            Player2SelectEffect.SetActive(true);
            Player2Ready.SetActive(true);
            Player2Confirm.SetActive(false);

            isPlayer2Ready = true;
            CheckBothPlayersReady();
        }
        else if (Input.GetKeyDown(Player2UpKey))
        {
            Player2SelectEffect.SetActive(false);
            Player2Ready.SetActive(false);
            Player2Confirm.SetActive(true);

            isPlayer2Ready = false;
            countdownStarted = false;
        }

        // Handle countdown when both players are ready
        if (countdownStarted)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer <= 0)
            {
                SceneManager.LoadScene("Level2");
            }
        }
    }

    private void CheckBothPlayersReady()
    {
        if (isPlayer1Ready && isPlayer2Ready)
        {
            countdownStarted = true;
            countdownTimer = countdownTime;
        }
    }

    private void HandlePlayerInput(GameObject paddle, ref int playerScore, KeyCode leftKey, KeyCode rightKey, TextMeshProUGUI scoreText, TextMeshProUGUI scoreChangeText, int initialScore)
    {
        Vector3 currentScale = paddle.transform.localScale;

        if (Input.GetKeyDown(leftKey))
        {
            if (currentScale.y > minPaddleWidth)
            {
                paddle.transform.localScale = new Vector3(currentScale.x, currentScale.y - paddleAdjustment, currentScale.z);
                playerScore++;
                UpdateScoreText(scoreText, scoreChangeText, playerScore, initialScore);

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
                UpdateScoreText(scoreText, scoreChangeText, playerScore, initialScore);

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

    private void UpdateScoreText(TextMeshProUGUI scoreText, TextMeshProUGUI scoreChangeText, int currentScore, int initialScore)
    {
        scoreText.text = "$" + currentScore;
        int scoreDifference = currentScore - initialScore;

        if (scoreDifference != 0)
        {
            scoreChangeText.gameObject.SetActive(true);
            scoreChangeText.text = (scoreDifference > 0 ? "+" : "") + scoreDifference;
            scoreChangeText.color = scoreDifference > 0 ? Color.green : Color.red;
        }
        else
        {
            scoreChangeText.gameObject.SetActive(false);
        }
    }
}
