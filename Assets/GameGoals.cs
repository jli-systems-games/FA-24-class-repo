using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameGoals : MonoBehaviour
{
    private bool room1Begin;
    private bool room2Begin;
    private bool room3Begin;

    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;

    //2 floors
    public List<GameObject> walls = new();
    public List<GameObject> walls2 = new();

    //DISPLAY SCORE

    public static int score = 0;
    public GameObject csText;

    public GameObject player;
    public GameObject board;

    //SPAWN GOALS
    private int random;
    private int prev;

    public GameObject goalPrefab;

    public List<Transform> goalLocations = new();

    private bool spawning;
    private GameObject currentGoal;


    //TIMER

    public float timeLeft;
    public GameObject tText;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        spawning = false;
        room1Begin = false;
        room2Begin = false;
        room3Begin = false;
        //reset bools when restarting game

        cam1.SetActive(true);
        cam2.SetActive(false);
        cam3.SetActive(false);
    }

    void Update()
    {
        DisplayScore();
        DisplayTime();

        if (player.transform.position.y < -10f && !room2Begin)
        {
            TimerBegin();
            SpawnGoal();

            room2Begin = true;

            cam1.SetActive(false);
            cam2.SetActive(true);

            foreach (GameObject item in walls)
            {
                item.SetActive(true);
            }
        }

        if (player.transform.position.y < -32f && !room3Begin)
        {
            cam2.SetActive(false);
            cam3.SetActive(true);
            room3Begin = true;

            foreach (GameObject item in walls2)
            {
                item.SetActive(true);
            }
        }
    }


    //SCORE
    void DisplayScore()
    {
        csText.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    //GOALS
    public void SpawnGoal()
    {
        if (!spawning) return;

        if (currentGoal != null)
        {
            Destroy(currentGoal);
        }

        do
        {
            random = Random.Range(0, goalLocations.Count);
        } while (random == prev);

        prev = random;

        currentGoal = Instantiate(goalPrefab, goalLocations[random].position, Quaternion.identity);
    }



    //TIMER
    public void DisplayTime()
    {
        tText.GetComponent<TextMeshProUGUI>().text = timeLeft.ToString();
    }

    public void TimerBegin()
    {
        StartCoroutine(StartTimer());

        spawning = true;
    }

    //TIMER
    public IEnumerator StartTimer(float time = 60)
    {
        timeLeft = time;

        while (timeLeft > 0)
        {
            Debug.Log(timeLeft);
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        spawning = false; // Stop spawning when the timer ends

        if (currentGoal != null)
        {
            Destroy(currentGoal);
        }

        foreach (GameObject item in walls2)
        {
            item.SetActive(false);
        }

        board.GetComponent<Leaderboard>().GetScore();
    }


    //entrance

    public void Entrance()
    {
        foreach (GameObject item in walls)
        {
            item.SetActive(false);
        }
    }

    public void Closure()
    {
        foreach (GameObject item in walls)
        {
            item.SetActive(true);
        }
    }
}