using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SnowballRolling : MonoBehaviour
{
    public bool small;

    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 initialScale;

    public GameObject rHand;
    public GameObject lHand;
    public float speed = 2;
    private float elapsedTime = 0f;

    public float mouseY;

    public static float finalSnowballScale = 1f;
    public GameObject button;
    public AudioClip ding;

    public AudioSource rollingaudiosource;
    public AudioSource dingaudiosource;
    public AudioClip rollingSound;

    public GameObject text;

    public bool play;

    public void Start()
    {
        //audiosource = GetComponent<AudioSource>();

        play = true;
    }

    public void Update()
    {
        if (play == false)
        {
            if (rollingaudiosource.isPlaying)
            {
                rollingaudiosource.Pause();
            }
        }
    }

    void OnMouseDown()
    {
        if (play == true)
        {
            if (text.activeSelf)
            {
                text.SetActive(false);
            }

            screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

            offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

            initialScale = transform.localScale;


            if (!rollingaudiosource.isPlaying)
            {
                rollingaudiosource.Play();
            }
        }
    }

    void OnMouseDrag()
    {
        if (play == true)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;

            float dragDistanceY = curPosition.y - transform.position.y;
            Debug.Log(dragDistanceY);

            if (dragDistanceY > 0)
            {
                float scaleFactor = 1 + dragDistanceY * 0.05f;
                transform.localScale = new Vector3(initialScale.x * scaleFactor, initialScale.y * scaleFactor, initialScale.z * scaleFactor);

                HandRolling();
            }
        }
    }


    void OnMouseUp()
    {

        if (play == true)
        {
            finalSnowballScale = transform.localScale.x;

            if (finalSnowballScale >= 2.7f && small == false)
            {
                StartCoroutine(PlayDing());
                button.SetActive(true);

                play = false;
            }

            if (finalSnowballScale >= 1.65f && small == true)
            {
                StartCoroutine(PlayDing());
                button.SetActive(true);

                play = false;
            }

            if (rollingaudiosource.isPlaying)
            {
                rollingaudiosource.Pause();
            }
        }
        Debug.Log("Final snowball size: " + finalSnowballScale);
    }

    private IEnumerator PlayDing()
    {
        yield return null;
        dingaudiosource.PlayOneShot(ding);
    }

    public void HandRolling()
    {
        elapsedTime += Time.deltaTime;

        float yR = Mathf.PingPong(elapsedTime * speed, 1) * 1.5f - 3.5f;
        rHand.transform.position = new Vector3(rHand.transform.position.x, yR, rHand.transform.position.z);

        float yL = Mathf.PingPong(elapsedTime * speed, 1) * -1.5f - 2.5f;
        lHand.transform.position = new Vector3(lHand.transform.position.x, yL, lHand.transform.position.z);
    }
}
