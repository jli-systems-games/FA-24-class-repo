using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeImg2 : MonoBehaviour
{
    public Image image;
    public AudioSource audiosource;

    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;

    public checkGB eyeShadowStat;

    public Image kasumi;

    public Sprite frame4;
    public Sprite frame5;
    public Sprite frame6;
    public Sprite frame7;

    public GameObject nextCanvas;
    public GameObject currentCanvas;

    private IEnumerator oliviatalk()
    {
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame3;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame2;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame3;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame2;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame3;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame2;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame3;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame2;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame3;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame2;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame3;
        yield return new WaitForSeconds(0.3f);
        image.sprite = frame2;
        StartCoroutine(kasumitalk());
    }

    private IEnumerator kasumitalk()
    {
        kasumi.sprite = frame5;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame4;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame5;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame4;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame5;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame4;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame5;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame4;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame5;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame5;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame4;
        yield return new WaitForSeconds(0.3f);
        kasumi.sprite = frame5;
        nextCanvas.gameObject.SetActive(true);
        currentCanvas.gameObject.SetActive(false);
        
    }

    void Start()
    {
        audiosource.Play();
        if (eyeShadowStat.eyeshadowGood == false)
        {
            image.sprite = frame2;
            StartCoroutine(oliviatalk());
        }

        if (eyeShadowStat.eyeshadowGood == true)
        {
            image.sprite = frame1;
        }
        
    }

}
