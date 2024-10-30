using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallCollision : MonoBehaviour
{
    private PinballController pinballController;

    public int score = 0;
    public TextMeshProUGUI scoreText;
    public GameObject resetUI;
    public TextMeshProUGUI resetText;

    // Start is called before the first frame update
    void Start()
    {
        pinballController = FindObjectOfType<PinballController>();

        if (pinballController == null)
        {
            Debug.LogError("pinball manager not found");
        }

        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("bottom"))
    //    {
    //        resetUI.SetActive(true);
    //        resetText.text = "Your final score was " + score.ToString();
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("point5"))
        {
            AddScore(5);
        }
        else if (collision.gameObject.CompareTag("point10"))
        {
            AddScore(10);
        }
        else if (collision.gameObject.CompareTag("point25"))
        {
            AddScore(25);
        }
        else if (collision.gameObject.CompareTag("bottom"))
        {
            resetUI.SetActive(true);
            resetText.text = "Your final score was " + score.ToString();
        }
    }

    void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
