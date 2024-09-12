using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    //CHANGNG GAME
    // pick the next game
    public float nextGame;

    public void Start()
    {
        StartCoroutine(NextGame());
    }

    private IEnumerator NextGame()
    {
        yield return new WaitForSeconds(1f);

        PickGame();
    }

    public void PickGame()
    {
        nextGame = Random.Range(1, 7);

        if (nextGame == 6)
        {
            SceneManager.LoadScene(6, LoadSceneMode.Single);
        }
        else if (nextGame == 1)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
        else if (nextGame == 2)
        {
            SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
        else if (nextGame == 3)
        {
            SceneManager.LoadScene(3, LoadSceneMode.Single);
        }
        else if (nextGame == 4)
        {
            SceneManager.LoadScene(4, LoadSceneMode.Single);
        }
        else if (nextGame == 5)
        {
            SceneManager.LoadScene(5, LoadSceneMode.Single);
        }
    }

}
