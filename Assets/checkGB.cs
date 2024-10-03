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

    public Image image;
    public Sprite bad;
    public Sprite good;
    
    public AudioSource audioSource;
    public AudioSource nextLines;
    public AudioSource nextLinesGood;

    public bool ended;

    public GameObject regular2Canvas;

    private IEnumerator delayCheck()
    {
        Debug.Log("start coroutine");
        yield return new WaitForSeconds(5);
        if (outArea > inArea)
        {
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            nextLines.Play();
            regular2Canvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            regular2Canvas.gameObject.SetActive(false);
            image.sprite = bad;
        }

        if (outArea < inArea)
        {
            ended = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            nextLinesGood.Play();
            image.sprite = good;
            
        }

        if(ended == false)
        {
            StartCoroutine(delayCheck());
        }
    }

    void Start()
    {
        StartCoroutine(delayCheck());
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

        if (ended == false)
        {

            if (enteredArea == false && Input.GetMouseButton(0))
            {
                outArea += 1;
            }
        
            if (enteredArea == true && Input.GetMouseButton(0))
            {
                inArea += 1;
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
        

    }
}
