using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration = 0.5f;  // 震动持续时间
    public float dampingSpeed = 1.0f;  // 衰减速度

    private Vector3 initialPosition;  // 相机初始位置

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    // 触发相机震动
    public void TriggerShake()
    {      
        StartCoroutine(Shake(2));
    }
    public void TriggerShakeBallIn()
    {    
        StartCoroutine(Shake(1));
    }

    // 协程：实现相机震动效果
    IEnumerator Shake(float magnitude)
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            // 生成随机的位移偏移量
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            // 将相机位置设置为随机偏移量
            transform.localPosition = new Vector3(initialPosition.x + offsetX, initialPosition.y + offsetY, initialPosition.z);

            // 计算经过的时间
            elapsed += Time.deltaTime;

            // 随着时间衰减震动效果
            magnitude = Mathf.Lerp(magnitude, 0f, elapsed / shakeDuration * dampingSpeed);

            yield return null;
        }

        // 震动结束后恢复相机到初始位置
        transform.localPosition = initialPosition;
    }
}
