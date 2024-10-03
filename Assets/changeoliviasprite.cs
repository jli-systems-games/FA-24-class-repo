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
        yield return new WaitForSeconds(3f);
        if (bgood.blushGood == true && eyeShadowStat.eyeshadowGood == true)
        {
            olivia.sprite = oliviah;
        }
        if (bgood.blushGood == false)
        {
            olivia.sprite = olivias;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
