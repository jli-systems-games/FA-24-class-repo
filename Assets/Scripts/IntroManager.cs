using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroManager : MonoBehaviour
{
    public CanvasGroup fadeCanvasGroup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the Return/Enter key is pressed
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(StartGame());
        }
    }

    void GameEnter()
    {
        SceneManager.LoadScene("MiniGame1");
    }

    IEnumerator StartGame()
    {
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene("MiniGame1");
    }

    IEnumerator FadeIn()
    {
        float fadeDuration = 1f;
        while (fadeCanvasGroup.alpha > 0)
        {
            fadeCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float fadeDuration = 1f;
        while (fadeCanvasGroup.alpha < 1)
        {
            fadeCanvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}
