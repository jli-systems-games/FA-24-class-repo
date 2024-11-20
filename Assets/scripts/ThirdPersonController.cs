using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 3f;          // 移动速度
    public float rotationSpeed = 10f;    // 平滑旋转速度
    public float jumpHeight = 2f;        // 跳跃高度
    public float gravity = -9.81f;       // 重力

    [Header("Camera Settings")]
    public Transform cameraTransform;   // 摄像机的 Transform

    private CharacterController characterController; // 角色控制器组件
    private Vector3 velocity;            // 垂直方向的速度（用于重力）
    private bool isGrounded;             // 是否在地面

    void Start()
    {
        // 获取 CharacterController 组件
        characterController = GetComponent<CharacterController>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned!");
        }

        // 确保摄像机从一开始就跟随玩家
        AlignCamera();
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    private void LateUpdate()
    {
        // 在所有移动和旋转完成后更新摄像机
        AlignCamera();
    }

    void HandleMovement()
    {
        // 获取输入方向
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 inputDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // 只有在有输入时才移动
        if (inputDirection.magnitude >= 0.1f)
        {
            // 根据摄像机的方向计算目标角度
            float targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // 平滑旋转玩家
            float smoothAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetAngle, Time.deltaTime * rotationSpeed);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            // 计算移动方向
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            // 移动玩家
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    void HandleGravity()
    {
        // 检测是否着地
        isGrounded = characterController.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 稍微向下压，保持接触地面
        }

        // 跳跃逻辑
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 应用重力
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void AlignCamera()
    {
        if (cameraTransform != null)
        {
            // 将摄像机保持在玩家背后
            Vector3 cameraOffset = new Vector3(0, 2, -4); // 根据需要调整偏移值
            cameraTransform.position = transform.position + cameraOffset;
            cameraTransform.LookAt(transform.position + Vector3.up * 1.5f); // 看向玩家的上半部分
        }
    }
}
