using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CarWinState : MonoBehaviour
{
    public GameObject winPanel;

    public GameObject itemsNeeded;
    public TextMeshProUGUI pressF;
    public float promptDuration = 3f;

    private bool isplayerInRange = false;
    private bool isGamePaused = false;

    public Button restartButton;

    private void Start()
    {
        winPanel.SetActive(false);
        itemsNeeded.SetActive(false);
        pressF.gameObject.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayerInRange = true;

            if (CollectItems.item1 && CollectItems.item2)
            {
                pressF.gameObject.SetActive(true);
                itemsNeeded.SetActive(false);
            }
            else
            {
                itemsNeeded.SetActive(true);
                pressF.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayerInRange = false;

            itemsNeeded.SetActive(false);
            pressF.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isplayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (CollectItems.item1 && CollectItems.item2)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0f;

                PauseGame();
            }
        }
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
        SceneManager.LoadScene(0);
        ResumeGame();
    }
}
