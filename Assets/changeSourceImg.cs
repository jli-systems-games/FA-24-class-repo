using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeSourceImg : MonoBehaviour
{
    public Image image;

    public Sprite frame1;
    public Sprite frame2;
    public Sprite frame3;
    public Sprite frame4;


    private IEnumerator delay()
    {
        yield return new WaitForSeconds(1);
        image.sprite = frame2;
    }

    void Start()
    {
        StartCoroutine(delay());
    }

}
