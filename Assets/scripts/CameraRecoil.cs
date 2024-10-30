using UnityEngine;
using System.Collections;

public class CameraRecoil : MonoBehaviour
{
    public float recoilAmount = 0.1f;      // 抖动强度
    public float recoilRecoverySpeed = 5f; // 恢复速度

    private Vector3 originalPosition;      // 摄像机的初始位置
    private bool isRecoiling = false;      // 是否正在进行后坐力

    void Start()
    {
        // 记录摄像机的初始位置
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // 检测射击按键并触发后坐力
        if (Input.GetButtonDown("Fire1") && !isRecoiling)
        {
            StartCoroutine(ApplyRecoil());
        }

        // 平滑恢复到原始位置
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilRecoverySpeed);
    }

    private IEnumerator ApplyRecoil()
    {
        isRecoiling = true;

        // 添加一个随机抖动偏移量，仅在X轴和正Y轴产生偏移
        Vector3 recoilOffset = new Vector3(
            Random.Range(-recoilAmount, recoilAmount), // X轴的随机偏移量（左右方向）
            Random.Range(0, recoilAmount),             // Y轴仅向上偏移，避免向下
            0                                         // Z轴保持为0，避免向前/向后的抖动
        );

        transform.localPosition += recoilOffset; // 将偏移量应用到摄像机的当前位置

        // 停顿片刻后恢复
        yield return new WaitForSeconds(0.05f);
        isRecoiling = false;
    }
}
