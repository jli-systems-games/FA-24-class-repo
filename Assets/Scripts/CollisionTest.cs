using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
        ResetGame();
    }

    void ResetGame()
    {
        // Reload the current scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
