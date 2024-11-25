using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float survivalTime = 120f;
    private float remainingTime;
    public Slider timerSlider;
    public Image fillImage;

    public UnityEvent<float> onTimerUpdate;
    public UnityEvent<int> onDifficultyIncrease;
    public UnityEvent onGameOver;

    [SerializeField]
    public Color[] difficultyColors;

    private int difficultyStage = 1;
    private bool playerAlive = true;

    public GameObject gameOverScreen;
    public GameObject winScreen;

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        remainingTime = survivalTime;
        difficultyStage = 1;
        playerAlive = true;

        if (timerSlider != null)
        {
            timerSlider.maxValue = survivalTime;
            timerSlider.value = 0f;
        }

        UpdateTimerColor();
        StartCoroutine(StartEnemyAfterDelay(3f));
        StartCoroutine(SurvivalTimer());
    }

    private IEnumerator StartEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        FindObjectOfType<EnemyScript>()?.StartAttacks();
    }

    private IEnumerator SurvivalTimer()
    {
        float difficultyInterval = 30f;
        float timeSinceLastDifficultyIncrease = 0f;

        while (remainingTime > 0f && playerAlive)
        {
            remainingTime -= Time.deltaTime;
            timeSinceLastDifficultyIncrease += Time.deltaTime;

            onTimerUpdate?.Invoke(remainingTime);

            if (timerSlider != null)
            {
                timerSlider.value = survivalTime - remainingTime;
            }

            if (timeSinceLastDifficultyIncrease >= difficultyInterval)
            {
                difficultyStage++;
                UpdateTimerColor();
                onDifficultyIncrease?.Invoke(difficultyStage);
                timeSinceLastDifficultyIncrease = 0f;
            }

            yield return null;
        }

        GameOver();
    }

    private void UpdateTimerColor()
    {
        if (fillImage != null && difficultyColors.Length > 0)
        {
            int colorIndex = Mathf.Clamp(difficultyStage - 1, 0, difficultyColors.Length - 1);
            fillImage.color = difficultyColors[colorIndex];
        }
    }

    public void PlayerDied()
    {
        playerAlive = false;
        GameOver();
    }

    private void GameOver()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        if (playerAlive)
        {
            winScreen?.SetActive(true);
        }

        onGameOver?.Invoke();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
