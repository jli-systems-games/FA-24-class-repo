using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;

    void Update()
    {
        /*if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        */
        if (remainingTime <= 0)
        {
            remainingTime = 0;
            SceneManager.LoadScene("gameOver");
        }


        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
