using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Players : MonoBehaviour
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

    public player2 p2;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {   moveDirection = Movement.action.ReadValue<Vector2>();
        
        //Debug.Log(Movement.action.ReadValue<Vector2>());
        moveAround();
        Debug.Log(score);
        if (score == 8)
        {

            gameEnd = true;

        }
        if (gameEnd || p2.gameEnd)
        {
            finalPoints.text = score.ToString("");
        }
    }
  

    void moveAround()
    {
        move = new Vector3(0,moveDirection.y,0 ) * moveSpeed;
        
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
