using System.Collections;
using UnityEngine;
using TMPro;

public class blinkTextOpen : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // 要控制闪烁的 TextMeshPro 对象
    public float transitionDuration = 1f; // 每个阶段渐变的持续时间

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>(); // 如果没有手动设置 TextMeshPro 对象，则尝试获取当前对象上的组件
        }

        if (textMeshPro != null)
        {
            StartCoroutine(BlinkText()); // 开始渐变协程
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
            // 透明度从 1f 渐变到 0.5f
            yield return StartCoroutine(FadeAlpha(1f, 0.5f, transitionDuration));

            // 透明度从 0.5f 渐变到 0f
            yield return StartCoroutine(FadeAlpha(0.5f, 0f, transitionDuration));

            // 透明度从 0f 渐变到 0.5f
            yield return StartCoroutine(FadeAlpha(0f, 0.5f, transitionDuration));

            // 透明度从 0.5f 渐变到 1f
            yield return StartCoroutine(FadeAlpha(0.5f, 1f, transitionDuration));

            yield return StartCoroutine(FadeAlpha(1f, 1f, transitionDuration));
        }
    }

    private IEnumerator FadeAlpha(float from, float to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // 计算当前时间的透明度值
            textMeshPro.alpha = Mathf.Lerp(from, to, elapsedTime / duration);

            // 累加时间
            elapsedTime += Time.deltaTime;

            // 等待下一帧
            yield return null;
        }

        // 确保在最后一帧完全到达目标值
        textMeshPro.alpha = to;
    }
}
