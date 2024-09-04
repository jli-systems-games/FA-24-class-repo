using System.Collections;
using UnityEngine;

public class PowerJem : MonoBehaviour
{
    public Player player;  // 代表哪个玩家
    public enum Player
    {
        Player1, Player2
    }
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private AudioManager audioManager;
    private GameStatusManager statusManager;
    private bool canInteract = true;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        statusManager = FindObjectOfType<GameStatusManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();        
        originalScale = transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && canInteract)
        {
            audioManager.PlayRandomAudioPowerGet();
            canInteract = false;

            if (player == Player.Player1)
            {
                statusManager.AddP1Power();
            }
            else
            {
                statusManager.AddP2Power();
            }

            // 调用协程来处理放大和透明度消失的效果
            StartCoroutine(CollisionEffect());
        }
    }

    private IEnumerator CollisionEffect()
    {
        // 将物体放大1.5倍
        Vector3 targetScale = originalScale * 1.5f;
        transform.localScale = targetScale;

        // 逐渐改变物体的透明度
        float duration = 1f;  // 持续时间1秒
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            // 计算当前透明度
            float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);

            // 设置物体的透明度
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a = alpha;
                spriteRenderer.color = color;
            }

            yield return null;
        }

        // 1秒后销毁自身
        Destroy(gameObject);
    }
}
