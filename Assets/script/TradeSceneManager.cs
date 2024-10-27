using UnityEngine;
using TMPro;

public class TradeSceneManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI Player1ScoreText;
    public TextMeshProUGUI Player2ScoreText;

    [Header("Player 1 Settings")]
    public GameObject Player1Paddle;
    public KeyCode Player1LeftKey = KeyCode.A;
    public KeyCode Player1RightKey = KeyCode.D;

    [Header("Player 2 Settings")]
    public GameObject Player2Paddle;
    public KeyCode Player2LeftKey = KeyCode.LeftArrow;
    public KeyCode Player2RightKey = KeyCode.RightArrow;

    private float paddleAdjustment = 1f;
    private int minPaddleWidth = 1;
    private int maxPaddleWidth = 10;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ApplyScoresInTradeScene(Player1ScoreText, Player2ScoreText);
        }
        else
        {
            Debug.LogWarning("GameManager instance not found.");
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            HandlePlayerInput(Player1Paddle, ref GameManager.Player1Score, Player1LeftKey, Player1RightKey, Player1ScoreText);
            HandlePlayerInput(Player2Paddle, ref GameManager.Player2Score, Player2LeftKey, Player2RightKey, Player2ScoreText);
        }
    }

    private void HandlePlayerInput(GameObject paddle, ref int playerScore, KeyCode leftKey, KeyCode rightKey, TextMeshProUGUI scoreText)
    {
        Vector3 currentScale = paddle.transform.localScale;

        // Shrink paddle and increase score
        if (Input.GetKeyDown(leftKey))
        {
            if (currentScale.x > minPaddleWidth)
            {
                paddle.transform.localScale = new Vector3(currentScale.x - paddleAdjustment, currentScale.y, currentScale.z);
                playerScore++;
                UpdateScoreText(scoreText, playerScore);
            }
        }

        // Extend paddle and decrease score
        if (Input.GetKeyDown(rightKey))
        {
            if (currentScale.x < maxPaddleWidth && playerScore > 0)
            {
                paddle.transform.localScale = new Vector3(currentScale.x + paddleAdjustment, currentScale.y, currentScale.z);
                playerScore--;
                UpdateScoreText(scoreText, playerScore);
            }
        }
    }

    private void UpdateScoreText(TextMeshProUGUI scoreText, int score)
    {
        scoreText.text = "$" + score;
    }
}
