using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeText3 : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public checkGB eyeshadowStat;

    private IEnumerator subtitleTiming()
    {
        if(eyeshadowStat.eyeshadowGood == false)
        {
            subtitle.text = "O-Okay...";
        }

        if(eyeshadowStat.eyeshadowGood == true)
        {
            subtitle.text = "Okay.";
        }
        yield return new WaitForSeconds(2f);
        subtitle.text = "I'll do one of your cheeks then.";
        yield return new WaitForSeconds(1.8f);
        subtitle.text = "You can really tell you're wearing makeup when you put on blush. It's nice.";
        yield return new WaitForSeconds(4.5f);
        subtitle.text = " ";
    }

    void Start()
    {
        StartCoroutine(subtitleTiming());
    }
}
