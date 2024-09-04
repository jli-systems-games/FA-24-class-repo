using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject Player1Paddle;
    public GameObject Player1Goal;

    [Header("Player 2")]
    public GameObject Player2Paddle;
    public GameObject Player2Goal;

    [Header("Score UI")]
    public GameObject Player1Text;
    public GameObject Player2Text;
    public GameObject BallText;
    public GameObject Broke1Text;
    public GameObject Broke2Text;

    private int Player1Score=10;
    private int Player2Score=10;
    private int BallPrice;

    void Start()
    {

    }

    public void Player1Scored()
    {
        Player1Score = Player1Score - BallPrice;
        Player1Text.GetComponent<TextMeshProUGUI>().text = "$"+ Player1Score.ToString();
        ResetPosition();
    }

    public void Player1IsBroke()
    {
        if (Player1Score <= 0)
        {
            Debug.Log("Player1 isBroke");
            Broke1Text.SetActive(true);
        }
        else
        {
            Broke1Text.SetActive(false);
        }
    }

    public void Player2Scored()
    {
        Player2Score = Player2Score - BallPrice;
        Player2Text.GetComponent<TextMeshProUGUI>().text = "$" + Player2Score.ToString();
        ResetPosition();
    }

    public void BallPriced()
    {
        Debug.Log(BallPrice);
        BallPrice++;
        BallText.GetComponent<TextMeshProUGUI>().text = "Price:$"+BallPrice.ToString();
  
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        Player1Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<Paddle>().Reset();
        BallText.GetComponent<TextMeshProUGUI>().text = "Price:0";
        BallPrice = 0;
    }


}
