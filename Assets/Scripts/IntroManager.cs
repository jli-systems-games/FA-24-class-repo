using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Return/Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            //StartCoroutine(StartGame());
            StartGame();
        }
    }

    void StartGame()
    {
        SceneManager.LoadScene("MiniGame1");
    }
}
