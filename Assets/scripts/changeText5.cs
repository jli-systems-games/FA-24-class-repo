using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeText5 : MonoBehaviour
{
    public TextMeshProUGUI subtitle;

    private IEnumerator subtitleTiming()
    {
        subtitle.text = "How do I look?";
        yield return new WaitForSeconds(2f);
        subtitle.text = "You look cute";
        yield return new WaitForSeconds(1.5f);
        subtitle.text = " ";
    }

    void Start()
    {
        StartCoroutine(subtitleTiming());
    }
}
