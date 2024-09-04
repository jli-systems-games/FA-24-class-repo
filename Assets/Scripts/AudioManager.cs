using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public AudioClip playerSet;
    public AudioClip gameStart;
    public AudioClip gameEnd;
    public AudioClip ballIn;
    public AudioClip powerReady;
    public AudioClip usePower;
    public AudioClip usePowerF;
    public AudioClip[] ballHitAudioClips;
    public AudioClip[] powerGetAudioClips;

    public void PlayButtonClick()
    {
        PlaySound(buttonClick, 1);
    }
    public void PlayButtonHover()
    {
        PlaySound(buttonHover, 0.3f);
    }
    public void PlayPlayerSet()
    {
        PlaySound(playerSet, 1);
    }
    public void PlayGameStart()
    {
        PlaySound(gameStart, 1);
    }
    public void PlayGameEnd()
    {
        PlaySound(gameEnd, 1);
    }
    public void PlayBallIn()
    {
        PlaySound(ballIn, 1);
    }    
    public void PlayPowerReady()
    {
        PlaySound(powerReady, 1);
    }
    public void PlayUsePower()
    {
        PlaySound(usePower, 1);
    }
    public void PlayCannotUsePower()
    {
        PlaySound(usePowerF, 1);
    }
    public void PlayRandomAudioBallHit()
    {
        int randomIndex = Random.Range(0, ballHitAudioClips.Length);
        AudioClip selectedClip = ballHitAudioClips[randomIndex];

       PlaySound(selectedClip, 1);
    }
    public void PlayRandomAudioPowerGet()
    {
        int randomIndex = Random.Range(0, powerGetAudioClips.Length);
        AudioClip selectedClip = powerGetAudioClips[randomIndex];

        PlaySound(selectedClip, 1);
    }

    private void PlaySound(AudioClip clip, float volume)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip is null.");
            return;
        }

        // 创建一个新的GameObject来附加AudioSource组件
        GameObject tempAudioObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        tempAudioSource.clip = clip;
        tempAudioSource.volume = volume;
        tempAudioSource.Play();

        // 销毁临时的GameObject，当音频播放结束后
        Destroy(tempAudioObject, clip.length);
    }
}
