using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeWall : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象是否有 "Ball" 标签
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioManager.PlayRandomAudioBallHit();
        }
    }
}
