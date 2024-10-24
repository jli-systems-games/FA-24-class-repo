using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public NewGunManager gameManager;
    public float damage;

    public Collider2D hitCollider;
    private bool canMove = true;

    public GameObject dieParticle;

    private Rigidbody rb;  // 用于物理运动的 Rigidbody2D

    private void Awake()
    {
        gameManager = FindObjectOfType<NewGunManager>();
        rb = GetComponent<Rigidbody>();  // 获取 Rigidbody2D 组件
    }

    private void Start()
    {
        float randomMultiplier2 = Random.Range(0.5f, 1.5f);
        speed *= randomMultiplier2;
    }

    private void Update()
    {
        if (canMove)
        {
            // 获取玩家位置
            Vector2 targetPosition = (Vector2)gameManager.playerTransform.transform.position + Vector2.up * 1.2f;

            // 计算移动方向
            Vector2 directionToTarget = (targetPosition - (Vector2)transform.position).normalized;

            // 更新 Rigidbody2D 的速度，使敌人沿着方向移动
            rb.velocity = directionToTarget * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;  // 如果不能移动，停止所有速度
        }
    }

    public void Die()
    {
        Instantiate(dieParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

    public IEnumerator StopMove()
    {
        canMove = false;
        yield return new WaitForSeconds(0.3f);
        canMove = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.DecreaseHealth(damage);
            Die();
        }
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        gameManager.DecreaseHealth(damage);
    //        StartCoroutine(Destroy());
    //    }
    //}
}
