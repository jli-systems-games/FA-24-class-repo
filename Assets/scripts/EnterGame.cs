using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterGame : MonoBehaviour
{

    private float enterTimer = 0f;
    public GameObject HoldE;
    public GameObject eTimer;
    public Image timer;
    public float EHoldTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HoldToEnter();
    }

    private void HoldToEnter()
    {
       // if (Time.time - lastSwitchTime < globalSwitchCooldown) return;

        if (Input.GetKey(KeyCode.E))
        {

            HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
            eTimer.SetActive(true);
            enterTimer += Time.deltaTime;

            float progress = enterTimer / EHoldTime;
            timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);

            if (enterTimer >= EHoldTime)
            {
                //eTimer.GetComponent<Image>().alpha = 0f;
                // SwitchControl();
                SceneManager.LoadScene("Spark");
                enterTimer = 0f;
                //lastSwitchTime = Time.time;
            }
        }
        else
        {
            enterTimer = 0f;
            eTimer.SetActive(false);
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 1f;
            timer.GetComponent<Image>().fillAmount = 0f;
        }
    }
}
