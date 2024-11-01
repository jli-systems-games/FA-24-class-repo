using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level3Manager : MonoBehaviour
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

    private int BallPrice = 1;
    public Slider healthBar;

    void Start()
    {
        healthBar.maxValue = 1f;
        healthBar.value = 1f;

        Broke1Text.SetActive(false);
        Broke2Text.SetActive(false);

        Player1Paddle.transform.localScale = new Vector3(Player1Paddle.transform.localScale.x, Data.Player1Paddle, Player1Paddle.transform.localScale.z);
        Player2Paddle.transform.localScale = new Vector3(Player2Paddle.transform.localScale.x, Data.Player2Paddle, Player2Paddle.transform.localScale.z);

        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        Player1Text.GetComponent<TextMeshProUGUI>().text = "$" + Data.Player1Score.ToString();
        Player2Text.GetComponent<TextMeshProUGUI>().text = "$" + Data.Player2Score.ToString();
    }

    public void Player2Scored()
    {
        Data.Player1Score -= BallPrice;
        UpdateScoreUI();
        ResetPosition();
        ResetBallPrice();
    }

    public void Player1Scored()
    {
        Data.Player2Score -= BallPrice;
        UpdateScoreUI();
        ResetPosition();
        ResetBallPrice();
    }

    public void BallPriced()
    {
        BallPrice++;
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
    }

    public void ResetBallPrice()
    {
        BallPrice = 1;
        BallText.GetComponent<TextMeshProUGUI>().text = "Price: $" + BallPrice.ToString();
    }

    public void ResetPosition()
    {
        ball.GetComponent<Level3Ball>().Reset();
        Player1Paddle.GetComponent<Paddle>().Reset();
        Player2Paddle.GetComponent<Paddle>().Reset();
        LeftHand.SetActive(false);
        RightHand.SetActive(false);
    }

    public void ReduceHealth()
    {
        healthBar.value -= 0.33f;
        if (healthBar.value <= 0)
        {
            Debug.Log("Health depleted. Game Over.");
        }
    }

    public void EndLevel3()
    {
        StartCoroutine(WaitAndSwitchToEndScene());
    }

    private IEnumerator WaitAndSwitchToEndScene()
    {
        yield return new WaitForSeconds(3);
        LoadEndScene();
    }

    private void LoadEndScene()
    {
        // Check healthBar value to decide which end scene to load
        if (healthBar.value <= 0.1)
        {
            SceneManager.LoadScene("HE"); // Load HE scene if health is depleted
        }
        else
        {
            SceneManager.LoadScene("BE"); // Load BE scene otherwise
        }
    }
}
