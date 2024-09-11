using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggGame : MonoBehaviour
{
    private GameManager _gameManager;

    //GameObjects
    public GameObject egg;
    private SpriteRenderer _eggSprite;
    public Sprite[] eggStates;

    private float timeBetweenStateChange;
    private bool stoppedCooking;

    //result bools
    public bool didGreat;
    public bool didOk;
    public bool failed;

    public GameObject qIndic;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _eggSprite = egg.GetComponent<SpriteRenderer>();
        stoppedCooking = false;
        qIndic.SetActive(false);

        StartMicroGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Debug.Log("pressed Q");
            stoppedCooking = true;
            Debug.Log("stopped cooking: " + stoppedCooking);
        }
    }

    public void StartMicroGame()
    {
        _eggSprite.sprite = eggStates[0];
        qIndic.SetActive(true);

        didGreat = false;
        didOk = false;
        failed = false;

        StartCoroutine(cookEgg());
    }

    public IEnumerator cookEgg()
    {
        for (int i = 0; i < (eggStates.Length); i++)
        {
            if (!stoppedCooking) 
            {
                timeBetweenStateChange = Random.Range(.5f, 3);
                yield return new WaitForSeconds(timeBetweenStateChange);

                _eggSprite.sprite = eggStates[i];

                if (i == eggStates.Length - 1)
                {
                    didGreat = false;
                    didOk = false;
                    failed = true;

                    StartCoroutine(_gameManager.Result(didGreat, didOk, failed));
                }
            }

            else
            {
                if (i <= 2)
                {
                        didGreat = false;
                        didOk = false;
                        failed = true;
                }

                if (i == 3)
                {
                    didGreat = true;
                    didOk = false;
                    failed = false;
                }
                
                if (i == 4 || i == 5)
                {
                        didGreat = false;
                        didOk = true;
                        failed = false;
                }

                qIndic.SetActive(false);
                StartCoroutine(_gameManager.Result(didGreat, didOk, failed));
            }
        }
    }
}
