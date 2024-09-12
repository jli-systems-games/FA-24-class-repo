using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CherryGame : MonoBehaviour
{
    private GameManager _gameManager;

    //Cherry Mini-Game GameObjects
    public GameObject iceCreamBowl;
    public GameObject cherryPrefab;
    public List<GameObject> cherries = new List<GameObject>();

    public int cherryScore;
    private int cherryNum;
    public int cherriesDropped;

    public Timer cherryTimer;
    private bool wrapUpStarted;

    //result bools
    public bool didGreat;
    public bool didOk;
    public bool failed;


    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        wrapUpStarted = false;
        cherriesDropped = 0;

        //StartMicroGame(GameManager.score);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cherryTimer.timer == 0 && wrapUpStarted == false)
        {
            wrapUpStarted = true;
            EndMicroGame();
        }
    }
    public void StartMicroGame(int score)
    {
        for (int i = 0; i < cherries.Count; i++)
        {
            Destroy(cherries[i]);
        }

        cherries.Clear();

        cherryScore = 0;
        cherriesDropped = 0;
        wrapUpStarted = false;

        if (score < 3) 
        {
            cherryNum = 1;
        }
        if (score >= 3) 
        {
            cherryNum++;
        }

        for (int i = 0; i < cherryNum; i++)
        {
            CreateCherries();
        }

        cherryTimer.timer = 5;
        cherryTimer.timeMeter.maxValue = 5;
    }

    public void CreateCherries()
    {
        Vector3 cherryPos = new Vector3(Random.Range(-4, 7), Random.Range(2, 4), 0);
        GameObject newCherry = Instantiate(cherryPrefab, cherryPos, Quaternion.identity, transform);
        cherries.Add(newCherry);
    }

    public void CountCherry()
    {
        cherriesDropped++;

        Debug.Log("Cherries Dropped: " + cherriesDropped + "+ Cherry Count: " + cherries.Count);

        if(cherriesDropped == cherries.Count)
        {
            wrapUpStarted = true;
            EndMicroGame();
        }
    }

    public void EndMicroGame()
    {

        if (cherryScore == cherries.Count)
        {
            didGreat = true;
            didOk = false;
            failed = false;
        }

        else if (cherryScore > 0)
        {
            didGreat = false;
            didOk = true;
            failed = false;
        }

        else 
        {
            didGreat= false;
            didOk = false;
            failed = true;
        }

        wrapUpStarted = false;

        StartCoroutine(_gameManager.Result(didGreat, didOk, failed));
    }

    public void ChangeState(GameState newState)
    {

    }
}
