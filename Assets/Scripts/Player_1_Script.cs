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
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (this.gameObject.CompareTag("P1"))
        {
            if (Input.GetKey(KeyCode.RightShift))
            {
                Sabotage();
            }
        }
        if (this.gameObject.CompareTag("P2"))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Sabotage();
            }
        }
    }

     public void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            playerPos += new Vector3(0, speed, 0);
        }

        if (Input.GetKey(KeyCode.S)) 
        {
            playerPos += new Vector3(0, -speed, 0);
        }
    }

     public void Sabotage()
    {
        
    }
}
