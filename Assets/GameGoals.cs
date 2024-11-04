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
    public List<GameObject> walls3 = new();

    //DISPLAY SCORE

    public static int score = 0;
    public static int highscore = 0;

    public GameObject csText;
    public GameObject hsText;

    public GameObject player;
    //public GameObject board;

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
        BeginRoom3();
        highscore = 0;
    }

    private void BeginRoom3()
    {
        score = 0;

        spawning = false;
        room1Begin = true;
        room2Begin = false;
        room3Begin = false;
        //reset bools when restarting game

        //cam1.SetActive(true);
        //cam2.SetActive(false);
        //cam3.SetActive(false);
    }

    void Update()
    {
        DisplayScore();
        HighScore();
        DisplayTime();

        //player fals into room 2
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

        //player falls into room 3
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

        //player plays again
        if (player.transform.position.y < -51f)
        {
            player.transform.position = new Vector3(-12.61f, 6f, 0f);
            room1Begin = false;

        }

        if (player.transform.position.y < 4f && !room1Begin)
        {
            BeginRoom3();
            cam1.SetActive(true);
            cam3.SetActive(false);
            room1Begin = true;
            foreach (GameObject item in walls3)
            {
                item.SetActive(true);
            }
        }
    }


    //SCORE
    void DisplayScore()
    {
        csText.GetComponent<TextMeshProUGUI>().text = score.ToString();
        hsText.GetComponent<TextMeshProUGUI>().text = highscore.ToString();
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

        //board.GetComponent<Leaderboard>().GetScore();
    }

    private void HighScore()
    {
        if (score > highscore)
        {
            highscore = score;
        }
        //else
        //{
        //    highscore = highscore;
        //}
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

    //play again
    public void PlayAgain()
    {
        foreach (GameObject item in walls3)
        {
            item.SetActive(false);
        }
    }
}