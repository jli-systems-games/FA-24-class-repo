using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeText4 : MonoBehaviour
{
    public blush blushStat;
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

    private IEnumerator subtitleTimingN2()
    {
        subDone = true;
        subtitle.text = "Now she looks like she got punched twice.";
        yield return new WaitForSeconds(3f);
        subtitle.text = " ";

    }

    void Update()
    {
        if (eyeshadowStat.eyeshadowGood == true && blushStat.blushGood == true && blushStat.subchange == true && subDone == false)
        {
            StartCoroutine(subtitleTimingP());
        }

        if (eyeshadowStat.eyeshadowGood == true && blushStat.blushGood == false && blushStat.subchange == true && subDone == false)
        {
            StartCoroutine(subtitleTimingN());
        }

        if (eyeshadowStat.eyeshadowGood == false && blushStat.blushGood == false&& blushStat.subchange == true && subDone == false)
        {
            StartCoroutine(subtitleTimingN2());
        }
    }
}
