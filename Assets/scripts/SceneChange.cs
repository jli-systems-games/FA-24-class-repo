using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChange : MonoBehaviour
{
    public GameObject[] torches;
    public GameObject[] fireplaces;
    public TextMeshProUGUI progressText; 
    public GameObject sceneChangePanel;

    private int litCount = 0;
    private const int totalLights = 6;

    void Start()
    {
        litCount = 0;
        UpdateProgressUI();

        if (sceneChangePanel != null)
        {
            sceneChangePanel.SetActive(false);
        }
    }

    public void LightObject(GameObject obj)
    {
        litCount++;
        UpdateProgressUI();

        // checks if all obj lit
        if (litCount == totalLights)
        {
            LoadNextScene();
        }
    }

    void UpdateProgressUI()
    {
        if (progressText != null)
        {
            progressText.text = $"{litCount}/{totalLights} sparks";
        }
    }

    void LoadNextScene()
    {
        if (sceneChangePanel != null)
        {
            sceneChangePanel.SetActive(true);
        }

        StartCoroutine(SceneTransition());
    }

    IEnumerator SceneTransition()
    {
        yield return new WaitForSeconds(18);

        SceneManager.LoadScene("scene2");
    }
}