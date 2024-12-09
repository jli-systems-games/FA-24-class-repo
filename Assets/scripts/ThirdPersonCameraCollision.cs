using UnityEngine;

public class ThirdPersonCameraCollision : MonoBehaviour
{
    public Transform target; // 摄像机跟随的目标
    public Vector3 offset = new Vector3(0, 1.5f, -3f); // 摄像机相对于目标的偏移
    public float smoothSpeed = 10f; // 平滑移动速度
    public LayerMask collisionMask; // 检测哪些层

    private Vector3 currentCameraPosition;

    private void Start()
    {
        currentCameraPosition = target.position + offset;
    }

    private void LateUpdate()
    {
        HandleCameraCollision();
    }

    private void HandleCameraCollision()
    {
        // 目标位置
        Vector3 targetPosition = target.position + Vector3.up * 1.5f;

        // 理想的摄像机位置
        Vector3 desiredCameraPosition = targetPosition + offset;

        // 射线检测障碍物
        Ray ray = new Ray(targetPosition, desiredCameraPosition - targetPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, offset.magnitude, collisionMask))
        {
            // 如果碰撞，摄像机移动到碰撞点稍微靠外
            currentCameraPosition = hit.point - ray.direction * 0.2f; // 偏移 0.2 防止摄像机卡进墙内
        }
        else
        {
            // 没有碰撞，恢复到理想位置
            currentCameraPosition = Vector3.Lerp(currentCameraPosition, desiredCameraPosition, Time.deltaTime * smoothSpeed);
        }

        // 设置摄像机位置和方向
        transform.position = currentCameraPosition;
        transform.LookAt(targetPosition);
    }
}
