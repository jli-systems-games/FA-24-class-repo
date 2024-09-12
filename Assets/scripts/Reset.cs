using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    //private GameManager _gameManager;
    //_gameManager.gameSManager.topAllCoroutines();

    private void Start()
    {
        StopAllCoroutines();

    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Start");
    }

    
}
