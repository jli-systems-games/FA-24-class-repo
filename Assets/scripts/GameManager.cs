using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<BuckController> controllers = new List<BuckController>();

    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    public bool isGameOver = false;
    public GameObject BackToStart;

    private float enterTimer = 0f;
    public GameObject HoldE;
    public GameObject eTimer;
    public Image timer;
    public float EHoldTime = 5f;

    public float switchTimer = 0f;
    //public GameObject Spark;

    public void Start()
    {
        BackToStart.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
        }
        else
        {
            HoldToRestart();
        }
    }

    public void RegisterController(BuckController controller)
    {
        if (!controllers.Contains(controller))
        {
            controllers.Add(controller);
        }
    }

    public void UnregisterController(BuckController controller)
    {
        if (controllers.Contains(controller))
        {
            controllers.Remove(controller);
        }
    }

    public void CheckGameOver()
    {
        foreach (var controller in controllers)
        {
            if (controller.isActive)
            {
                return;
            }
        }

        GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! No active controllers left.");

        StartCoroutine(MoveTimerToCenter());
        StartCoroutine(ShowBackToStartWithDelay());
    }

    private System.Collections.IEnumerator ShowBackToStartWithDelay()
    {
        yield return new WaitForSeconds(1f); 
        BackToStart.SetActive(true);
    }
    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = elapsedTime.ToString("F2");
    }

    private System.Collections.IEnumerator MoveTimerToCenter()
    {
        RectTransform rectTransform = timerText.rectTransform;

        Vector3 startPosition = rectTransform.anchoredPosition;
        Vector3 targetPosition = new Vector3(0, 0, 0);

        Vector3 startScale = rectTransform.localScale;
        Vector3 targetScale = new Vector3(2, 2, 2);

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);

            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
        rectTransform.localScale = targetScale;
    }

    private void HoldToRestart()
    {
        if (Input.GetKey(KeyCode.E))
        {
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
            eTimer.SetActive(true);
            enterTimer += Time.deltaTime;

            float progress = enterTimer / EHoldTime;
            timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);

            if (enterTimer >= EHoldTime)
            {
                SceneManager.LoadScene("Enter");
                enterTimer = 0f;
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

/*
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<BuckController> controllers = new List<BuckController>();

    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isGameOver = false;
    public GameObject BackToStart;

    private float enterTimer = 0f;
    public GameObject HoldE;
    public GameObject eTimer;
    public Image timer;
    public float EHoldTime = 5f;
    //public GameObject Spark;

    public void Start()
    {
        BackToStart.SetActive(false);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTimer();
            //HoldToRestart();
        }
    }

    public void RegisterController(BuckController controller)
    {
        if (!controllers.Contains(controller))
        {
            controllers.Add(controller);
        }
    }

    public void UnregisterController(BuckController controller)
    {
        if (controllers.Contains(controller))
        {
            controllers.Remove(controller);
        }
    }

    public void CheckGameOver()
    {
        foreach (var controller in controllers)
        {
            if (controller.isActive)
            {
                return;
            }
        }

        GameOver();
    }

    private void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over! No active controllers left.");

        StartCoroutine(MoveTimerToCenter());
        BackToStart.SetActive(true);

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E");
            HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
            eTimer.SetActive(true);
            enterTimer += Time.deltaTime;

            float progress = enterTimer / EHoldTime;
            timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);
            //Spark.SetActive(true);

            if (enterTimer >= EHoldTime)
            {

                SceneManager.LoadScene("Enter");
                enterTimer = 0f;

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

    private void UpdateTimer()
    {
        elapsedTime += Time.deltaTime;
        timerText.text = elapsedTime.ToString("F2");
    }

    private System.Collections.IEnumerator MoveTimerToCenter()
    {
        RectTransform rectTransform = timerText.rectTransform;

        Vector3 startPosition = rectTransform.anchoredPosition;
        Vector3 targetPosition = new Vector3(0, 0, 0);

        Vector3 startScale = rectTransform.localScale;
        Vector3 targetScale = new Vector3(2, 2, 2);

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, t);

            rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
        rectTransform.localScale = targetScale;
    }

    /* private void HoldToRestart()
     {
         // if (Time.time - lastSwitchTime < globalSwitchCooldown) return;

         if (Input.GetKey(KeyCode.E))
         {
             Debug.Log("E");
             HoldE.GetComponent<TextMeshProUGUI>().alpha = 0f;
             eTimer.SetActive(true);
             enterTimer += Time.deltaTime;

             float progress = enterTimer / EHoldTime;
             timer.GetComponent<Image>().fillAmount = Mathf.Clamp01(progress);
             //Spark.SetActive(true);

             if (enterTimer >= EHoldTime)
             {

                 SceneManager.LoadScene("Enter");
                 enterTimer = 0f;

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

}*/

