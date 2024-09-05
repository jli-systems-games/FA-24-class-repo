using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public paddlePoint paddlepoint1;
    public paddlePoint paddlepoint2;
    public paddlePoint paddlepoint3;
    public paddlePoint paddlepoint33;
    private float points1;
    private float points2;
    private float points3;

    public GameObject endScreen;
    
    public void Update()
    {
        points1 = GameObject.FindWithTag("ppoint1").GetComponent<paddlePoint>().pointScore;
        points2 = GameObject.FindWithTag("ppoint2").GetComponent<paddlePoint>().pointScore;
        points3 = ((GameObject.FindWithTag("ppoint3").GetComponent<paddlePoint>().pointScore) + (GameObject.FindWithTag("ppoint33").GetComponent<paddlePoint>().pointScore));

        if(points1 >= 10 || points2 >= 10 || points3 >= 10 )
        {
            endScreen.SetActive(true);
        }

    }
}
