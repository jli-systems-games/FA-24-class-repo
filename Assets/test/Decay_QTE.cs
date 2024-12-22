using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Decay_QTE : MonoBehaviour
{

    private bool QT_once;
    private bool QT_spam;
    private bool QT_roll;
    private bool run;


    private int count; //count of sequential qte
    private int key; //key to press random.range
    private int max; //max of qte sequence
    private float waitTime; //how long to wait before starting another qte

    [Header("poipup")]
    public float fillAmount = 1;
    public GameObject fillObject;
    private float speed = 0.5f;
    public GameObject letterDisplay;
    public GameObject popup;


    [Header("generic")]
    //public GameObject result;
    public GameObject shrimp;
    public GameObject cover;
    public TMP_Text displayText;
    private List<string> wordsList = new List<string> { "shrimptastic!", "shrimptacular!", "its as shrimple as that", "youre shrimpcredible!", "very shrimpressive!" };

    [Header("horse")]
    public GameObject horses;
    public List<GameObject> horse;
    public List<GameObject> hurdles;
    public List<Transform> spots;
    private bool setup;
    private bool breakHurdle;
    private Vector3 targetPosition;
    private GameObject targetHorse;
    private float timer = 1;

    [Header("roll")]
    public GameObject miniShrimp;
    public GameObject track;

    [Header("weight")]
    public GameObject shrimps;
    public List<GameObject> weightShrimp = new();
    public GameObject bars;
    public List<GameObject> weightBars = new();
    public GameObject bench;


    //private KeyCode[] correctKeys = { KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R };

    // Start is called before the first frame update
    void Start()
    {
        count = 1;

        zTurnOff();

        SceneDetect();
    }

    private void SceneDetect()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        string sceneName = activeScene.name;

        cover.SetActive(true);


        if (sceneName == "Running")
        {
            StartCoroutine(R_Start());
            Debug.Log("roll");
        }
        else if (sceneName == "Weight")
        {
            foreach (GameObject item in weightShrimp)
            {
                item.SetActive(false);
            }
            foreach (GameObject item in weightBars)
            {
                item.SetActive(false);
            }

            StartCoroutine(W_Start());
            Debug.Log("weight");
        }
        else if (sceneName == "Equestrian")
        {
            StartCoroutine(H_Start());
            Debug.Log("horse");
        }
    }

    IEnumerator R_Start()
    {
        //displayText.text = "Running!\n\nu spin me round and round and round and round and round and round and round";
        //yield return new WaitForSeconds(3);
        //displayText.text = "o shit spoilers sorry";
        //yield return new WaitForSeconds(0.5f);
        //displayText.text = "click the keys shown on your keyboard in the order shown!\n\nkeep going till i finish i uh haha typo i mean yknow until u cross that finish line.";
        //yield return new WaitForSeconds(0.7f);




        displayText.text = "Running!\n\n its like swimming without the water!hahahahahahahahahahahahahahahahahahahhahahahahahahahahahahahhaha";
        yield return new WaitForSeconds(4f);
        displayText.text = "click the keys\nR, E, W, & Q repeatedly to cross the finish line!";
        yield return new WaitForSeconds(5f);
        displayText.text = "Start!";
        yield return new WaitForSeconds(1f);
        displayText.text = "";
        cover.SetActive(false);
        zRoll();
    }

    IEnumerator W_Start()
    {
        displayText.text = "Weightlifting!\n\n oh yeaaa ur a strong little shrimp arent u";
        yield return new WaitForSeconds(3f);
        displayText.text = "uhhh not in a weird way tho yknow just like ur about to lift weights so...\n\nshow em what u got!";
        yield return new WaitForSeconds(5f);
        displayText.text = "Mash the corresponding key on your keyboard and lift all three weights!";
        yield return new WaitForSeconds(5f);
        displayText.text = "Start!";
        yield return new WaitForSeconds(1f);
        bench.SetActive(true);
        displayText.text = "";
        cover.SetActive(false);
        zSpam();
    }

    IEnumerator H_Start()
    {
        foreach (GameObject item in horse)
        {
            item.SetActive(false);
        }
        foreach (GameObject item in hurdles)
        {
            item.SetActive(false);
        }

        displayText.text = "Horseback Riding!";
        yield return new WaitForSeconds(1f);
        displayText.text = "Quickly click the indicated key to jump over all three hurdles smoothly!";
        yield return new WaitForSeconds(5);
        displayText.text = "start!";
        yield return new WaitForSeconds(1);
        displayText.text = "";
        cover.SetActive(false);
        //zOnce();

        horse[0].SetActive(true);
        horse[0].transform.position = spots[0].position;
        hurdles[0].SetActive(true);

        //horses.transform.position = spots[2].position;
        targetPosition = spots[0].position;
        setup = true;
    }

    IEnumerator horseSetUp()
    {
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

        if (setup)
        {//horse
            targetPosition = spots[1].position;

            float moveSpeed = 1000f;

            RectTransform rectTransform = horses.GetComponent<RectTransform>();
            Vector3 position = rectTransform.position; // Get the current position
            position.y = Mathf.Sin(Time.time * 50f) * 50; // Modify the y value
            rectTransform.position = position; // Assign the modified position back
            // -280f + y.val

            horse[0].GetComponent<RectTransform>().position = Vector3.MoveTowards(horse[0].GetComponent<RectTransform>().position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(horse[0].GetComponent<RectTransform>().position, targetPosition) < 0.1f)
            {
                setup = false;
                zOnce();
                //look up
            }
        }

        if (breakHurdle)
        {

            targetPosition = spots[4].position;


            if (timer < 0)
            {
                foreach (GameObject item in horse)
                {
                    item.SetActive(false);
                }
                horse[0].SetActive(true);


                float moveSpeed = 1000f;

                RectTransform rectTransform = horses.GetComponent<RectTransform>();
                Vector3 position = rectTransform.position; // Get the current position
                position.y = Mathf.Sin(Time.time * 50f) * 50; // Modify the y value
                rectTransform.position = position; // Assign the modified position back


                targetHorse.GetComponent<RectTransform>().position = Vector3.MoveTowards(targetHorse.GetComponent<RectTransform>().position, targetPosition, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(targetHorse.GetComponent<RectTransform>().position, targetPosition) < 0.1f)
                {
                    breakHurdle = false;
                    zOnce();
                    //look up
                }
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }

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
                         Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.R) || fillAmount <= 0)
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

            if (count == 1)
            {
                weightBars[0].SetActive(true);
            }
            else if (count == 2)
            {
                weightBars[1].SetActive(true);

            }
            else if (count == 3)
            {
                weightBars[2].SetActive(true);

            }

            if (run)
            {
                fillAmount -= Time.deltaTime * speed;
                // For the shrimp
                RectTransform srectTransform = shrimps.GetComponent<RectTransform>();
                Vector3 sposition = srectTransform.position; // Get the current position
                sposition.y = -134 + Mathf.Sin(Time.time * 80f) * 2; // Modify the y value
                sposition.x = -2 + Mathf.Sin(Time.time * 50f) * 7; // Ensure the x value stays the same
                srectTransform.position = sposition; // Assign the modified position back

                // For the bar
                RectTransform brectTransform = bars.GetComponent<RectTransform>();
                Vector3 bposition = brectTransform.position; // Get the current position
                bposition.y = -73 + Mathf.Sin(Time.time * 100f) * 3; // Modify the y value
                bposition.x = Mathf.Sin(Time.time * 70f) * 7; ; // Ensure the x value stays the same
                brectTransform.position = bposition; // Assign the modified position back


                if (fillAmount < 1 && Input.GetKeyDown(correctKey))
                {
                    fillAmount += 0.2f / count;
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
            {//count < max
                if (track.transform.position.x > -5500)
                {
                    //in cse i want minishrimp again
                    //|| miniShrimp.transform.position.x > 680f
                    //float zRotationOffset = (count % 2 == 0) ? 10 : -10;
                    //Vector3 zrotation = miniShrimp.transform.eulerAngles;
                    //zrotation.z += zRotationOffset;
                    //miniShrimp.transform.eulerAngles = zrotation;

                    //Vector3 position = miniShrimp.transform.position;
                    //position.x += 10;
                    //miniShrimp.transform.position = position;

                    //count++;

                    //track.

                    Vector3 position = track.transform.position;
                    position.x -= 50;
                    track.transform.position = position;

                    KeyGen();

                    Vector3 rotation = shrimp.transform.eulerAngles;
                    // Modify the Z-axis rotation
                    rotation.z -= 60f;

                    // Apply the new rotation
                    shrimp.transform.eulerAngles = rotation;
                }
                else
                {
                    //result.GetComponent<TMP_Text>().text = "shrimpcredible!";

                    int pick = Random.Range(0, wordsList.Count);
                    string randomWord = wordsList[pick]; // Access the randomly picked word
                    displayText.text = randomWord;

                    count = max;
                    waitTime = 3f;
                    StartCoroutine(Again());

                }
            }

            if (count == max)
            {
                displayText.GetComponent<TMP_Text>().text = "u cross";
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
        waitTime = 1.5f;

        if (fillAmount == 1)
        {
            //result.GetComponent<TMP_Text>().text = $"#{count} win";

            foreach (GameObject item in horse)
            {
                item.SetActive(false);
            }
            horse[1].SetActive(true);
            foreach (GameObject item in hurdles)
            {
                item.SetActive(false);
            }

            int pick = Random.Range(0, wordsList.Count);
            string randomWord = wordsList[pick]; // Access the randomly picked word
            displayText.text = randomWord;

            targetHorse = horse[3];
            breakHurdle = true;
            timer = 1;
        }
        else if (fillAmount == 0)
        {
            //result.GetComponent<TMP_Text>().text = "loss";

            foreach (GameObject item in horse)
            {
                item.SetActive(false);
            }
            horse[2].SetActive(true);
            foreach (GameObject item in hurdles)
            {
                item.SetActive(false);
            }
            hurdles[1].SetActive(true);

            targetHorse = horse[2];

            breakHurdle = true;
            timer = 1;


            displayText.text = "ermm..";
        }

        if (targetHorse = horse[2])
        {
            targetHorse.transform.position = spots[3].position;

        }
        else if (targetHorse = horse[3])
        {
            targetHorse.transform.position = spots[2].position;

        }


        StartCoroutine(Again());
    }

    private void SpamResult()
    {
        fillObject.GetComponent<Image>().fillAmount = fillAmount;
        run = false;
        waitTime = 2f;

        Vector3 position = bars.transform.position;
        position.y = 0;
        bars.transform.position = position;

        //turn on tall one
        weightShrimp[0].SetActive(false);
        weightShrimp[1].SetActive(true);

        //result.GetComponent<TMP_Text>().text = $"#{count} win";

        int pick = Random.Range(0, wordsList.Count);
        string randomWord = wordsList[pick]; // Access the randomly picked word
        displayText.text = randomWord + "\n\n";


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

        displayText.GetComponent<TMP_Text>().text = "";
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
        displayText.GetComponent<TMP_Text>().text = "";
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

        //reset bar height
        Vector3 position = bars.transform.position;
        position.x = -73;
        bars.transform.position = position;
        //turn on short shrimp
        weightShrimp[0].SetActive(true);
        weightShrimp[1].SetActive(false);

        popup.SetActive(true);
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
        //result.GetComponent<TMP_Text>().text = "";
        key = 4;
        KeyGen();
    }

    IEnumerator Again()
    {
        yield return new WaitForSeconds(0.5f);
        popup.SetActive(false);


        yield return new WaitForSeconds(waitTime);
        displayText.text = "";

        if (count < max)
        {
            count++;
            if (QT_once)
            {
                foreach (GameObject item in horse)
                {
                    item.SetActive(false);
                }
                horse[0].SetActive(true);
                horses.transform.position = spots[2].position;
                foreach (GameObject item in hurdles)
                {
                    item.SetActive(false);
                }
                hurdles[0].SetActive(true);

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
            //zTurnOff();
            GameObject.Find("SportSwitcher").GetComponent<SceneChanger>().Counter();
        }
    }
}
