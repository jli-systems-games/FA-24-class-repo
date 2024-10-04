using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flipHanako : MonoBehaviour
{
    public Image hanako;

    public Sprite hanako1;
    public Sprite hanako2;

    public Image olivia;

    public Sprite olivia1;
    public Sprite olivia2;

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
        audiosource.Play();
        StartCoroutine(picTiming());
        StartCoroutine(oliviaTime());
        StartCoroutine(shockCoroutine());
    }

}
