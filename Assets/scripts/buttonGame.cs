using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonGame : MonoBehaviour
{
    public TextMeshProUGUI text;

    public string[] hitOrNot;
    public string decision;

    public Sprite bunSad;
    public Sprite bunHappy;

    public Button bunny;
    public bool BunClicked = false;

    public bool timerException = false;

    public GameObject gameCanvas;
    public GameObject transitionCanvas;


    public GameObject timerGame2;
    public timerGame2 timerr;


    private IEnumerator delayText()
    {
        yield return new WaitForSeconds(1);
        decision = hitOrNot[Random.Range(0, hitOrNot.Length)];
        text.text = decision;
        yield return new WaitForSeconds(1);
        text.text = " ";
    }

    private IEnumerator outcome()
    {

        if(decision == "don't hit the bunny" && BunClicked == true)
        {
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("gameOver");
        }

        if(decision == "don't hit the bunny" && BunClicked == false && 
        timerr.remainingTime == 0)
        {
            yield return new WaitForSeconds(1);
            bunny.image.sprite = bunHappy;
            yield return new WaitForSeconds(1);
            transitionCanvas.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(false);

        }

        if(decision == "hit the bunny" && BunClicked == true)
        {
            yield return new WaitForSeconds(1);
            text.text = "good";
            yield return new WaitForSeconds(1);
            transitionCanvas.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(false);
        }

        if(decision == "hit the bunny" && BunClicked == false)
        {
            yield return new WaitForSeconds(1);
            bunny.image.sprite = bunHappy;
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene("gameOver");
        }
        
    }

    private IEnumerator outcomeException()
    {
            timerException = true;
            yield return new WaitForSeconds(1);
            bunny.image.sprite = bunHappy;
            yield return new WaitForSeconds(1);
            transitionCanvas.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(false);
    }

    public void bunnyClicked()
    {
        bunny.image.sprite = bunSad;
        BunClicked = true;
        StartCoroutine(outcome());
    }

    void Start()
    {
        StartCoroutine(delayText());
        GameObject timerGame2 = GameObject.FindWithTag("timer");
        timerr = timerGame2.GetComponent<timerGame2>();
    }

    void Update()
    {
        if(decision == "don't hit the bunny" && BunClicked == false && 
        timerr.remainingTime <= 1)
        {
            timerException = true;
            StartCoroutine(outcomeException());
        }
        Debug.Log(timerException);
    }

}
