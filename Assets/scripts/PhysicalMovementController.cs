using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalMovementController : MonoBehaviour
{
    public float moveSpeed = 5f; // 移动速度
    public float rotationSpeed = 720f; // 旋转速度
    public float jumpForce = 5f; // 跳跃力
    public float gravity = -9.8f; // 重力

    private CharacterController controller; // 角色控制器
    private Vector3 moveDirection; // 移动方向
    private bool isGrounded; // 是否在地面

    public Transform cameraTransform; // 摄像机

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        // 使用 Unity 默认输入系统获取方向输入
        float horizontal = Input.GetAxis("Horizontal"); // A/D 或 左右方向键
        float vertical = Input.GetAxis("Vertical"); // W/S 或 上下方向键

        // 基于摄像机方向的移动
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0f; // 忽略垂直分量
        right.y = 0f; // 忽略垂直分量
        forward.Normalize();
        right.Normalize();

        // 计算目标移动方向
        Vector3 desiredMoveDirection = (forward * vertical + right * horizontal).normalized;

        // 如果有输入，则移动和旋转角色
        if (desiredMoveDirection.magnitude > 0.1f)
        {
            // 旋转角色面对移动方向
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // 设置移动方向
            moveDirection.x = desiredMoveDirection.x * moveSpeed;
            moveDirection.z = desiredMoveDirection.z * moveSpeed;
        }
        else
        {
            moveDirection.x = 0f;
            moveDirection.z = 0f;
        }

        // 应用重力
        if (!isGrounded)
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        else if (moveDirection.y < 0)
        {
            moveDirection.y = -2f;
        }

        // 应用移动
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void HandleJump()
    {
        // 检查是否按下空格键进行跳跃
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            moveDirection.y = jumpForce;
        }
    }
}

