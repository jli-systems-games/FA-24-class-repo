using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EggGame : MonoBehaviour
{
    private GameManager _gameManager;

    //GameObjects
    public GameObject egg;
    private SpriteRenderer _eggSprite;
    public Sprite[] eggStates;

    public int eggStatesCount;
    private float timeBetweenStateChange;
    private float timeLimit;
    private bool stoppedCooking;

    //result bools
    public bool didGreat;
    public bool didOk;
    public bool failed;

    private bool pressedButton;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        stoppedCooking = false;

        //StartMicroGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && pressedButton == false) 
        {
            pressedButton = true;
            Debug.Log("pressed Q");
            stoppedCooking = true;
            Debug.Log("stopped cooking: " + stoppedCooking);
            StartCoroutine(cookEgg());
        }
    }

    public void StartMicroGame(int score)
    {
        _eggSprite = egg.GetComponent<SpriteRenderer>();

        pressedButton = false;
        _eggSprite.sprite = eggStates[0];

        didGreat = false;
        didOk = false;
        failed = false;
        eggStatesCount = 0;

        timeLimit = score/1.5f;

        StartCoroutine(cookEgg());
    }

    public IEnumerator cookEgg()
    {
        if (!stoppedCooking && eggStatesCount < eggStates.Length) 
        {
            if(timeLimit <= 2)
            {
                timeBetweenStateChange = Random.Range(.5f, 3- timeLimit);
            }
            else
            {
                timeBetweenStateChange = Random.Range(.5f, 1);
            }
                yield return new WaitForSeconds(timeBetweenStateChange);
                
            if (!pressedButton)
            {
                eggStatesCount++;
                _eggSprite.sprite = eggStates[eggStatesCount];
            }
               
            if (eggStatesCount == eggStates.Length-1)
                {
                    didGreat = false;
                    didOk = false;
                    failed = true;

                    StartCoroutine(_gameManager.Result(didGreat, didOk, failed));
                }

            else
            {
                StartCoroutine(cookEgg());
            }
        }

            else
            {
                if (eggStatesCount <= 2)
                {
                        didGreat = false;
                        didOk = false;
                        failed = true;
                }

                if (eggStatesCount == 3)
                {
                    didGreat = true;
                    didOk = false;
                    failed = false;
                }
                
                if (eggStatesCount == 4 || eggStatesCount == 5)
                {
                        didGreat = false;
                        didOk = true;
                        failed = false;
                }

                StartCoroutine(_gameManager.Result(didGreat, didOk, failed));
            }
    }
}
