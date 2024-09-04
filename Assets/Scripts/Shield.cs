using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float fadeDuration = 2f; // 渐隐时间
    private Animator animator;
    void Start()
    {
        // 获取当前物体的SpriteRenderer组件
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // 设置初始alpha值为0.6
        Color color = spriteRenderer.color;
        color.a = 0.6f;
        spriteRenderer.color = color;
    }

    // 当碰撞检测到带有“Ball”标签的物体时触发
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            animator.SetTrigger("Trigger");
            // 开始淡出效果
            StartCoroutine(FadeOutAndDestroy());
        }
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float startAlpha = spriteRenderer.color.a;
        float timeElapsed = 0f;

        // 逐渐减少alpha值
        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, 0, timeElapsed / fadeDuration);
            Color newColor = spriteRenderer.color;
            newColor.a = newAlpha;
            spriteRenderer.color = newColor;

            yield return null; // 等待下一帧
        }

        // 确保完全透明
        Color finalColor = spriteRenderer.color;
        finalColor.a = 0f;
        spriteRenderer.color = finalColor;

        // 销毁Shield对象
        Destroy(gameObject);
    }
}
