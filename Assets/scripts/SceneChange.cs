using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneChange : MonoBehaviour
{
    public GameObject[] torches;
    public GameObject[] fireplaces;
    public TextMeshProUGUI progressText;

    private int litCount = 0;
    private const int totalLights = 6;

    void Start()
    {
        litCount = 0;
        UpdateProgressUI();
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
        SceneManager.LoadScene("scene2");
    }
}