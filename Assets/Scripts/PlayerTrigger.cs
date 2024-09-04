using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Player player;
    public enum Player
    { 
        Player1,
        Player2, 
    }
    private GameStatusManager statusManager;
    private CameraShake cameraShake;
    private AudioManager audioManager;
    private void Start()
    {
        statusManager = FindObjectOfType<GameStatusManager>();
        cameraShake = FindObjectOfType<CameraShake>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (player == Player.Player1)
            { 
                statusManager.DecreaseP1Health();            
            }
            else if (player == Player.Player2) 
            {
                statusManager.DecreaseP2Health();
            }

            audioManager.PlayBallIn();
            cameraShake.TriggerShakeBallIn();
            Destroy(collision.gameObject); 
        }
    }
}
