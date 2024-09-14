using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float defaultShakeDuration = 0.5f;
    public float defaultShakeMagnitude = 0.2f;

    private Vector3 originalPos;

    void Start()
    {
        originalPos = transform.localPosition;
    }

    public void ShakeCamera(float duration, float magnitude)
    {
        StopAllCoroutines(); // 停止之前的抖动
        StartCoroutine(Shake(duration, magnitude));
    }

    public void ShakeCamera()
    {
        ShakeCamera(defaultShakeDuration, defaultShakeMagnitude);
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // 在给定的范围内随机移动相机的位置
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x + offsetX, originalPos.y + offsetY, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // 抖动结束后恢复相机到原来的位置
        transform.localPosition = originalPos;
    }
}
