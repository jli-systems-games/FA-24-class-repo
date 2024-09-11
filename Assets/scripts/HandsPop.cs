using System.Collections;
using UnityEngine;

public class HandsPop : MonoBehaviour
{
    private Vector2 startPosition; // 使用场景中的初始位置
    private Vector2 endPosition;   // 上移的位置
    private float showDuration = 0.5f;
    private HandsManager gameManager; // 引用游戏管理器
    private bool hittable = false;    // 是否可被击打

    private void Awake()
    {
        // 获取场景中的初始位置
        startPosition = transform.localPosition;
        // 设置上移后的目标位置
        endPosition = startPosition + new Vector2(0f, 3.5f); // 调整上移的高度，可以根据需要修改
    }

    public void Initialize(HandsManager manager)
    {
        gameManager = manager;
    }

    // 开始显示手
    public IEnumerator ShowHand()
    {
        hittable = true;

        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            // 从起始位置到目标位置插值
            transform.localPosition = Vector2.Lerp(startPosition, endPosition, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = endPosition;

        // 等待击打，3秒内未击打则判定为Missed
        float timer = 3f;
        while (timer > 0f && hittable)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        if (hittable)
        {
            Debug.Log("Missed");
            yield return HideHand();
        }
    }

    private void OnMouseDown()
    {
        if (hittable)
        {
            hittable = false;
            Debug.Log("Hand Hit!");

            StopAllCoroutines(); // 停止所有协程
            StartCoroutine(HideHand()); // 开始隐藏
        }
    }

    // 隐藏手
    private IEnumerator HideHand()
    {
        float elapsed = 0f;
        while (elapsed < showDuration)
        {
            // 从目标位置返回到初始位置
            transform.localPosition = Vector2.Lerp(endPosition, startPosition, elapsed / showDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPosition;
        gameObject.SetActive(false); // 隐藏物体
        gameManager.OnHandCleared(); // 通知管理器手已被击打完
    }
}
