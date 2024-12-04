using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private List<BuckController> controllers = new List<BuckController>();

    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isGameOver = false;

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

        float duration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rectTransform.anchoredPosition = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = targetPosition;
    }
}
