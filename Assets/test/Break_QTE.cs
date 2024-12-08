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

    public GameObject[] boxes = new GameObject[4];

    [Header("Shrimp")]
    public SpriteRenderer shrimp;
    public Sprite[] shrimps;
    private int lastShrimp = -1;


    [Header("Combo")]
    private int combo;
    public GameObject comboBox;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        KeyGen();
    }

    // Update is called once per frame
    void Update()
    {
        if (run)
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

    }

    private void Correct()
    {
        if (combo < 50)
        {
            combo++;
            comboBox.GetComponent<TMP_Text>().text = $"{combo}";

            audioSource.GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(1, sounds.Length)]);
            NextShrimp();

            KeyGen();
        }
        else
        {
            comboBox.GetComponent<TMP_Text>().text = $"{combo} end";
            run = false;

            foreach (GameObject item in boxes)
            {
                item.SetActive(false);
            }
        }
    }

    private void Incorrect()
    {
        combo = 0;
        comboBox.GetComponent<TMP_Text>().text = $"combo lost!!!";

        audioSource.GetComponent<AudioSource>().PlayOneShot(sounds[0]);
        shrimp.sprite = shrimps[1];

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

        shrimp.sprite = shrimps[newShrimp];

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
}
