using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeText : MonoBehaviour
{
    public TextMeshProUGUI subtitle;
    public checkGB eyeshadowStat;

    public bool subDone = false;

    private IEnumerator subtitleTimingP()
    {
        subDone = true;
        subtitle.text = "Cute!";
        yield return new WaitForSeconds(1f);
        subtitle.text = " ";
        yield return new WaitForSeconds(0.2f);
        subtitle.text = "Cute!";
        yield return new WaitForSeconds(1f);
        subtitle.text = "Squee!";
        yield return new WaitForSeconds(1f);
        subtitle.text = " ";

    }

    private IEnumerator subtitleTimingN()
    {
        subDone = true;
        subtitle.text = "She looks like she got punched.";
        yield return new WaitForSeconds(3f);
        subtitle.text = " ";

    }

    void Update()
    {
        if (eyeshadowStat.eyeshadowGood == true && eyeshadowStat.subchange == true && subDone == false)
        {
            StartCoroutine(subtitleTimingP());
        }

        if (eyeshadowStat.eyeshadowGood == false && eyeshadowStat.subchange == true && subDone == false)
        {
            StartCoroutine(subtitleTimingN());
        }
    }
}
