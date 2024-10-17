using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class eatTimer : MonoBehaviour
{
    //https://www.youtube.com/watch?v=bcvLM_riVuw
    Image timerBar;
    public float maxTime = 25f;
    public float timeLeft;
    public float difficulty = 1;
    public GameObject timesUpText;
    public GameObject ShrimpOriginal;
    public GameObject ShrimpSpot;
    public GameObject restart;


    public bool canStart;

    void Awake()
    {
        timesUpText.SetActive(false);
        restart.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
        //StartCoroutine(sdiif());

        //StartCoroutine(difficultyChanger());
        StartCoroutine(StartBar());
        canStart = false;

        ShrimpOriginal.SetActive(true);
        ShrimpSpot.SetActive(false);
    }


    private IEnumerator difficultyChanger()
    {
        yield return new WaitForSeconds(5f);
        difficulty++;
        StartCoroutine(difficultyChanger());
        //StartCoroutine(StartBar());
    }


    public void RestartGame()
    {
        timeLeft = 25f;
        Time.timeScale = 1;

        difficulty = 1f;
        StartCoroutine(StartBar());
        StartCoroutine(sdiif());
        timerBar.fillAmount = 100f;
        canStart = false;
        restart.SetActive(false);

    }

    private IEnumerator sdiif()
    {
        StartCoroutine(difficultyChanger());

        yield break;
    }

    private IEnumerator StartBar()
    {
        yield return new WaitForSeconds(5f);
        canStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canStart == true)
        {

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime * difficulty;
                timerBar.fillAmount = timeLeft / maxTime;

            }
            else
            {
                timesUpText.SetActive(true);
                restart.SetActive(true);
                Time.timeScale = 0;
            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                timeLeft = timeLeft + 5f;

                ShrimpOriginal.SetActive(false);
                ShrimpSpot.SetActive(true);
            }

            if (Input.GetKeyUp(KeyCode.D))
            {

                ShrimpOriginal.SetActive(true);
                ShrimpSpot.SetActive(false);
            }

            if (timeLeft > 25)
            {
                timeLeft = 25;
            }
        }
    }
}
