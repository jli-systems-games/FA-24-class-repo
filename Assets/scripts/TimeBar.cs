using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using TMPro;

public class TimeBar : MonoBehaviour
{
    public GameObject TimeOut_Textholder;
    public Image timerImage;
    //public GameObject Lose;

    //private GameManager _gameManager;
    //public bool TimeOut;

    private float timeRemaining;
    public float maxTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //TimeOut = false;

        timeRemaining = maxTime;
        /*_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Lose.SetActive(false);

        if (Lose == null)
        {
            Lose = GameObject.Find("Lose"); // 确保名字与场景中的对象一致
        }

        if (Lose != null)
        {
            Lose.SetActive(false);
        }
        else
        {
            Debug.LogError("Lose object not found in the scene.");
        }*/
    }

    // Update is called once per frame

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerImage.fillAmount = timeRemaining / maxTime;
        }
        else
        {
            //_gameManager.currentGameIndex++;
            TimeOut_Textholder.SetActive(true);
            //Lose.SetActive(true);

            //StartCoroutine(ShowLose());

        }
    }

    IEnumerator ShowLose()
    {
       // Lose.SetActive(true);
        yield return new WaitForSeconds(0.5f);
       // _gameManager.ChangeState(GameState.Transition);
    }
}
