using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class paddlePoint : MonoBehaviour
{
    bool isCurrentlyColliding;
    public float pointScore;


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player3")
        {
            isCurrentlyColliding = true;
            pointScore += 1;
            RestartScene();
            //reset game
        }
    }

    void OnTriggerExit(Collider other)
    {
        isCurrentlyColliding = false;
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    

    void FixedUpdate()
    {
        Debug.Log(pointScore);
    }

}
