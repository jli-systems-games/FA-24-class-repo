using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 单例模式
    public List<AudioClip> typeSoundList = new List<AudioClip>(); // 存储多个音频剪辑的列表
    private AudioSource audioSource;

    void Awake()
    {
        // 确保只有一个 AudioManager 实例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 保持对象在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject); // 如果已有一个实例存在，销毁这个新实例
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 禁止音频在开始时自动播放
    }

    void Update()
    {
        // 检测按键是否被按下（M、K、L中的任意一个）
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.L))
        {
            PlayRandomTypeSound();
        }
    }

    public void PlayRandomTypeSound()
    {
        if (typeSoundList.Count > 0)
        {
            // 随机选择一个音频剪辑
            int randomIndex = Random.Range(0, typeSoundList.Count);
            audioSource.clip = typeSoundList[randomIndex];
            audioSource.Play(); // 播放随机选择的音频
        }
        else
        {
            Debug.LogError("typeSoundList 为空！");
        }
    }
}
