using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Goal : MonoBehaviour
{
    public bool isPlayer1Goal;

    public AudioSource audioSource;
    public AudioClip soldSound;


    private void Start()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            PlaySoldSound();

            if (!isPlayer1Goal)
            {
                Debug.Log("Player 1 Scored...");
                GameObject.Find("Level2Manager").GetComponent<Level2Manager>().Player1Scored();
               
            }
            else
            {
                Debug.Log("Player 2 Scored...");
                GameObject.Find("Level2Manager").GetComponent<Level2Manager>().Player2Scored();
              
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