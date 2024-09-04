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
    public float moveSpeed = 0.5f;
    public Rigidbody rb;
    int score;
    public AudioSource ping;
    public bool gameEnd = false;
    public TMP_Text finalPoints;

    public Players p1;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveDirection = Movement.action.ReadValue<Vector2>();

        //Debug.Log(Movement.action.ReadValue<Vector2>());
        characterMove();
        Debug.Log(score);
        if (score == 5)
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
        move = new Vector3(0, moveDirection.y, 0) * moveSpeed;

        rb.MovePosition(rb.position + move);
    }
    void OnCollisionEnter(Collision collision)
    {
        ping.Play();
        if (collision.collider.CompareTag("Right"))
        {
            score++;
        }
        else
        {
            score--;
        }
    }
}
