using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class blush : MonoBehaviour
{
    public checkGB eyeShadowStat;
    public bool subchange;


    public Image hanako;
    public Sprite GERFB;
    public Sprite BERFB;
    public Sprite GEBB;
    public Sprite BEBB;
    public Sprite GEGB;
    public Sprite BEGB;
    public Sprite good;

    public int blushTaps;

    public bool blushGood;

    public bool ended;

    public Texture2D cursorTexture;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject regularCanvas;
    public GameObject nextCanvas;
    public GameObject currentCanvas;

    public AudioSource nextLines;
    public AudioSource nextLinesold;
    public AudioSource oldlinesGood;
    public AudioSource neutral;
    
    public GameObject blushComponent;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        if (eyeShadowStat.eyeshadowGood == true)
        {
            hanako.sprite = GERFB;
        }

        if (eyeShadowStat.eyeshadowGood == false)
        {
            hanako.sprite = BERFB;
        }
    }

    public void BlushTaps()
    {
        blushTaps += 1;
    }

    public void blusher()
    {
        if (blushTaps == 1)
        {
            blushGood = true;
        }
        if (blushTaps > 1)
        {
            blushGood = false;
        }

        StartCoroutine(checking());
    }

    private IEnumerator checking()
    {
        Debug.Log("started");
        yield return new WaitForSeconds(1f);
        if (blushGood == true && eyeShadowStat.eyeshadowGood == true)
        {
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            oldlinesGood.Play();
            blushComponent.gameObject.SetActive(false);
            subchange = true;
            hanako.sprite = good;
            yield return new WaitForSeconds(2f);
            hanako.sprite = GEGB;
        }
        if (blushGood == false && eyeShadowStat.eyeshadowGood == true)
        {
            Debug.Log("playing old lines");
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            nextLinesold.Play();
            regularCanvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            regularCanvas.gameObject.SetActive(false);
            blushComponent.gameObject.SetActive(false);
            subchange = true;
            hanako.sprite = GEBB;
        }
        if (blushGood == true && eyeShadowStat.eyeshadowGood == false)
        {
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            neutral.Play();
            regularCanvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(5);
            regularCanvas.gameObject.SetActive(false);
            blushComponent.gameObject.SetActive(false);
            subchange = true;
            hanako.sprite = BEGB;
        }
        if (blushGood == false && eyeShadowStat.eyeshadowGood == false)
        {
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            nextLines.Play();
            regularCanvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(5f);
            regularCanvas.gameObject.SetActive(false);
            blushComponent.gameObject.SetActive(false);
            subchange = true;
            hanako.sprite = BEBB;
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (ended == false && Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorTexture2, hotSpot, cursorMode);
        }

        if (ended == false && Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
}
