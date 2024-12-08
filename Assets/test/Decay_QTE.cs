using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Decay_QTE : MonoBehaviour
{
    public float fillAmount = 1;
    public GameObject fillObject;
    private float speed = 0.5f;
    public GameObject popup;


    private bool horse;
    private bool weight;


    private bool QT_once;
    private bool QT_spam;
    private bool QT_roll;
    private bool run;

    public GameObject result;
    public GameObject letterDisplay;
    public GameObject shrimp;

    public int count; //count of sequential qte
    private int key; //key to press random.range
    private int max; //max of qte sequence
    private float waitTime; //how long to wait before starting another qte

    //private KeyCode[] correctKeys = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };

    // Start is called before the first frame update
    void Start()
    {
        count = 1;

        QT_once = false;
        QT_spam = false;

        zTurnOff();
    }

    // Update is called once per frame
    void Update()
    {
        //input
        string[] validKeys = { "Q", "W", "E", "R" };
        KeyCode correctKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), validKeys[key]);

        fillObject.GetComponent<Image>().fillAmount = fillAmount;
        fillAmount = Mathf.Clamp(fillAmount, 0, 1);

        if (QT_once)
        {
            //max of qte sequence
            max = 3;

            //run
            if (run)
            {
                fillAmount -= Time.deltaTime * speed;

                if (Input.GetKeyDown(correctKey))
                {
                    fillAmount = 1;
                    OnceResult();
                }
                else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) ||
                         Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || fillAmount == 0)
                {
                    fillAmount = 0;
                    OnceResult();
                }
            }
        }

        if (QT_spam)
        {
            //max of qte sequence
            max = 3;

            if (run)
            {
                fillAmount -= Time.deltaTime * speed;

                if (fillAmount < 1 && Input.GetKeyDown(correctKey))
                {
                    fillAmount += 0.3f / count;
                }

                if (fillAmount >= 1)
                {
                    SpamResult();
                    run = false;
                }
            }
        }

        if (QT_roll)
        {
            max = 100;
            if (Input.GetKeyDown(correctKey))
            {
                if (count < max)
                {
                    count++;
                    KeyGen();

                    Vector3 rotation = shrimp.transform.eulerAngles;

                    // Modify the Z-axis rotation
                    rotation.z -= 60f;

                    // Apply the new rotation
                    shrimp.transform.eulerAngles = rotation;
                }
            }

            if (count == max)
            {
                result.GetComponent<TMP_Text>().text = "u cross";
                StartCoroutine(Again());
            }
        }
    }

    private void KeyGen()
    {
        if (QT_once || QT_spam)
        {
            key = Random.Range(0, 4);

        }
        else if (QT_roll)
        {
            if (key <= 3 && key > 0)
            {
                key--;
            }
            else
            {
                key = 3;
            }
        }

        if (key == 0)
        {
            letterDisplay.GetComponent<TMP_Text>().text = "Q";
        }
        else if (key == 1)
        {
            letterDisplay.GetComponent<TMP_Text>().text = "W";
        }
        else if (key == 2)
        {
            letterDisplay.GetComponent<TMP_Text>().text = "E";
        }
        else if (key == 3)
        {
            letterDisplay.GetComponent<TMP_Text>().text = "R";
        }
    }

    private void OnceResult()
    {
        fillObject.GetComponent<Image>().fillAmount = fillAmount;
        run = false;
        waitTime = 1f;

        if (fillAmount == 1)
        {
            result.GetComponent<TMP_Text>().text = $"#{count} win";
        }
        else if (fillAmount == 0)
        {
            result.GetComponent<TMP_Text>().text = "loss";
        }

        StartCoroutine(Again());
    }

    private void SpamResult()
    {
        fillObject.GetComponent<Image>().fillAmount = fillAmount;
        run = false;
        waitTime = 0.5f;

        result.GetComponent<TMP_Text>().text = $"#{count} win";

        StartCoroutine(Again());
    }

    public void zTurnOff()
    {
        run = false;
        QT_spam = false;
        QT_once = false;
        QT_roll = false;

        speed = 0f;

        popup.SetActive(false);
        count = 1;

        result.GetComponent<TMP_Text>().text = "";
    }

    public void zOnce()
    {
        run = true;
        QT_once = true;
        QT_spam = false;
        QT_roll = false;

        speed = 1f;
        fillAmount = 0.99f;

        popup.SetActive(true);
        result.GetComponent<TMP_Text>().text = "";
        KeyGen();
    }

    public void zSpam()
    {
        run = true;
        QT_once = false;
        QT_spam = true;
        QT_roll = false;


        speed = 0.5f;
        fillAmount = 0;

        popup.SetActive(true);
        result.GetComponent<TMP_Text>().text = "";
        KeyGen();
    }

    public void zRoll()
    {
        run = true;
        QT_once = false;
        QT_spam = false;
        QT_roll = true;

        fillAmount = 1;
        count = 1;

        popup.SetActive(true);
        result.GetComponent<TMP_Text>().text = "";
        key = 4;
        KeyGen();
    }

    IEnumerator Again()
    {
        yield return new WaitForSeconds(0.5f);
        popup.SetActive(false);

        yield return new WaitForSeconds(waitTime);

        if (count < max)
        {
            count++;
            if (QT_once)
            {
                zOnce();
            }
            else if (QT_spam)
            {
                zSpam();
            }
            else if (QT_roll)
            {
                KeyGen();
            }
        }
        else
        {
            zTurnOff();
        }
    }
}
