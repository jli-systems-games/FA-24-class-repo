using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flipHanako : MonoBehaviour
{
    public Image hanako;
    public Image kasumi;

    public Sprite kasumi1;
    public Sprite kasumi2;

    public Sprite hanako1;
    public Sprite hanako2;

    public Image olivia;

    public Sprite olivia1;
    public Sprite olivia2;
    public Sprite oliviaP1;
    public Sprite oliviaP2;

    public AudioSource audiosource;

    public GameObject teacher;
    public GameObject shock;

    public Image shockImg;

    public Sprite hanakoGEGB;
    public Sprite hanakoGEBB;
    public Sprite hanakoBEGB;
    public Sprite hanakoBEBB;

    public checkGB eyeshadowStat;
    public blush blushStat;

    public GameObject replayButton;

    private IEnumerator picTiming()
    {
        hanako.sprite = hanako2;
        yield return new WaitForSeconds(0.9f);
        hanako.sprite = hanako1;
        teacher.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.9f);
        hanako.sprite = hanako2;
        yield return new WaitForSeconds(0.9f);
        hanako.sprite = hanako1;
    }

    private IEnumerator oliviaTime()
    {
        if (eyeshadowStat.eyeshadowGood == true && blushStat.blushGood == true)
        {
            yield return new WaitForSeconds(2.4f);
            olivia.sprite = oliviaP2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP1;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP1;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP1;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = oliviaP1;
        }

        else
        {
            yield return new WaitForSeconds(2.4f);
            olivia.sprite = olivia2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia1;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia1;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia1;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia2;
            yield return new WaitForSeconds(0.2f);
            olivia.sprite = olivia1;
        }

    }

    private IEnumerator shockCoroutine()
    {
        yield return new WaitForSeconds(4.3f);
        shock.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        if (eyeshadowStat.eyeshadowGood == true && blushStat.blushGood == true)
        {
            shockImg.sprite = hanakoGEGB;
        }

        if (eyeshadowStat.eyeshadowGood == true && blushStat.blushGood == false)
        {
            shockImg.sprite = hanakoGEBB;
        }

        if (eyeshadowStat.eyeshadowGood == false && blushStat.blushGood == true)
        {
            shockImg.sprite = hanakoBEGB;
        }

        if (eyeshadowStat.eyeshadowGood == false && blushStat.blushGood == false)
        {
            shockImg.sprite = hanakoBEBB;
        }
    }

    void Start()
    {
        if (eyeshadowStat.eyeshadowGood == true && blushStat.blushGood == true)
        {
            olivia.sprite = oliviaP1;
            kasumi.sprite = kasumi1;
        }
        else
        {
            olivia.sprite = olivia1;
            kasumi.sprite = kasumi2;
        }


        audiosource.Play();
        StartCoroutine(picTiming());
        StartCoroutine(oliviaTime());
        StartCoroutine(shockCoroutine());
    }

    void Update()
    {
        if (!audiosource.isPlaying)
        {
            replayButton.gameObject.SetActive(true);
        }
    }

}
