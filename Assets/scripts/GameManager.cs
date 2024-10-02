using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public float timeLimit = 20f;
    public TextMeshProUGUI timerText;
    private bool isGamePaused = false;
    private float currentTime;

    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        currentTime = timeLimit;

        StartCoroutine(Timer());
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
        PauseGame();
    }

    public void LoseGame()
    {
        losePanel.SetActive(true);
        PauseGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        ResumeGame();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;

        LockCursor();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
    }

    private IEnumerator Timer()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            yield return null;
        }

        // when time is up
        LoseGame();
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

}
