using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallController : MonoBehaviour
{
    private Vector3 direction;
    public float speed;

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
            transform.position = spawnPoint;
        }

        if (collision.gameObject.name == "EastWall")
        {
            playerOneScore++;
            transform.position = spawnPoint;
        }
    }
}
