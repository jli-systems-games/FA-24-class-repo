using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public int clearedGames;
    public string[] games;
    public string randomGame;

    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<gameManager>().Length; i++)
        {
            if(Object.FindObjectsOfType<gameManager>()[i] != this)
            {
                if(Object.FindObjectsOfType<gameManager>()[i].name == gameObject.name)
                {
                Destroy(gameObject);
                }
            }
        }

        
        DontDestroyOnLoad(this.gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (string sceneName in games)
        {
            if (scene.name == sceneName)
            {
                clearedGames++;
            }
        }
        
    }

    public void StartGame()
    {
        randomGame = games[Random.Range(0, games.Length)];
        SceneManager.LoadScene(randomGame);
    }


    void Update()
    {
        
    }
}
