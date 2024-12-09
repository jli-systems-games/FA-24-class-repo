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
    public float EHoldTime = 10f;
    public GameObject Spark;

    public GameObject audioSource;

    private bool canHoldToEnter = false;

    void Start()
    {
        Spark.SetActive(false);
        eTimer.SetActive(false);
        audioSource.SetActive(false);
        StartCoroutine(EnableHoldToEnterAfterDelay());
    }

    void Update()
    {
        if (canHoldToEnter)
        {
            HoldToEnter();
        }
    }

    private IEnumerator EnableHoldToEnterAfterDelay()
    {
        yield return new WaitForSeconds(1f); 
        canHoldToEnter = true;
    }

    private void HoldToEnter()
    {
        if (Input.GetKey(KeyCode.E))
        {
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
            eTimer.SetActive(true);
            audioSource.SetActive(true);
            enterTimer += Time.deltaTime;

            float progress = enterTimer / EHoldTime;
            timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);
            Spark.SetActive(true);

            if (enterTimer >= EHoldTime)
            {
                SceneManager.LoadScene("Spark");
                enterTimer = 0f;
            }
        }
        else
        {
            Spark.SetActive(false);
            enterTimer = 0f;
            eTimer.SetActive(false);
            audioSource.SetActive(false);
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 1f;
            timer.GetComponent<Image>().fillAmount = 0f;
        }
    }
}
