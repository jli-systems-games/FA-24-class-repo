using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    public GameObject canvas;
    public GameObject game;
    public GameObject startScreen;

    private void OnTriggerEnter2D(Collider2D collision)
	{
		canvas.gameObject.SetActive(true);
	}

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startGame()
    {
        game.gameObject.SetActive(true);
        startScreen.gameObject.SetActive(false);
    }
}
