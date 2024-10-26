 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Goal;

    public AudioSource audioSource;
    public AudioClip soldSound;


    private void Start()
    {
       // imageManager = GameObject.Find("ImageManager").GetComponent<ImageManager>(); // Ensure this GameObject is named "ImageManager"
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            PlaySoldSound();

            if (!isPlayer1Goal)
            {
                Debug.Log("Player 1 Scored...");
                GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scored();
                //imageManager.OnScore(); // Call OnScore method in ImageManager
            }
            else
            {
                Debug.Log("Player 2 Scored...");
                GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scored();
               //imageManager.OnScore(); // Call OnScore method in ImageManager
            }
        }
    }

    private void PlaySoldSound()
    {
        if (audioSource != null && soldSound != null)
        {
            audioSource.PlayOneShot(soldSound);
        }
    }
}