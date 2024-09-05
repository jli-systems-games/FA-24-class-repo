using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ball;

    public GameObject leftSlider;
    public GameObject rightSlider;

    public GameObject leftWall;
    public GameObject rightWall;

    //UI
    public GameObject csText;
    public GameObject hsText;

    private int currentScore;
    //private int highScore;

    private AudioSource audioSource;
    public AudioClip Boom;
    public AudioClip Huh;
    public AudioClip Bounce;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Hit()
    {
        audioSource.PlayOneShot(Boom);

        currentScore++;
        csText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
    }

    public void Miss()
    {
        audioSource.PlayOneShot(Huh);

        ResetPosition();
    }

    public void Bouncing()
    {
        audioSource.PlayOneShot(Bounce);

    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        leftSlider.GetComponent<PlayerController>().Reset();
        rightSlider.GetComponent<PlayerController>().Reset();

        currentScore = 0;
        csText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
    }
}
