using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePlayerController : MonoBehaviour
{    
    private Rigidbody2D playerRigidbody;
    private float jumpForce = 8.5f;
    private bool canJump = true; 
    private AudioManager audioManager;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }
    private void Update()
    {       
            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                // 当资源调用都就绪且canJump为true时，通过按下空格为玩家输入一个向上的力(jumpForce)
                playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                canJump = false;
                audioManager.PlayBallJump();
            }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
}
