using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalls : MonoBehaviour
{
    public Player player = Player.Player1;
    public float moveSpeed = 5f; // 移动速度
    public float minY = -4f;     // 最低 y 值
    public float maxY = 4f;      // 最高 y 值

    private AudioManager audioManager;
    private Animator animator;
    public enum Player
    {
        Player1,
        Player2
    }

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveAmount = 0f;

        // 玩家1的移动控制
        if (player == Player.Player1)
        {
            if (Input.GetKey(KeyCode.W))
            {
                moveAmount = 1f; // W 键向上移动
            }
            else if (Input.GetKey(KeyCode.S))
            {
                moveAmount = -1f; // S 键向下移动
            }
        }
        // 玩家2的移动控制
        else if (player == Player.Player2)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                moveAmount = 1f; // 上箭头键向上移动
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                moveAmount = -1f; // 下箭头键向下移动
            }
        }

        // 计算物体的新位置并限制在范围内
        float newY = Mathf.Clamp(transform.position.y + moveAmount * moveSpeed * Time.deltaTime, minY, maxY);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    // 检测碰撞
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象是否有 "Ball" 标签
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioManager.PlayRandomAudioBallHit();
            animator.SetTrigger("Start");
            // 获取 Ball 脚本
            Ball ballScript = collision.gameObject.GetComponent<Ball>();

            // 获取球的 Rigidbody2D
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();

            // 获取 PlayerWalls 的 Rigidbody2D
            Rigidbody2D playerRb = GetComponent<Rigidbody2D>();

            if (ballScript != null && ballRb != null && playerRb != null)
            {
                // 根据 PlayerWalls 的玩家状态来调用相应的 SetToPlayer 函数
                if (player == Player.Player1)
                {
                    ballScript.SetToPlayer1();
                }
                else if (player == Player.Player2)
                {
                    ballScript.SetToPlayer2();
                }
            }
        }
    }

}
