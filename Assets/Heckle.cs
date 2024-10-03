using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heckle : MonoBehaviour
{
    public Image tomatoImage;
    private Animator animator;
    public string animationName = "tomato";
    private bool isAnimating = false;

    public AudioSource audioSource;
    public AudioClip booAudio;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        tomatoImage.canvasRenderer.SetAlpha(0);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D) && !isAnimating)
        {
            tomatoImage.canvasRenderer.SetAlpha(1);
            PlayAnimation();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            PlaySound();
        }
    }

    void PlayAnimation()
    {
        isAnimating = true;
        animator.Play(animationName, -1, 0f);
        StartCoroutine(HideAnimation());
    }

    // Coroutine to wait for the animation to finish
    IEnumerator HideAnimation()
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(animationLength);

        tomatoImage.canvasRenderer.SetAlpha(0);

        isAnimating = false;
    }

    void PlaySound()
    {
        if (audioSource != null && booAudio != null)
        {
            // Play the sound effect
            audioSource.PlayOneShot(booAudio);
        }
        else
        {
            Debug.LogError("no audio!");
        }
    }
}
