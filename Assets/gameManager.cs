using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public int winScore = 7;

    public string playeronewin = "playeronewin";
    public string playertwowin = "playertwowin";

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

        if (Player1Score >= winScore)
        {
            SceneManager.LoadScene(playeronewin);
        } 
    }

    public void Player2Scored()
    {
        Player2Score++;
        player2text.GetComponent<TextMeshProUGUI>().text = Player2Score.ToString();
        ResetPosition();

        if (Player2Score >= winScore)
        {
            SceneManager.LoadScene(playertwowin);
        }
    }

    void Update()
    {
        if (Player1Score >= winScore)
        {
            Debug.Log("Player 1 Wins, loading scene: " + playeronewin);
            SceneManager.LoadScene(playeronewin);
        }

        if (Player2Score >= winScore)
        {
            Debug.Log("Player 2 Wins, loading scene: " + playertwowin);
            SceneManager.LoadScene(playertwowin);
        }
    }
}
//https://www.youtube.com/watch?v=YHSanceczXY&t=10s