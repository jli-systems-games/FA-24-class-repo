using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public int maxAudioSources = 10; // 最大音频播放上限
    private Queue<GameObject> activeAudioObjects = new Queue<GameObject>(); // 使用队列来存储活跃的音频对象

    public AudioClip emptyMeg;
    public AudioClip pistolShot;
    public AudioClip smgShot, smgShotSilencer;
    public AudioClip smgReloadUnplug, smgReloadPlug;
    public AudioClip pistolReload;
    public AudioClip smgReload;
    public AudioClip switchLight;
    public AudioClip switchWeapon;

    public AudioClip bulletPickup;
    public AudioClip medicPickup;
    public AudioClip waterPickup;
    public AudioClip batteryPickup;

    public AudioClip[] buffPickUpClips;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayPistolShot()
    {
        PlaySound(pistolShot, 1f);
    }
    public void PlaySmgShot()
    {
        PlaySound(smgShot, 0.2f);
    }
    public void PlaySmgShotSilencer()
    {
        PlaySound(smgShotSilencer, 0.8f);
    }
    public void PlayPistolReload()
    {
        PlaySound(pistolReload, 1);
    }
    public void PlaySmgReload()
    {
        PlaySound(smgReload, 1);
    }
    public void PlaySmgReloadUnplug()
    {
        PlaySound(smgReloadUnplug, 1);
    }
    public void PlaySmgReloadPlug()
    {
        PlaySound(smgReloadPlug, 1);
    }
    public void PlayEmptyMeg()
    {
        PlaySound(emptyMeg, 1);
    }
    public void PlaySwitchLight()
    {
        PlaySound(switchLight, 1);
    }
    public void PlaySwitchWeapon()
    {
        PlaySound(switchWeapon, 1);
    }

    public void PlayBulletPickup()
    {
        PlaySound(bulletPickup, 1);
    }
    public void PlayMedicPickup()
    {
        PlaySound(medicPickup, 1);
    }
    public void PlayWaterPickup()
    {
        PlaySound(waterPickup, 1);
    }
    public void PlayBatteryPickup()
    {
        PlaySound(batteryPickup, 1);
    }

    public void PlayRandomBuffPickUp()
    {

        int randomIndex = Random.Range(0, buffPickUpClips.Length);
        AudioClip selectedClip = buffPickUpClips[randomIndex];

        PlaySound(selectedClip, 1);
    }
    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null.");
            return;
        }

        // 如果当前队列已满，移除最早的音频对象
        if (activeAudioObjects.Count >= maxAudioSources)
        {
            GameObject oldestAudioObject = activeAudioObjects.Dequeue(); // 移除队列中最早的音频对象
            Destroy(oldestAudioObject); // 销毁最早的音频对象
        }

        // 创建一个新的 GameObject 来附加 AudioSource 组件
        GameObject tempAudioObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        // 设置 AudioSource 的属性
        tempAudioSource.clip = clip;
        tempAudioSource.volume = volume;
        tempAudioSource.Play();

        // 将新创建的音频对象加入队列中
        activeAudioObjects.Enqueue(tempAudioObject);

        // 启动协程来销毁音频对象并从队列中移除
        StartCoroutine(DestroyAudioObjectWhenFinished(tempAudioObject, clip.length));
    }
    private IEnumerator DestroyAudioObjectWhenFinished(GameObject audioObject, float delay)
    {
        // 等待音频播放结束
        yield return new WaitForSeconds(delay);

        // 从队列中移除已经播放完成的音频对象（但由于之前队列满时我们已经移除，所以不需要再处理队列）
        if (activeAudioObjects.Contains(audioObject))
        {
            activeAudioObjects = new Queue<GameObject>(activeAudioObjects); // 重建队列，去除已销毁对象
        }

        // 销毁临时的 GameObject
        Destroy(audioObject);
    }
}
