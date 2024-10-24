using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isHit = false; // 用于防止多次触发销毁

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isHit)
        {
            isHit = true; // 防止重复触发
            StartCoroutine(DestroyEnemyAfterDelay(1f)); // 等待2秒再销毁敌人
        }
    }

    // 协程：延迟2秒销毁敌人
    IEnumerator DestroyEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // 销毁敌人
        FindObjectOfType<EnemySpawner>().EnemyCleared(); // 通知 EnemySpawner，减少敌人数
    }
}
