using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeKasumiSprite : MonoBehaviour
{
    public blush bgood;
    public checkGB eyeShadowStat;

    public Image kasumi;
    public Sprite kasumih;
    public Sprite kasumis;
    
    void Start()
    {
        StartCoroutine(delayChange());
    }

    private IEnumerator delayChange()
    {
        if (bgood.blushGood == true && eyeShadowStat.eyeshadowGood == true)
        {
            yield return new WaitForSeconds(3f);
            kasumi.sprite = kasumih;
        }
        if (bgood.blushGood == false && eyeShadowStat.eyeshadowGood == false)
        {
            yield return new WaitForSeconds(3f);
            kasumi.sprite = kasumis;
        }
        if (bgood.blushGood == false && eyeShadowStat.eyeshadowGood == true)
        {
            yield return new WaitForSeconds(3f);
            kasumi.sprite = kasumis;
        }
        if (bgood.blushGood == true && eyeShadowStat.eyeshadowGood == false)
        {
            yield return new WaitForSeconds(3f);
            kasumi.sprite = kasumis;
        }


    }

}
