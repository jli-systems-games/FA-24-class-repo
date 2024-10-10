using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void ZSMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ZSBackyard()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ZSRolling()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ZSDecorate()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
