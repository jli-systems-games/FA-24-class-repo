using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void SetScore(int value)
    {
        text.text = value.ToString();
    }
}
