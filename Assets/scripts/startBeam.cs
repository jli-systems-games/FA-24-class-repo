using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startBeam : MonoBehaviour
{
    public GameObject lightBeamGObj;

    public void startLight()
    {
        lightBeamGObj.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToLevelSelection()
    {
        SceneManager.LoadScene("LevelLoader");
    }

    public void nextLevel(string levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
