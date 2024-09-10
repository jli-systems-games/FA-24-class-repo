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
    public TextMeshProUGUI outcome;

    public int cherryScore;
    private int cherryNum;
    public int cherriesDropped;

    private Timer cherryTimer;
    private bool wrapUpStarted;

    // Start is called before the first frame update
    private void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        cherryTimer = FindObjectOfType<Timer>();
        wrapUpStarted = false;
        cherriesDropped = 0;
        outcome.enabled = false;

        StartMicroGame(GameManager.score);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cherryTimer.timer == 0 && wrapUpStarted == false)
        {
            StartCoroutine(EndMicroGame());
        }

        if(cherriesDropped == cherryNum && wrapUpStarted == false)
        {
            StartCoroutine(EndMicroGame());
        }
    }
    public void StartMicroGame(int score)
    {
        cherryScore = 0;

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
    }

    public void CreateCherries()
    {
        Vector3 cherryPos = new Vector3(Random.Range(-4, 7), Random.Range(2, 4), 0);
        GameObject newCherry = Instantiate(cherryPrefab, cherryPos, Quaternion.identity);
        cherries.Add(newCherry);
    }

    public IEnumerator EndMicroGame()
    {
        wrapUpStarted = true;

        if (cherryScore == cherryNum)
        {
            outcome.text = "Great!";
        }

        else if (cherryScore > 0)
        {
            outcome.text = "Okay!";
        }

        else 
        {
            outcome.text = "You Suck!";
        }

        outcome.enabled = true;

        yield return new WaitForSeconds(2);

        for (int i = 0; i < cherries.Count; i++)
        {
            Destroy(cherries[i]);
        }

        cherries.Clear();

        outcome.enabled = false;

        wrapUpStarted = false;

        gameObject.SetActive(false);
    }

    public void ChangeState(GameState newState)
    {

    }
}
