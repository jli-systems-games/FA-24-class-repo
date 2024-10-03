using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class checkGB : MonoBehaviour
{

    public bool enteredArea;
    public int inArea;
    public int outArea;

    public bool notcalled = true;

    public Image image;
    public Sprite bad;
    public Sprite good;
    public Sprite good2;
    
    public AudioSource audioSource;
    public AudioSource nextLines;
    public AudioSource nextLinesGood;

    public bool ended;
    public bool subchange;

    public bool eyeshadowGood;

    public GameObject regular2Canvas;
    public GameObject nextCanvas;
    public GameObject currentCanvas;

    private IEnumerator delayCheck()
    {
        Debug.Log("start coroutine");
        yield return new WaitForSeconds(4);
        if (outArea > inArea)
        {
            eyeshadowGood = false;
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            nextLines.Play();
            regular2Canvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            regular2Canvas.gameObject.SetActive(false);
            subchange = true;
            image.sprite = bad;
        }

        if (outArea < inArea)
        {
            eyeshadowGood = true;
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            subchange = true;
            nextLinesGood.Play();
            image.sprite = good;
            yield return new WaitForSeconds(2f);
            image.sprite = good2;
            
        }

        if(ended == false)
        {
            StartCoroutine(delayCheck());
        }
    }


    void Update()
    {

        RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            enteredArea = true;
        }
        else
        {
            enteredArea = false;
        }

        Debug.Log(enteredArea);

        if (ended == false)
        {

            if (enteredArea == false && Input.GetMouseButton(0))
            {
                outArea += 1;
                if (notcalled == true)
                {
                    notcalled = false;
                    StartCoroutine(delayCheck());
                }
            }
        
            if (enteredArea == true && Input.GetMouseButton(0))
            {
                inArea += 1;
                if (notcalled == true)
                {
                    notcalled = false;
                    StartCoroutine(delayCheck());
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("played");
                audioSource.Play();
            }

        }

        if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Paused");
                audioSource.Pause();
            }
        if(ended == true)
        {
            if (outArea > inArea && !nextLines.isPlaying)
            {
                nextCanvas.gameObject.SetActive(true);
                currentCanvas.gameObject.SetActive(false);
            }
            
        
            if (outArea < inArea && !nextLinesGood.isPlaying)
            {
                nextCanvas.gameObject.SetActive(true);
                currentCanvas.gameObject.SetActive(false);
            }
    
        }
    }
}
