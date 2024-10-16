using UnityEngine;
using System.Collections;

public class RobotMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // 机器人的移动速度
    public float backwardSpeed = 1.0f; // 后退的速度
    public float rotationSpeed = 100.0f; // 旋转的速度
    public float backwardDistance = 0.15f; // 后退距离
    public float rotationAngle = 30f; // 旋转的角度
    public float waitTime = 0.5f; // 停顿时间
    private Vector3 moveDirection; // 当前移动方向
    private Rigidbody rb;
    private bool isMoving = true; // 标记机器人是否可以移动
    private RobotStatus robotStatus; // 引用RobotStatus脚本

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        robotStatus = GetComponent<RobotStatus>(); // 获取RobotStatus脚本
        ChangeDirection(); // 初始化移动方向
    }

    void Update()
    {
        if (!isMoving || robotStatus == null || !robotStatus.IsAlive()) return; // 如果机器人已死亡或不能移动，停止移动

        // 使用Rigidbody的MovePosition来移动机器人
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isMoving || robotStatus == null || !robotStatus.IsAlive()) return; // 如果机器人已死亡，忽略碰撞

        // 当碰到墙壁时，执行后退和旋转逻辑
        if (collision.gameObject.CompareTag("Wall") && isMoving)
        {
            isMoving = false;
            StartCoroutine(HandleCollision()); // 启动处理碰撞的协程
        }
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(waitTime); // 1. 停顿一会

        // 2. 直线后退
        float movedDistance = 0;
        Vector3 backwardDirection = -moveDirection;
        while (movedDistance < backwardDistance)
        {
            float step = backwardSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + backwardDirection * step);
            movedDistance += step;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime); // 3. 停顿一会

        // 4. 平滑旋转，围绕机器人的中心
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, initialRotation.eulerAngles.y + rotationAngle, 0);
        float rotationProgress = 0;
        while (rotationProgress < 1)
        {
            rotationProgress += rotationSpeed * Time.deltaTime / rotationAngle;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, rotationProgress);
            yield return null;
        }

        yield return new WaitForSeconds(waitTime); // 5. 停顿一会

        ChangeDirection(); // 6. 更新移动方向

        isMoving = true; // 恢复机器人移动
    }

    private void ChangeDirection()
    {
        moveDirection = transform.forward; // 根据机器人的旋转设置新的移动方向
    }

    public bool IsMoving()
    {
        return isMoving;
    }

}
