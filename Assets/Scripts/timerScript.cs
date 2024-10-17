using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
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
    


    void Awake()
    {
        timesUpText.SetActive(false);
        restart.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;

        //StartCoroutine(difficultyChanger());

        ShrimpOriginal.SetActive(true);
        ShrimpSpot.SetActive(false);

    }

    private IEnumerator difficultyChanger()
    {
        yield return new WaitForSeconds(5f);
        difficulty++;
        StartCoroutine(difficultyChanger());
    }


    public void RestartGame()
    {
        timeLeft = 25f;
        Time.timeScale = 1;

        difficulty = 1f;

        StartCoroutine(sdiif());
        timerBar.fillAmount = 100f;
        restart.SetActive(false);
    }

    private IEnumerator sdiif()
    {
        StartCoroutine(difficultyChanger());

        yield break;
    }

    // Update is called once per frame
    void Update()
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

        if (timeLeft > 25)
        {
            timeLeft = 25;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            timeLeft = timeLeft + 5f;

            ShrimpOriginal.SetActive(false);
            ShrimpSpot.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {

            ShrimpOriginal.SetActive(true);
            ShrimpSpot.SetActive(false);
        }
    }

}
