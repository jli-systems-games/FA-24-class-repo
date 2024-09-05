using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goals : MonoBehaviour
{

    public GameObject nukeSprite;

    public bool isSlider;
    public bool isWall;
    public bool isEnd;

    public void Start()
    {
        nukeSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (isSlider)
            {
                nukeSprite.transform.position = collision.transform.position;
                nukeSprite.GetComponent<SpriteRenderer>().enabled = true;
                StartCoroutine(nukePause());

                Debug.Log("hit Slider");
                GameObject.Find("GameManager").GetComponent<GameManager>().Hit();
            }
            else if (isEnd)
            {
                Debug.Log("hit End");
                GameObject.Find("GameManager").GetComponent<GameManager>().Miss();
            }
            else if (isWall)
            {
                Debug.Log("hit Wall");
                GameObject.Find("GameManager").GetComponent<GameManager>().Bouncing();
            }
        }
    }

    IEnumerator nukePause()
    {
        yield return new WaitForSeconds(3f);

        nukeSprite.GetComponent<SpriteRenderer>().enabled = false;

    }
}
