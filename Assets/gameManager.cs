using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player 1")]
    public GameObject player1Paddle;
    public GameObject player1goal;

    [Header("Player 2")]
    public GameObject player2Paddle;
    public GameObject player2goal;

    [Header("Score UI")]
    public GameObject player1text;
    public GameObject player2text;

    private int Player1Score;
    private int Player2Score;

    void ResetPosition()
    {
    ball.transform.position = Vector3.zero;
        ball.GetComponent<ball>().Launch();

    player1Paddle.transform.position = new Vector3(-8, 0.78f, 0); 
    player2Paddle.transform.position = new Vector3(8, 0.78f, 0);  
    }

    public void Player1Scored()
    {
        Player1Score++;
        player1text.GetComponent<TextMeshProUGUI>().text = Player1Score.ToString();
        ResetPosition();
    }

    public void Player2Scored()
    {
        Player2Score++;
        player2text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        ResetPosition();
    }
}
