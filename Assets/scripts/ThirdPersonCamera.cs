using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 玩家角色的 Transform
    public Vector3 offset = new Vector3(0, 3, -6); // 摄像机的偏移量
    public float smoothSpeed = 0.125f; // 平滑移动速度
    public float rotationSpeed = 100f; // 鼠标控制视角的速度
    public LayerMask collisionMask; // 碰撞检测的图层

    private float pitch = 0f; // 垂直视角角度
    private float yaw = 0f; // 水平视角角度
    private Vector3 velocity = Vector3.zero; // 用于 SmoothDamp 的速度缓存

    private void Start()
    {
        // 初始方向与目标位置保持一致
        if (target)
        {
            yaw = target.eulerAngles.y;
        }

        Cursor.lockState = CursorLockMode.Locked; // 隐藏并锁定鼠标
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target is not assigned!");
            return;
        }

        HandleCameraRotation();
        HandleCameraPosition();
    }

    private void HandleCameraRotation()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // 更新水平和垂直视角
        yaw += mouseX * rotationSpeed * Time.deltaTime;
        pitch -= mouseY * rotationSpeed * Time.deltaTime; // 改为减法，修复 Y 轴反转问题

        // 限制垂直视角范围
        pitch = Mathf.Clamp(pitch, -30f, 60f);
    }

    private void HandleCameraPosition()
    {
        Vector3 targetPosition = target.position + Vector3.up * 1.5f; // 玩家上方的目标点
        Vector3 desiredPosition = targetPosition + Quaternion.Euler(pitch, yaw, 0) * offset;

        // 射线检测，防止摄像机穿墙
        if (Physics.Linecast(targetPosition, desiredPosition, out RaycastHit hit, collisionMask))
        {
            // 如果有碰撞，将相机位置调整到碰撞点
            desiredPosition = hit.point - (desiredPosition - targetPosition).normalized * 0.2f; // 距离墙面稍微偏移
        }

        // 平滑移动到目标位置
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        // 让摄像机始终看向目标
        transform.LookAt(targetPosition);
    }
}
