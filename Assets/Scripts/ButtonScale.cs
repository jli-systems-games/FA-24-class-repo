using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;  // 引用按钮组件
    private RectTransform rectTransform;  // 按钮的 RectTransform
    private Vector3 originalScale;  // 原始大小
    private float scaleFactor = 0.9f;  // 缩小比例
    private float duration = 0.1f;  // 动画持续时间
    private bool isPointerOver = false;  // 检查鼠标是否悬停在按钮上
    private AudioManager audioManager;

    void Start()
    {
        // 获取按钮和 RectTransform 组件
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
        audioManager = FindObjectOfType<AudioManager>();
        // 为按钮点击事件添加监听
        button.onClick.AddListener(OnButtonClick);
    }

    // 点击按钮时执行
    void OnButtonClick()
    {
        audioManager.PlayButtonClick();
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(ScaleEffect());
    }

    // 鼠标悬停时执行
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isPointerOver)  // 确保只在初次进入时触发一次效果
        {
            isPointerOver = true;
            audioManager.PlayButtonHover();
            StartCoroutine(ScaleEffect());
        }
    }

    // 鼠标移开时重置状态
    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOver = false;
    }

    // 协程：缩小按钮然后恢复
    IEnumerator ScaleEffect()
    {
        // 缩小按钮
        yield return StartCoroutine(ScaleTo(scaleFactor));
        // 恢复按钮大小
        yield return StartCoroutine(ScaleTo(1.0f));
    }

    // 协程：缩放到目标大小
    IEnumerator ScaleTo(float targetScale)
    {
        Vector3 target = originalScale * targetScale;
        Vector3 startScale = rectTransform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            rectTransform.localScale = Vector3.Lerp(startScale, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.localScale = target;  // 最终设置为目标大小
    }
}
