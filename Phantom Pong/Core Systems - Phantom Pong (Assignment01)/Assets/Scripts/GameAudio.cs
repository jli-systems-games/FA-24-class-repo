using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudio : MonoBehaviour
{
    public AudioSource asSounds;

    public AudioClip paddleSound;
    public AudioClip scoreSound;
    public AudioClip winSound;
    public AudioClip buttonSound;

    public void PlayPaddleSound()
    {
        asSounds.PlayOneShot(paddleSound);
    }

    public void PlayScoreSound()
    {
        asSounds.PlayOneShot(scoreSound);
    }

    public void PlayWinSound()
    {
        asSounds.PlayOneShot(winSound);
    }

    public void PlayButtonSound()
    {
        asSounds.PlayOneShot(buttonSound);
    }
}
