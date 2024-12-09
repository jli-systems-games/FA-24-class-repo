using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // 玩家角色的 Transform
    public Vector3 offset = new Vector3(0, 3, -6); // 摄像机的偏移量
    public float smoothSpeed = 0.125f; // 平滑移动速度
    public float rotationSpeed = 100f; // 鼠标控制视角的速度

    private float pitch = 0f; // 垂直视角角度
    private float yaw = 0f; // 水平视角角度

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
    pitch += mouseY * rotationSpeed * Time.deltaTime; // 修改为加法

    // 限制垂直视角范围
    pitch = Mathf.Clamp(pitch, -30f, 60f);

    // 旋转摄像机
    Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
    transform.position = target.position + rotation * offset;
    transform.LookAt(target.position + Vector3.up * 1.5f); // 对准玩家胸部或头部
}


    private void HandleCameraPosition()
    {
        Vector3 desiredPosition = target.position + Quaternion.Euler(pitch, yaw, 0) * offset;

        // 射线检测，防止摄像机穿墙
        if (Physics.Linecast(target.position, desiredPosition, out RaycastHit hit))
        {
            desiredPosition = hit.point - (desiredPosition - target.position).normalized * 0.2f; // 调整摄像机位置
        }

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }


    private void HandleCameraCollision()
    {
        Vector3 targetPosition = target.position + Vector3.up * 1.5f; // 目标位置
        Vector3 desiredCameraPosition = targetPosition + Quaternion.Euler(pitch, yaw, 0) * offset; // 理想的摄像机位置

        // 检测从目标到摄像机的障碍物
        Ray ray = new Ray(targetPosition, desiredCameraPosition - targetPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, offset.magnitude))
        {
            // 如果碰撞，摄像机移动到障碍物表面
            transform.position = hit.point - ray.direction * 0.2f; // 稍微偏离墙面，避免摄像机陷入墙壁
        }
        else
        {
            // 没有碰撞，设置到理想位置
            transform.position = desiredCameraPosition;
        }

        // 始终看向目标
        transform.LookAt(targetPosition);
    }

}
