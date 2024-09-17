using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // don't want object to be destroyed while loading new scene
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); // loads next scene using build index
    }

    public void LoadScene(string sceneName) // uses string parameter to load the scene we want using scene name
    {
        SceneManager.LoadSceneAsync(sceneName);
    }
}
