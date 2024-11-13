using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // 小球的 Transform
    public Transform target;

    // 摄像机与小球的偏移量
    public Vector3 offset;

    // 平滑跟随速度
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target != null)
        {
            // 计算目标位置（小球位置 + 偏移量）
            Vector3 desiredPosition = target.position + offset;

            // 使用平滑插值计算摄像机的新位置
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 更新摄像机位置
            transform.position = smoothedPosition;

            // 如果需要摄像机始终看向小球，可以启用下面的代码
             transform.LookAt(target);
        }
    }
}
