using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    private Vector3 direction;
    public float speed;
    public float initialSpeed;
    public float boostedSpeed = 20f;

    [SerializeField]
    private int playerOneScore;
    [SerializeField]
    private int playerTwoScore;


    public Vector3 spawnPoint;

    public TextMeshProUGUI playerOneText;
    public TextMeshProUGUI playerTwoText;

    // Start is called before the first frame update
    void Start()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        initialSpeed = speed;
        this.direction = new Vector3(1f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += direction * speed;
        playerOneText.text = playerOneScore.ToString();
        playerTwoText.text = playerTwoScore.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        direction = Vector3.Reflect(direction, normal);

        if (collision.gameObject.name == "WestWall")
        {
            playerTwoScore++;
            ResetBall();
        }

        if (collision.gameObject.name == "EastWall")
        {
            playerOneScore++;
            ResetBall();
        }

    if (collision.gameObject.CompareTag("Cat"))
        {
            speed = boostedSpeed;
        }
    }

    private void ResetBall()
    {
        transform.position = spawnPoint;
        speed = initialSpeed;
        direction = new Vector3(1f, 0f, 1f);
    }
}
