using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI playerScoreText; // Reference to the player score UI text
    public TextMeshProUGUI gameScoreText;   // Reference to the game score UI text

    private int playerScore = 0;
    private int gameScore = 0;

    public float negativeBoundaryX = -8.5f;
    public float positiveBoundaryX = 8.5f;

    public BrickMove brickR;
    public BrickMove brickL;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        ScoreUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        // Win condition
        if (BallBoundary.IsOutOfBounds(player, negativeBoundaryX, positiveBoundaryX))
        {
            PlayerPoint();
        }
    }

    public void PlayerPoint()
    {
        Debug.Log("PlayerPoint");
        playerScore++;
        ScoreUpdate();
        TeleportPlayer();

        // Increase difficulty
        brickR.IncreaseSpeed();
        brickL.IncreaseSpeed();
        brickR.IncreaseSize();
        brickL.IncreaseSize();
    }

    public void GamePoint()
    {
        Debug.Log("GamePoint");
        gameScore++;
        ScoreUpdate();
        TeleportPlayer();
    }

    void ScoreUpdate()
    {
        playerScoreText.text = playerScore.ToString();
        gameScoreText.text = gameScore.ToString();
    }

    void TeleportPlayer()
    {
        player.transform.position = Vector3.zero;

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
