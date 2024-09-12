using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class timerGame2 : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] public float remainingTime;

    public GameObject script;
    public buttonGame buttongame;

    void Update()
    {

        GameObject script = GameObject.FindWithTag("script");
        buttongame = script.GetComponent<buttonGame>();

        
        if(buttongame.timerException == false)
        {
            if (remainingTime <= 0)
            {
                SceneManager.LoadScene("gameOver");
            }
        }


        remainingTime -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}