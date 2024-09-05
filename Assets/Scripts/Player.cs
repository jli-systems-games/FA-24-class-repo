using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Vector3 playerPos;
    [SerializeField] private GameObject otherPlayer;

    public bool invertedLeft;
    public bool invertedRight;

    private int sabotageChance;
    public bool sabotagable;

    public int score;

    [SerializeField] private TextMeshProUGUI scoreboard;

    [SerializeField] private AudioSource siren;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
        invertedLeft = false;
        invertedRight = false;
        sabotagable = true;
        //Debug.Log(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos;

        scoreboard.text = score.ToString();
            if (this.gameObject.CompareTag("P1"))
            {
                MovementP1();
                if (Input.GetKeyDown(KeyCode.RightShift) && otherPlayer.GetComponent<Player>().sabotagable)
                {
                    Sabotage("right");
                }
            }
            if (this.gameObject.CompareTag("P2"))
            {
                MovementP2();
                if (Input.GetKeyDown(KeyCode.LeftShift) && otherPlayer.GetComponent<Player>().sabotagable)
                {
                    Sabotage("left");
                }
            }
    }

     public void MovementP1()
    {
        if (invertedLeft) 
        {
            Debug.Log("left controls inverted");
            if (Input.GetKey(KeyCode.W))
            {
                playerPos += new Vector3(0, -speed, 0) * Time.deltaTime;
            }


            if (Input.GetKey(KeyCode.S))
            {
                playerPos += new Vector3(0, speed, 0) * Time.deltaTime;
            }
        }

        else if (!invertedLeft)
        {
            if (Input.GetKey(KeyCode.W))
            {
                playerPos += new Vector3(0, speed, 0) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                playerPos += new Vector3(0, -speed, 0) * Time.deltaTime;
            }
        }
    }

    public void MovementP2()
    {
        if (invertedRight)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerPos += new Vector3(0, -speed, 0) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerPos += new Vector3(0, speed, 0) * Time.deltaTime;
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                playerPos += new Vector3(0, speed, 0) * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                playerPos += new Vector3(0, -speed, 0) * Time.deltaTime;
            }
        }
    }

     public void Sabotage(string side)
    {
        Debug.Log("sabotaged!");
        sabotageChance = Random.Range(1, 4);

        siren.Play();

        Debug.Log("Sabotage Chance: " + sabotageChance);

        otherPlayer.GetComponent<Player>().sabotagable = false;

        if (sabotageChance == 1)
        {
            if (side == "left")
            {
                StartCoroutine(InvertControlsLeft());
            }

            if (side == "right")
            {
                StartCoroutine(InvertControlsRight());
            }
        }

        if (sabotageChance == 2)
        {
            StartCoroutine(SlowOpponent());
        }

        if(sabotageChance == 3)
        {
            StartCoroutine(DeactivateOpp());
        }
    }

    public IEnumerator InvertControlsLeft()
    {
        otherPlayer.GetComponent<Player>().invertedLeft = true;
        //Debug.Log("left controls inverted: " + invertedLeft);

        yield return new WaitForSeconds(5);

        otherPlayer.GetComponent<Player>().invertedLeft = false;
        //Debug.Log("left controls inverted: " + invertedLeft);

        yield return new WaitForSeconds(5);

        otherPlayer.GetComponent<Player>().sabotagable = true;
    }

    public IEnumerator InvertControlsRight()
    {
        otherPlayer.GetComponent<Player>().invertedRight = true;
        Debug.Log ("right controls inverted: " + invertedRight);
        yield return new WaitForSeconds(5);
        otherPlayer.GetComponent<Player>().invertedRight = false;

        yield return new WaitForSeconds(5);

        otherPlayer.GetComponent<Player>().sabotagable = true;
    }

    public IEnumerator SlowOpponent()
    {
        float oppSpeed = otherPlayer.GetComponent<Player>().speed;
        otherPlayer.GetComponent<Player>().speed = .5f;

        yield return new WaitForSeconds(5);

        otherPlayer.GetComponent<Player>().speed = oppSpeed;

        yield return new WaitForSeconds(5);

        otherPlayer.GetComponent<Player>().sabotagable = true;
    }

    public IEnumerator DeactivateOpp()
    {
        otherPlayer.GetComponent<SpriteRenderer>().enabled = false;
        otherPlayer.GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(2);

        otherPlayer.GetComponent<SpriteRenderer>().enabled = true;
        otherPlayer.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(5);

        otherPlayer.GetComponent<Player>().sabotagable = true;
    }
}
