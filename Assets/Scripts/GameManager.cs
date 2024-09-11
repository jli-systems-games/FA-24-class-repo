using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timePerGame = 10f; // Duration for each mini-game
    private float timer;
    private int currentGame = 0; // Index for the current mini-game
    private string[] miniGames = { "MiniGame1", "MiniGame2", "MiniGame3" }; // Scene names for mini-games

    // Start is called before the first frame update
    void Start()
    {
        timer = timePerGame;
        LoadMiniGame(currentGame);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            currentGame = (currentGame + 1) % miniGames.Length; // Cycle through mini-games
            LoadMiniGame(currentGame);
            timer = timePerGame; // Reset the timer for the next mini-game
        }
    }

    void LoadMiniGame(int gameIndex)
    {
        SceneManager.LoadScene(miniGames[gameIndex]);
    }
}
