using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // For scene management

public class GameManager : MonoBehaviour
{
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

    private int Player1Score = 10; // Initial score for Player 1
    private int Player2Score = 10; // Initial score for Player 2
    private int BallPrice = 1; // Initial price for the ball

    void Start()
    {
        // Hide broke messages at the start
        Broke1Text.SetActive(false);
        Broke2Text.SetActive(false);
        
        // Initialize ball price display
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
    }

    public void Player2Scored()
    {
        // Deduct ball price from Player 1's score when Player 2 scores
        Player1Score -= BallPrice;
        Player1Text.GetComponent<TextMeshProUGUI>().text = "$" + Player1Score.ToString();
        ResetPosition();
    }

    public void Player1Scored()
    {
        // Deduct ball price from Player 2's score when Player 1 scores
        Player2Score -= BallPrice;
        Player2Text.GetComponent<TextMeshProUGUI>().text = "$" + Player2Score.ToString();
        ResetPosition();
    }

    public void BallPriced()
    {
        // Increase ball price and update the display
        BallPrice++;
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
    }

    private void ResetPosition()
    {
        // Reset ball and paddle positions
        ball.GetComponent<Ball>().Reset();
        Player1Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<Paddle>().Reset();

        // Update ball price display to current price
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();

        // Hide hands after reset
        LeftHand.SetActive(false);
        RightHand.SetActive(false);

        // Check if any player is broke and display appropriate message
       /* if (Player1Score <= 0)
        {
            Debug.Log("Player 1 is Broke");
            Broke1Text.SetActive(true);
        }
        if (Player2Score <= 0)
        {
            Debug.Log("Player 2 is Broke");
            Broke2Text.SetActive(true);
        }*/
    }

    public void EndLevel()
    {
        StartCoroutine(WaitAndSwitchScene());
    }

    private IEnumerator WaitAndSwitchScene()
    {
        // Wait for 3 seconds
        yield return new WaitForSeconds(3);

        // Load the next scene (Level2)
        SceneManager.LoadScene("Trade"); // Make sure the scene name matches the actual name
    }
}
