using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    [SerializeField] GameObject partyModeButton;
    [SerializeField] float partyTime = 30f;
    [SerializeField] float buttonDisplayTime = 2f;

    private bool partyModeActive = false;
    private bool buttonDisplayedOnce = false;

    public GameObject winningPanel;
    public GameObject endingPanel;
    public Event_Sim simScript;
    public GameObject discoBall;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= partyTime && !partyModeActive && !buttonDisplayedOnce)
            {
                StartCoroutine(DisplayPartyButton());
            }
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

    IEnumerator DisplayPartyButton()
    {
        buttonDisplayedOnce = true;
        partyModeButton.SetActive(true);

        yield return new WaitForSeconds(buttonDisplayTime);

        if (!partyModeActive)
        {
            partyModeButton.SetActive(false);
        }
    }

    public void ActivatePartyMode()
    {
        partyModeActive = true;
        partyModeButton.SetActive(false);
        discoBall.SetActive(true);
    }

    void CheckGameResult()
    {
        if (!winningPanel.activeSelf && !endingPanel.activeSelf)
        {
            if (simScript.needHunger > 0 && simScript.needEnergy > 0 && simScript.needEntertainment > 0)
            {
                winningPanel.SetActive(true);
                endingPanel.SetActive(false);
            }

            else if (simScript.needHunger == 0 || simScript.needEnergy == 0 || simScript.needEntertainment == 0)
            {
                endingPanel.SetActive(true);
                winningPanel.SetActive(false);
            }
        }
    }
}
