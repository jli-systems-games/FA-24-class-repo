using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalScore : MonoBehaviour
{
    public static int currentScore = 0;

    public GameObject csText;

    void Update()
    {
        DisplayScore();
    }

    void DisplayScore()
    {
        csText.GetComponent<TextMeshProUGUI>().text = currentScore.ToString();
    }
}
