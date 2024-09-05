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
    public Vector3 p1Position;
    public float moveSpeed = 0.5f;
    public Rigidbody rb;
    int score;
    public AudioSource ping;
    public bool gameEnd = false;
    public TMP_Text finalPoints;
    float ogXPosition;
    Enemy gn;

    public player2 p2;
    public GameObject gnome;
    // Start is called before the first frame update
    void Start()
    {

        ogXPosition = transform.position.x;
        gn = gnome.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   moveDirection = Movement.action.ReadValue<Vector2>();
        
       
        moveAround();
        p1Position = transform.position;
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
        

        if(gnome.activeSelf == true)
        {
            move = new Vector3(moveDirection.x, moveDirection.y, 0) * moveSpeed;
        }
        else
        {
            transform.position = new Vector3(ogXPosition, transform.position.y, 0);
            move = new Vector3(0,moveDirection.y,0 ) * moveSpeed;

        }

        
        rb.MovePosition(rb.position + move);
    }
    void OnCollisionEnter(Collision collision)
    {
        ping.Play();
        if (collision.collider.CompareTag("Right"))
        {
            
            if(gnome.activeSelf == true)
            {
                gn.isCaughtByPlayer = true;
                gn.hasCaughtBall = false;
            }
            else
            {   
                score++;

            }
           
            Debug.Log(gn.isCaughtByPlayer);
         
        }
        else
        {
            score--;
        }
    }

}
