using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip gameStart;
    public AudioClip defended;
    public AudioClip defendReady;
    public AudioClip handSwing;
    public AudioClip buttonPress;
    public AudioClip ballJump;
    public AudioClip[] ballScore;
    public AudioClip phoneVib;

    public void PlayGameStart()
    {
        PlayClip(gameStart, 1);
    }
    public void PlayDefended()
    {
        PlayClip(defended, 1);
    }
    public void PlayDefendReady()
    {
        PlayClip(defendReady, 1);
    }
    public void PlayHandSwing()
    {
        PlayClip(handSwing, 1);
    }
    public void PlayButtonPress()
    {
        PlayClip(buttonPress, 1);
    }
    public void PlayBallJump()
    {
        PlayClip(ballJump, 0.5f);
    }
    public void PlayBallScore()
    {
        int randomIndex = Random.Range(0, ballScore.Length);
        AudioClip selectedClip = ballScore[randomIndex];

        PlayClip(selectedClip, 1);
    }
    public void PlayPhoneVibrate()
    {
        PlayClip(phoneVib, 1);
    }





    private void PlayClip(AudioClip clip, float volume)
    {
        GameObject tempAudioObject = new GameObject("TempAudio");
        AudioSource tempAudioSource = tempAudioObject.AddComponent<AudioSource>();

        tempAudioSource.clip = clip;
        tempAudioSource.volume = volume;
        tempAudioSource.Play();

        // 销毁临时的GameObject，当音频播放结束后
        Destroy(tempAudioObject, clip.length);
    }
}
