using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class player2 : MonoBehaviour
{
    public InputActionReference Movement;

    Vector3 moveDirection = Vector3.zero;
    Vector3 move = Vector3.zero;
    public Vector3 p2Position;
    public float moveSpeed = 0.5f;
    public Rigidbody rb;
    int score;
    public AudioSource ping;
    public bool gameEnd = false;
    public TMP_Text finalPoints;
    float ogXPosition;
    Enemy gn;

    public Players p1;
    public GameObject gnome;
    // Start is called before the first frame update
    void Start()
    {

        ogXPosition = transform.position.x;
        gn = gnome.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = Movement.action.ReadValue<Vector2>();
        

       
        characterMove();
        //Debug.Log(score);
        p2Position = transform.position;
        if (score == 8)
        {

            gameEnd = true;
        
        }
        if (gameEnd || p1.gameEnd)
        {
            Debug.Log("gameend!");
            finalPoints.text = score.ToString("");
        }
    }


    void characterMove()
    {
       
        if (gnome.activeSelf == true)
        {
            move = new Vector3(moveDirection.x, moveDirection.y, 0) * moveSpeed;
        }
        else
        {
            transform.position = new Vector3(ogXPosition, transform.position.y, 0);
            move = new Vector3(0, moveDirection.y, 0) * moveSpeed;

        }

        rb.MovePosition(rb.position + move);
    }
    void OnCollisionEnter(Collision collision)
    {
        ping.Play();
        if (collision.collider.CompareTag("Right"))
        {
            score++;
            if (gnome.activeSelf == true)
            {
                gn.isCaughtByPlayer = true;
                gn.hasCaughtBall = false;
            }
            Debug.Log(gn.isCaughtByPlayer);

        }
        else
        {
            score--;
        }
    }

}
