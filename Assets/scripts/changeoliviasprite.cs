using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeoliviasprite : MonoBehaviour
{
    public blush bgood;
    public checkGB eyeShadowStat;

    public Image olivia;
    public Sprite oliviah;
    public Sprite olivias;
    
    void Start()
    {
        StartCoroutine(delayChange());
    }

    private IEnumerator delayChange()
    {
        if (bgood.blushGood == true && eyeShadowStat.eyeshadowGood == true)
        {
            yield return new WaitForSeconds(3f);
            olivia.sprite = oliviah;
        }
        if (bgood.blushGood == false && eyeShadowStat.eyeshadowGood == false)
        {
            yield return new WaitForSeconds(3f);
            olivia.sprite = olivias;
        }
        if (bgood.blushGood == false && eyeShadowStat.eyeshadowGood == true)
        {
            olivia.sprite = olivias;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
