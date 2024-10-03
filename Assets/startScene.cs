using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startScene : MonoBehaviour
{
    public AudioSource audiosource;

    public GameObject halfMakeup;

    public GameObject nextCanvas;
    public GameObject thisCanvas;

    public Image image;
    public Sprite frame1;

    private IEnumerator delayEffect()
    {
        audiosource.Play();
        yield return new WaitForSeconds(2);
        halfMakeup.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        image.sprite = frame1;

    }

    void Start()
    {
        StartCoroutine(delayEffect());
    }

    void Update()
    {
        if (!audiosource.isPlaying)
        {
            nextCanvas.gameObject.SetActive(true);
            thisCanvas.gameObject.SetActive(false);
        }
    }
}
