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
    public GameObject gameOverText;
    public Button restartButton;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = Camera.main.transform;

        gameOverPanel.SetActive(false);
        gameOverText.SetActive(false);
        restartButton.enabled = false;

        restartButton.onClick.AddListener(RestartGame);
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
        gameOverText.SetActive(true);
        restartButton.enabled = true;

        audioSource.Stop();
        restartButton.interactable = true;

        if (Input.GetMouseButtonDown(0))
        {
            RestartGame();
            Debug.Log("Mouse Clicked");
        }
    }

    public void RestartGame()
    {
        Debug.Log("Restart Game button clicked.");

        SceneManager.LoadScene("scene2");
    }
}