using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreUpdate : MonoBehaviour
{
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public paddlePoint paddlepoint1;
    public paddlePoint paddlepoint2;
    public paddlePoint paddlepoint3;
    public paddlePoint paddlepoint33;
    private string points1;
    private string points2;
    private string points3;

    public void Update()
    {
        points1 = GameObject.FindWithTag("ppoint1").GetComponent<paddlePoint>().pointScore.ToString();
        points2 = GameObject.FindWithTag("ppoint2").GetComponent<paddlePoint>().pointScore.ToString();
        points3 = ((GameObject.FindWithTag("ppoint3").GetComponent<paddlePoint>().pointScore) + (GameObject.FindWithTag("ppoint33").GetComponent<paddlePoint>().pointScore)).ToString();

        text1.text = "P1 points:" + points1;
        text2.text = "P2 points:" + points2;
        text3.text = "P3 points:" + points3;
    }

}
