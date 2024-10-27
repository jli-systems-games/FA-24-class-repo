using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }  // Singleton instance

    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;
    public GameObject Player1Cost;
    public GameObject LeftHand;

    [Header("Player 2")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;
    public GameObject Player2Cost;
    public GameObject RightHand;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;
    public GameObject BallText;
    public GameObject Broke1Text;
    public GameObject Broke2Text;

    public static int Player1Score = 10;
    public static int Player2Score = 10;
    private int BallPrice = 1;

    private void Awake()
    {
        // Implement singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager instances
        }
    }

    void Start()
    {
        Broke1Text.SetActive(false);
        Broke2Text.SetActive(false);
        
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        Player1Text.GetComponent<TextMeshProUGUI>().text = "$" + Player1Score.ToString();
        Player2Text.GetComponent<TextMeshProUGUI>().text = "$" + Player2Score.ToString();
    }

    public void Player2Scored()
    {
        Player1Score -= BallPrice;
        UpdateScoreUI();
        ResetPosition();
    }

    public void Player1Scored()
    {
        Player2Score -= BallPrice;
        UpdateScoreUI();
        ResetPosition();
    }

    public void BallPriced()
    {
        BallPrice++;
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        Player1Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<Paddle>().Reset();

        BallPrice = 1;
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();

        LeftHand.SetActive(false);
        RightHand.SetActive(false);
    }

    public void EndLevel()
    {
        StartCoroutine(WaitAndSwitchScene());
    }

    private IEnumerator WaitAndSwitchScene()
    {
        yield return new WaitForSeconds(3);
        LoadTradeScene(); // Switch to LoadTradeScene method
    }

    // Method to load trade scene
    public void LoadTradeScene()
    {
        // Check if the current scene is already the trade scene to avoid reloading
        if (SceneManager.GetActiveScene().name != "Trade")
        {
            SceneManager.LoadScene("Trade");
        }
    }

    // Method to be called in the new scene to update scores
    public void ApplyScoresInTradeScene(TextMeshProUGUI player1Text, TextMeshProUGUI player2Text)
    {
        player1Text.text = "$" + Player1Score.ToString();
        player2Text.text = "$" + Player2Score.ToString();
    }
}
