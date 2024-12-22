using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Break_QTE : MonoBehaviour
{
    private int key;
    private int lastKey = -1;

    private bool run;
    private float timer;
    private bool setup;
    private Vector3 targetPosition;
    public Transform spot0;
    private bool countDown;

    public GameObject cover;
    public GameObject[] boxes = new GameObject[4];
    public GameObject[] letters = new GameObject[4];

    [Header("Shrimp")]
    public GameObject shrimp;
    public Sprite[] shrimps;
    private int lastShrimp = -1;


    [Header("Combo")]
    private int combo;
    public GameObject comboBox;
    //combo is now correct key presses
    public TMP_Text displayText;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = spot0.GetComponent<RectTransform>().position;

        displayText.text = "";

        combo = 0;
        cover.SetActive(true);
        timer = 30;
        //KeyGen();

        foreach (GameObject item in boxes)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in letters)
        {
            item.SetActive(false);
        }

        StartCoroutine(BD_Start());

    }

    IEnumerator BD_Start()
    {
        displayText.text = "BREAKDANCING!\n\nshow of those moves.\nshrimp? more like shrizz...";
        yield return new WaitForSeconds(3f);

        displayText.text = "Press the highlighted letter on your keyboard!\n\nYou have 30 seconds to click as many letters correctly as possible!";
        yield return new WaitForSeconds(8f);

        displayText.text = "";
        cover.SetActive(false);
        setup = true;
        //go to update
    }

    private void BD_PlayScenes()
    {
        timer = 4;
        countDown = true;
        //go to update
    }

    IEnumerator BeginGame()
    {
        yield return new WaitForSeconds(1f);

        displayText.text = "";
        timer = 30;
        KeyGen();

        foreach (GameObject item in letters)
        {
            item.SetActive(true);
        }
        //go down
    }
    // Update is called once per frame
    void Update()
    {

        if (setup)
        {
            //shrimp walks onto stage, 
            float moveSpeed = 1000f;
            shrimp.GetComponent<RectTransform>().position = Vector3.MoveTowards(shrimp.GetComponent<RectTransform>().position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(shrimp.GetComponent<RectTransform>().position, targetPosition) < 0.1f)
            {
                setup = false;
                BD_PlayScenes();
                //go back up
            }
        }

        if (countDown)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
                displayText.text = $"{timer:0}";
            }
            else
            {
                displayText.text = "Start!";
                StartCoroutine(BeginGame());
                //go back up
                countDown = false;
            }

        }

        if (run)
        {
            timer -= Time.deltaTime;
            comboBox.GetComponent<TMP_Text>().text = $"{timer:0}";

            if (timer > 0)
            {
                string[] validKeys = { "Q", "W", "E", "R" };
                KeyCode correctKey = (KeyCode)System.Enum.Parse(typeof(KeyCode), validKeys[key]);

                if (Input.GetKeyDown(correctKey))
                {
                    Correct();
                }
                else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.W) ||
                           Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R))
                {
                    Incorrect();
                }
            }
            else
            {
                StartCoroutine(Result());
                run = false;
            }
        }
    }

    private void Correct()
    {
        combo++;

        audioSource.GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(1, sounds.Length)]);
        NextShrimp();

        KeyGen();
    }

    private void Incorrect()
    {
        combo--;

        audioSource.GetComponent<AudioSource>().PlayOneShot(sounds[0]);
        shrimp.GetComponent<SpriteRenderer>().sprite = shrimps[1];

        KeyGen();
    }

    //only for correct
    private int NextShrimp() //int can be called again if nessesary for secondary function ie. animations?
    {
        //
        int newShrimp;
        do
        {
            newShrimp = Random.Range(2, shrimps.Length);
        } while (newShrimp == lastShrimp);

        lastShrimp = newShrimp;

        shrimp.GetComponent<SpriteRenderer>().sprite = shrimps[newShrimp];

        return newShrimp;
    }

    //always - generate new key
    private void KeyGen()
    {
        run = true;

        //turn off current box
        foreach (GameObject item in boxes)
        {
            item.SetActive(false);
        }
        //generate next key (ensure its not the same as last
        do
        {
            key = Random.Range(0, 4);

        } while (key == lastKey);
        //remember new key
        lastKey = key;
        //highlight corresponding box

        boxes[key].SetActive(true);
    }

    IEnumerator Result()
    {
        foreach (GameObject item in boxes)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in letters)
        {
            item.SetActive(false);
        }


        if (combo <= 25)
        {
            displayText.text = $"score:{combo}\n\nwhat a shrimpwreck...";
        }
        else if (combo > 25 && combo < 70)
        {
            displayText.text = $"score:{combo}\n\nshrimprovable...";
        }
        else if (combo >= 70)
        {
            displayText.text = $"score:{combo}\n\nshrimpressive!!";
        }


        yield return new WaitForSeconds(3f);
        GameObject.Find("SportSwitcher").GetComponent<SceneChanger>().Counter();
    }
}
