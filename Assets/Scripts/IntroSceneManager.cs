using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    public TextMeshProUGUI[] introTexts; // Assign the text objects in the Inspector
    public Image fadeImage; // Assign the fade screen image
    public float textFadeDuration = 2.0f; // Duration for each text fade-in
    public float delayBetweenTexts = 5.0f; // Delay between each text fade-in
    public float fadeOutDelay = 2.0f; // Delay before fading out all texts and screen
    public float fadeOutDuration = 2.0f; // Duration for fading out everything
    public string nextScene = "Scene 01"; // Scene to load after the intro

    private void Start()
    {
        // Ensure all texts start with alpha 0
        foreach (var text in introTexts)
        {
            SetAlpha(text, 0);
        }

        StartCoroutine(PlayIntro());
    }

    private IEnumerator PlayIntro()
    {
        // Start with the screen faded in
        fadeImage.color = new Color(0, 0, 0, 1);
        yield return StartCoroutine(FadeImage(0)); // Fade screen to transparent

        // Fade in each text sequentially with a delay
        foreach (var text in introTexts)
        {
            yield return StartCoroutine(FadeText(text, 1)); // Fade text to alpha 1
            yield return new WaitForSeconds(delayBetweenTexts); // Wait before the next text
        }

        // Delay before fading out all texts and screen
        yield return new WaitForSeconds(fadeOutDelay);

        // Fade out all text and screen
        foreach (var text in introTexts)
        {
            StartCoroutine(FadeText(text, 0)); // Fade text back to alpha 0
        }
        yield return StartCoroutine(FadeImage(1)); // Fade screen to black

        // Load the next scene
        SceneManager.LoadScene(nextScene);
    }

    private IEnumerator FadeText(TextMeshProUGUI text, float targetAlpha)
    {
        float elapsedTime = 0f;
        Color color = text.color;
        float initialAlpha = color.a;

        while (elapsedTime < textFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(initialAlpha, targetAlpha, elapsedTime / textFadeDuration);
            text.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        text.color = color;
    }

    private IEnumerator FadeImage(float targetAlpha)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        float initialAlpha = color.a;

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Lerp(initialAlpha, targetAlpha, elapsedTime / fadeOutDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = targetAlpha;
        fadeImage.color = color;
    }

    private void SetAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }
}