using UnityEngine;

public class PlayAudioOnTrigger : MonoBehaviour
{
    public AudioClip soundToPlay; // 要播放的音频
    private AudioSource audioSource;
    private bool hasPlayed = false; // 确保音频只播放一次

    private void Start()
    {
        // 添加 AudioSource 组件并初始化
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 不在开始时播放
        audioSource.clip = soundToPlay;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查进入触发区域的是否是玩家，并且音频尚未播放过
        if (other.CompareTag("Player") && !hasPlayed)
        {
            hasPlayed = true; // 确保只播放一次
            audioSource.Play();
        }
    }
}
