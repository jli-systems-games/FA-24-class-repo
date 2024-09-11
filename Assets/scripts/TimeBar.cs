using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public GameObject TimeOut_Textholder;
    public Image timerImage;

    float timeRemaining;
    public float maxTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        timeRemaining = maxTime;
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
            TimeOut_Textholder.SetActive(true);

        }
    }
}
