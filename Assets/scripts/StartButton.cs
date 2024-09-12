using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //public GameObject Rstop;
    //public GameObject Rwalk;

    void Start()
    {
        //Rwalk.SetActive(true);
       // Rstop.SetActive(false);
    }

    public void PlayGame()
    {
        //Rwalk.SetActive(false);
        //Rstop.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     
    }
}
