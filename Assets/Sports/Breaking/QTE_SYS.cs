using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE_SYS : MonoBehaviour
{
    public GameObject QBox;
    public GameObject WBox;
    public GameObject EBox;
    public GameObject RBox;
    public float nextAlpha = 0.5f;
    public float nowAlpha = 1f;

    public float points;
    public float combo;

    //public GameObject PassBox;


    public GameObject TimerBox;
    public int Timer;

    public GameObject[] shrimps = new GameObject[5];

    public AudioSource audioSource;
    public AudioClip yay;
    public AudioClip boo;

    public int QTEGen;
    public int WaitingForKey;
    public int CorrectKey;
    public int CountingDown;


    public float rotationSpeed = 300f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (WaitingForKey == 0)
        {
            //generates random number -> which letter gets displayed
            QTEGen = Random.Range(1, 5);

            //sets this to 1, that way in CountDown() after 3.5 seconds pass and its still 1, i will fail as a time out
            CountingDown = 1;
            StartCoroutine(CountDown());

            //PassBox.GetComponent<TMP_Text>().text = "";

            shrimps[0].SetActive(true);

            //letter displays, increase waitingforkey to 1 so we know were not waiting anymore
            if (QTEGen == 1)
            {
                WaitingForKey = 1;

                var image = QBox.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, nowAlpha);

            }
            if (QTEGen == 2)
            {
                WaitingForKey = 1;

                var image = WBox.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, nowAlpha);
            }
            if (QTEGen == 3)
            {
                WaitingForKey = 1;

                var image = EBox.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, nowAlpha);
            }
            if (QTEGen == 4)
            {
                WaitingForKey = 1;

                var image = RBox.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, nowAlpha);
            }
        }
        //looking at clicked key, 1 = right, 2 = wrong
        if (QTEGen == 1)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("QKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 2)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("WKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 3)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("EKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }
        if (QTEGen == 4)
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetButtonDown("RKey"))
                {
                    CorrectKey = 1;
                    StartCoroutine(KeyPressing());
                }
                else
                {
                    CorrectKey = 2;
                    StartCoroutine(KeyPressing());
                }
            }
        }

        if (shrimps[0].activeSelf)
        {
            shrimps[0].transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }

    IEnumerator KeyPressing()
    {
        QTEGen = 5;

        //correct
        if (CorrectKey == 1)
        {
            //increase counting down for a bit to not trigger the time out
            CountingDown = 2;
            //PassBox.GetComponent<TMP_Text>().text = "oh yeahh";

            audioSource.PlayOneShot(yay);

            if (shrimps[0].activeSelf)
            {
                shrimps[0].SetActive(false);

                int shrimp;
                shrimp = Random.Range(2, 5);
                shrimps[shrimp].SetActive(true);
            }


            yield return new WaitForSeconds(1.5f);
            //resets correctkey for next input
            CorrectKey = 0;
            //PassBox.GetComponent<TMP_Text>().text = "";
            //DisplayBox.GetComponent<TMP_Text>().text = "";


            foreach (GameObject item in shrimps)
            {
                if (item.activeSelf)
                {
                    item.SetActive(false);
                }
            }
            shrimps[0].SetActive(true);


            yield return new WaitForSeconds(1.5f);

            //resets waitingforkey so it can rerun the random range
            WaitingForKey = 0;
            //resets countingdown for next letter
            CountingDown = 1;
        }
        if (CorrectKey == 2)
        {
            CountingDown = 2;
            //PassBox.GetComponent<TMP_Text>().text = "umm";

            audioSource.PlayOneShot(boo);


            if (shrimps[0].activeSelf)
            {
                shrimps[0].SetActive(false);
                shrimps[1].SetActive(true);
            }


            yield return new WaitForSeconds(1.5f);
            CorrectKey = 0;
            //PassBox.GetComponent<TMP_Text>().text = "";
            //DisplayBox.GetComponent<TMP_Text>().text = "";


            foreach (GameObject item in shrimps)
            {
                if (item.activeSelf)
                {
                    item.SetActive(false);
                }
            }
            shrimps[0].SetActive(true);


            yield return new WaitForSeconds(1.5f);
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }

    IEnumerator CountDown()
    {
        //how long to time out
        yield return new WaitForSeconds(3.5f);
        if (CountingDown == 1)
        {
            //increase numbers so they dont trigger anything
            QTEGen = 5;
            CountingDown = 2;
            //PassBox.GetComponent<TMP_Text>().text = "umm";

            audioSource.PlayOneShot(boo);


            if (shrimps[0].activeSelf)
            {
                shrimps[0].SetActive(false);
                shrimps[1].SetActive(true);
            }


            yield return new WaitForSeconds(1.5f);

            //reset correct key for next input
            CorrectKey = 0;
            //PassBox.GetComponent<TMP_Text>().text = "";
            //DisplayBox.GetComponent<TMP_Text>().text = "";


            foreach (GameObject item in shrimps)
            {
                if (item.activeSelf)
                {
                    item.SetActive(false);
                }
            }
            shrimps[0].SetActive(true);


            yield return new WaitForSeconds(1.5f);

            //reset both for next iteration
            WaitingForKey = 0;
            CountingDown = 1;
        }
    }
}
