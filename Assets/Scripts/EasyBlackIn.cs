using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyBlackIn : MonoBehaviour
{
    public CanvasGroup blackCanvas;
    void Start()
    {
        StartCoroutine(FadeCanvasGroup(blackCanvas, 1, 0, 1));
    }
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }

        cg.alpha = endAlpha;  // 保证最后的alpha值精确到目标值
    }
}
