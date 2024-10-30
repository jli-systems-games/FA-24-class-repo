using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    public GameObject winningPanel;
    public GameObject endingPanel;
    public Event_Sim simScript;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            timerText.color = Color.red;

            CheckGameResult();
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void CheckGameResult()
    {
        if (!winningPanel.activeSelf && !endingPanel.activeSelf)
        {
            if (simScript.needHunger > 0 && simScript.needEnergy > 0 && simScript.needEntertainment > 0)
            {
                winningPanel.SetActive(true);
            }
            else
            {
                endingPanel.SetActive(true);
            }
        }
    }

}
