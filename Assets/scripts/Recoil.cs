using UnityEngine;

public class Recoil : MonoBehaviour
{
    public Transform gunTransform; // 枪的 Transform
    public float recoilAmount = 0.5f; // 后坐力强度
    public float recoilRecoverySpeed = 5f; // 后坐力恢复速度

    private Vector3 originalPosition; // 枪的初始位置
    private bool isRecoiling = false; // 是否在后坐状态

    void Start()
    {
        originalPosition = gunTransform.localPosition; // 存储初始位置
    }

    void Update()
    {
        // 当射击时产生后坐力
        if (Input.GetButtonDown("Fire1") && !isRecoiling)
        {
            StartCoroutine(ApplyRecoil());
        }

        // 平滑恢复到原始位置
        gunTransform.localPosition = Vector3.Lerp(gunTransform.localPosition, originalPosition, Time.deltaTime * recoilRecoverySpeed);
    }

    private System.Collections.IEnumerator ApplyRecoil()
    {
        isRecoiling = true;

        // 随机选择向右或向上偏移
        Vector3 recoilDirection = Random.Range(0, 2) == 0 ? Vector3.right : Vector3.up;
        Vector3 recoilOffset = recoilDirection * recoilAmount;

        // 将后坐力方向应用到枪的当前位置
        gunTransform.localPosition += recoilOffset;

        yield return new WaitForSeconds(0.05f); // 短暂停留时间以增强效果

        isRecoiling = false; // 后坐力结束
    }
}
