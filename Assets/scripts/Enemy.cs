using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool isHit = false; // ���ڷ�ֹ��δ�������

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && !isHit)
        {
            isHit = true; // ��ֹ�ظ�����
            StartCoroutine(DestroyEnemyAfterDelay(1f)); // �ȴ�2�������ٵ���
        }
    }

    // Э�̣��ӳ�2�����ٵ���
    IEnumerator DestroyEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject); // ���ٵ���
        FindObjectOfType<EnemySpawner>().EnemyCleared(); // ֪ͨ EnemySpawner�����ٵ�����
    }
}
