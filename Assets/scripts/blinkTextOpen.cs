using System.Collections;
using UnityEngine;
using TMPro;

public class blinkTextOpen : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; 
    public float transitionDuration = 1f; 

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>(); 
        }

        if (textMeshPro != null)
        {
            StartCoroutine(BlinkText()); 
        }
        else
        {
            Debug.LogError("TextMeshProUGUI 组件未设置或未找到！");
        }
    }

    private IEnumerator BlinkText()
    {
        while (true)
        {
            
            yield return StartCoroutine(FadeAlpha(1f, 0.5f, transitionDuration));

            
            yield return StartCoroutine(FadeAlpha(0.5f, 0f, transitionDuration));

            
            yield return StartCoroutine(FadeAlpha(0f, 0.5f, transitionDuration));

            
            yield return StartCoroutine(FadeAlpha(0.5f, 1f, transitionDuration));

            yield return StartCoroutine(FadeAlpha(1f, 1f, transitionDuration));
        }
    }

    private IEnumerator FadeAlpha(float from, float to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            
            textMeshPro.alpha = Mathf.Lerp(from, to, elapsedTime / duration);

            
            elapsedTime += Time.deltaTime;

            
            yield return null;
        }

        
        textMeshPro.alpha = to;
    }
}
