using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class clapGame : MonoBehaviour
{
    public TextMeshProUGUI text;

    public Sprite bun2;
    public Sprite bun3;
    public Sprite bun4;
    public Sprite bunHap;
    public Sprite bunCry;

    public Button bunny;

    public Sprite handclap1;
    public Sprite handclap2;

    public Button hands;

    public GameObject handss;
    public GameObject timer;

    public bool clapped = false;

    public GameObject gameCanvas;
    public GameObject transitionCanvas;

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2);
        bunny.image.sprite = bun2;
        text.text = " ";
        yield return new WaitForSeconds(2);
        bunny.image.sprite = bun3;
        yield return new WaitForSeconds(2);
        text.text = "oh no";
        bunny.image.sprite = bun4;
        yield return new WaitForSeconds(2);
        text.text = "quick, clap!!";
        yield return new WaitForSeconds(1);
        text.text = " ";
        timer.gameObject.SetActive(true);
        handss.gameObject.SetActive(true);
    }

    private IEnumerator outcome()
    {

        if(clapped == true)
        {
            yield return new WaitForSeconds(2);
            bunny.image.sprite = bunHap;
            yield return new WaitForSeconds(1);
            transitionCanvas.gameObject.SetActive(true);
            gameCanvas.gameObject.SetActive(false);
        }
    }

    private IEnumerator delayCry()
    {
        yield return new WaitForSeconds(2);
        bunny.image.sprite = bunCry;
    }
        
    private IEnumerator resetClap()
    {

        hands.image.sprite = handclap2;
        yield return new WaitForSeconds(0.3f);
        hands.image.sprite = handclap1;
        
    }


    public void clappedd()
    {
        StartCoroutine(resetClap());
        clapped = true;
        StartCoroutine(outcome());
    }


    void Start()
    {
        StartCoroutine(StartGame());
    }

    void Update()
    {
        if(clapped == false && handss.gameObject.activeSelf == true)
        {
            StartCoroutine(delayCry());
        }
    }

}
