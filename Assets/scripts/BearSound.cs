using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BearSound : MonoBehaviour
{
    public AudioClip bearSound;
    private AudioSource audioSource;
    public float bearDetection = 10f;
    public float bearDanger = 3f;
    private Transform player;

    public GameObject gameOverPanel;
    public Button restartButton;

    private bool isGamePaused = false;
    public FirstPersonMovement movementScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = Camera.main.transform;

        gameOverPanel.SetActive(false);
        restartButton.enabled = false;

        restartButton.onClick.AddListener(RestartGame);

        Time.timeScale = 1;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= bearDetection && !audioSource.isPlaying)
        {
            audioSource.clip = bearSound;
            audioSource.Play();
        }

        else if (distance > bearDetection && audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (distance <= bearDanger)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverPanel.SetActive(true);
        restartButton.enabled = true;

        audioSource.Stop();

        PauseGame();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public bool IsGamePaused()
    {
        return isGamePaused;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
        ResumeGame();

        movementScript.enabled = true;

        LockCursor();
    }
}