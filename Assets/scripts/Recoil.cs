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

        // 应用后坐力
        Vector3 recoilOffset = Vector3.back * recoilAmount; // 后坐力方向
        gunTransform.localPosition += recoilOffset;

        yield return new WaitForSeconds(0.05f); // 短暂停留时间以增强效果

        isRecoiling = false; // 后坐力结束
    }
}
