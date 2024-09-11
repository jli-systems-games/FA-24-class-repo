using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhippedCream : MonoBehaviour
{
    private GameManager _gameManager;
    public Timer _timer;

    //GameObjects
    public GameObject whisk;
    public GameObject whippingCream;

    public Sprite unwhippedCream;
    public Sprite whippedCream;

    public Animator whiskAnim;

    private float whipRate;
    private float numofWhips;
    private int numofRotations;
    private float initialTimer;

    //result bools
    public bool didGreat;
    public bool didOK;
    public bool failed;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //StartMicroGame(GameManager.score);
        whiskAnim = whisk.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            numofWhips++;
        }
    }

    public void ChangeState(GameState newState)
    {

    }

    public void StartMicroGame(int score)
    {
        whippingCream.GetComponent<SpriteRenderer>().sprite = unwhippedCream;
        whippingCream.GetComponent<SpriteRenderer>().flipY = false;
        numofRotations = 0;

        if ((score / 3) <= 10)
        {
            initialTimer = 15 - score / 3;
        }

        else
        {
            initialTimer = 5;
        }

        _timer.timer = initialTimer;

        StartCoroutine(whipCream());
    }

    public IEnumerator whipCream()
    {
        numofWhips = 0;

        yield return new WaitForSeconds(1.5f);

        whipRate = (numofWhips / 1.5f);

        Debug.Log("whipping rate: " + whipRate);

        if (whipRate > 0 && whipRate <= 2)
        {
            whiskAnim.speed = whipRate / 2;
        }

        else if (whipRate > 2 && whipRate <= 3) 
        {
            whiskAnim.speed = 1;
        }

        else if(whipRate > 3)
        {
            whiskAnim.speed = whipRate / 2;
        }

        if(whipRate > 0)
        {
            whiskAnim.enabled = true;
            whiskAnim.Play("whisk");
        }
        else
        {
            whiskAnim.enabled = false;
        }

        yield return new WaitForSeconds(whiskAnim.GetCurrentAnimatorClipInfo(0).Length);

        if (whipRate > 0) 
        {
            numofRotations++;
        }

        if (whipRate >= 6)
        {
            whippingCream.GetComponent<SpriteRenderer>().flipY = true;
            StartCoroutine(EndGame(true));
        }

        else if(numofRotations <= 3)
        {
            StartCoroutine(whipCream());
        }

        else
        {
            whiskAnim.enabled = false;
            StartCoroutine(EndGame(false));
        }
    }

    private IEnumerator EndGame(bool flippedOver)
    {
        if (flippedOver || _timer.timer <= 0)
        {
            didGreat = false;
            didOK = false;
            failed = true;
        }

        else if(_timer.timer > (initialTimer/2))
        {
            didGreat = true;
            didOK = false;
            failed = false;

            whippingCream.GetComponent<SpriteRenderer>().sprite = whippedCream;
        }

        else if (_timer.timer > 0 && _timer.timer <= (initialTimer/2))
        {
            didGreat = false;
            didOK = true;
            failed = false;

            whippingCream.GetComponent<SpriteRenderer>().sprite = whippedCream;
        }

        yield return new WaitForSeconds(1);

        StartCoroutine(_gameManager.Result(didGreat,didOK,failed));
    }
}
