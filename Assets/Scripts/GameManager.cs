using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float survivalTime = 90f;
    public Slider timerSlider;
    public Image fillImage;
    public Color[] difficultyColors;

    public UnityEvent<float> onTimerUpdate;
    public UnityEvent<int> onDifficultyIncrease = new UnityEvent<int>();

    public UnityEvent onGameOver;

    public GameObject gameOverScreen;
    public GameObject winScreen;
    public HUDManager hudManager;

    public Sprite[] backgroundStages;
    public GameObject background;

    private SpriteRenderer backgroundRenderer;
    private float remainingTime;
    public int difficultyStage = 1;
    private const int maxDifficultyStage = 3;
    private bool playerAlive = true;

    private void Start()
    {
        backgroundRenderer = background?.GetComponent<SpriteRenderer>();
        StartGame();
    }

    private IEnumerator StartEnemyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        var enemy = FindObjectOfType<EnemyScript>();
        if (enemy != null)
        {
            Debug.Log("EnemyScript found. Starting attacks...");
            enemy.StartAttacks();
        }
        else
        {
            Debug.LogError("EnemyScript not found in the scene.");
        }
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

        UpdateBackground();
        UpdateTimerColor();

   
        StartCoroutine(StartEnemyAfterDelay(3f));
        StartCoroutine(SurvivalTimer());
    }


    private IEnumerator SurvivalTimer()
    {
        float difficultyInterval = survivalTime / maxDifficultyStage;

        while (remainingTime > 0f && playerAlive)
        {
            remainingTime -= Time.deltaTime;
            //onTimerUpdate?.Invoke(remainingTime);

            timerSlider.value = survivalTime - remainingTime;

            if (remainingTime % difficultyInterval < Time.deltaTime && difficultyStage < maxDifficultyStage)
            {
                difficultyStage++;
                Debug.Log($"Difficulty stage updated to: {difficultyStage}");

                onDifficultyIncrease?.Invoke(difficultyStage);

                UpdateBackground();
                UpdateTimerColor();

                if (hudManager != null)
                {
                    Debug.Log($"Calling HUDManager to update HUD for stage: {difficultyStage}");
                    hudManager.UpdateHUD(difficultyStage);
                }
            }

            yield return null;
        }

        GameOver();
    }



    private void UpdateBackground()
    {
        if (backgroundRenderer != null && difficultyStage <= backgroundStages.Length)
            backgroundRenderer.sprite = backgroundStages[difficultyStage - 1];
    }

    private void UpdateTimerColor()
    {
        if (fillImage != null && difficultyStage <= difficultyColors.Length)
            fillImage.color = difficultyColors[difficultyStage - 1];
    }

    private void GameOver()
    {
        gameOverScreen.SetActive(!playerAlive);
        winScreen.SetActive(playerAlive);

        onGameOver?.Invoke();
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayerDied()
    {
        playerAlive = false;
    }
}
