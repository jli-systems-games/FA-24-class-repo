using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1_Script : MonoBehaviour
{
    public float speed;
    private Vector3 playerPos;
    [SerializeField] private GameObject otherPlayer;

    private bool invertedLeft;
    private bool invertedRight;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
        invertedLeft = false;
        invertedRight = false;
        //Debug.Log(playerPos);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos;
            if (this.gameObject.CompareTag("P1"))
            {
                MovementP1();
                if (Input.GetKey(KeyCode.RightShift))
                {
                    Sabotage("right");
                }
            }
            if (this.gameObject.CompareTag("P2"))
            {
                MovementP2();
                if (Input.GetKey(KeyCode.LeftShift))
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
        if(side == "left")
        {
            StartCoroutine(InvertControlsLeft());
        }

        if (side == "right") 
        {
            StartCoroutine(InvertControlsRight());
        }
    }

    public IEnumerator InvertControlsLeft()
    {
        invertedLeft = true;
        Debug.Log("left controls inverted: " + invertedLeft);

        yield return new WaitForSeconds(5);

        invertedLeft = false;
        Debug.Log("left controls inverted: " + invertedLeft);
    }

    public IEnumerator InvertControlsRight()
    {
        invertedRight = true;
        Debug.Log ("right controls inverted: " + invertedRight);
        yield return new WaitForSeconds(5);
        invertedRight = false;
    }
}
