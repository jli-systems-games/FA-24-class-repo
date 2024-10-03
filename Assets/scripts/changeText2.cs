using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeText2 : MonoBehaviour
{
    public TextMeshProUGUI subtitle;

    private IEnumerator subtitleTiming()
    {
        subtitle.text = "Hey, do that half face makeup thing they do on magazines.";
        yield return new WaitForSeconds(4.3f);
        subtitle.text = "I want to see the before and after.";
        yield return new WaitForSeconds(2.5f);
        subtitle.text = " ";
    }

    void Start()
    {
        StartCoroutine(subtitleTiming());
    }
}