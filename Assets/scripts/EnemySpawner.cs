using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // 敌人Prefab
    public int totalEnemies; // 生成的敌人总数
    public Vector3 spawnAreaMin; // 生成区域的最小点
    public Vector3 spawnAreaMax; // 生成区域的最大点
    public Animator doorAnimator; // 门的动画控制器
    public string doorOpenAnimationName = "DoorOpen"; // 开门动画名字

    public AudioClip kill1Sound;
    public AudioClip kill3Sound; // 消灭3个敌人时的音效
    public AudioClip kill10Sound; // 消灭10个敌人时的音效
    public AudioClip kill19Sound; // 消灭19个敌人时的音效
    public AudioClip doorOpenSound; // 所有敌人被消灭后的开门音效
    private AudioSource audioSource; // 音频播放器

    private int enemiesRemaining; // 当前场景剩余的敌人数
    private int enemiesKilled = 0; // 记录已消灭的敌人数

    void Start()
    {
        // 统计场景中已有的敌人数量
        enemiesRemaining = GameObject.FindGameObjectsWithTag("Enemy").Length;

        // 初始化敌人生成
        SpawnEnemies();

        // 初始化音频播放器
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < totalEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            // 生成敌人并将其旋转设为 90 度
            GameObject enemy = Instantiate(enemyPrefab, randomPosition, Quaternion.Euler(0, 90, 0));
            enemiesRemaining++; // 每生成一个敌人就增加计数
        }
    }

    // 减少敌人数
    public void EnemyCleared()
    {
        enemiesRemaining--;
        enemiesKilled++;

        PlayKillSound();

        // 当所有敌人被消灭时触发开门逻辑
        if (enemiesRemaining <= 0)
        {
            OpenDoor();
        }
    }

    void PlayKillSound()
    {
        // 根据消灭的敌人数播放对应的音效
        switch (enemiesKilled)
        {
            case 1:
                if (kill1Sound != null) audioSource.PlayOneShot(kill1Sound);
                break;
            case 3:
                if (kill3Sound != null) audioSource.PlayOneShot(kill3Sound);
                break;
            case 10:
                if (kill10Sound != null) audioSource.PlayOneShot(kill10Sound);
                break;
            case 19:
                if (kill19Sound != null) audioSource.PlayOneShot(kill19Sound);
                break;
        }
    }

    void OpenDoor()
    {
        if (doorAnimator != null)
        {
            // 播放开门动画
            doorAnimator.Play(doorOpenAnimationName);

            // 播放开门音效
            if (doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }

            Debug.Log("All enemies cleared. Door opened!");
        }
    }
}
