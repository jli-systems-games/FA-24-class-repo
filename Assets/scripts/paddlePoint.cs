using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class paddlePoint : MonoBehaviour
{
    public float pointScore;

    void Start()
    {
        Physics.IgnoreLayerCollision(3, 4);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player3")
        {
            pointScore += 1;
            RestartScene();
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
