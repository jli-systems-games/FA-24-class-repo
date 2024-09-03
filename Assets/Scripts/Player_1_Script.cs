using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1_Script : MonoBehaviour
{
    public float speed;
    private Vector3 playerPos;
    [SerializeField] private GameObject otherPlayer;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
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
                Sabotage();
            }
        }
        if (this.gameObject.CompareTag("P2"))
        {
            MovementP2();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sabotage();
            }
        }
    }

     public void MovementP1()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerPos += new Vector3(0, speed, 0) * Time.deltaTime;
            Debug.Log(playerPos);
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            playerPos += new Vector3(0, -speed, 0) * Time.deltaTime;
        }
    }

    public void MovementP2()
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

     public void Sabotage()
    {
        
    }
}
