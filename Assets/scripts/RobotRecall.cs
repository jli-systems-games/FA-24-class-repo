using UnityEngine;

public class RobotRecall : MonoBehaviour
{
    public Transform chargingStationPoint;   // 充电站位置
    public Transform repairStationPoint;     // 维修站位置
    public float recallSpeed = 3.0f;         // 召回时的移动速度

    private RobotMovement robotMovement;
    private RobotStatus robotStatus;
    private Rigidbody rb;
    private bool isRecalling = false;
    private Transform targetRecallPoint;

    void Start()
    {
        robotMovement = GetComponent<RobotMovement>();
        robotStatus = GetComponent<RobotStatus>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!robotStatus.IsAlive())
            return;

        if (isRecalling && targetRecallPoint != null)
        {
            // 停止机器人正常移动
            robotMovement.SetIsMoving(false);

            // 朝召回点移动
            MoveTowardsRecallPoint();
        }
    }

    // 修改 Recall 方法，接受召回点作为参数
    public void Recall(Transform recallPoint)
    {
        if (!robotStatus.IsAlive())
            return;

        targetRecallPoint = recallPoint;
        isRecalling = true;
    }

    private void MoveTowardsRecallPoint()
    {
        Vector3 direction = (targetRecallPoint.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetRecallPoint.position);

        // 使用 Rigidbody 移动机器人
        rb.MovePosition(transform.position + direction * recallSpeed * Time.deltaTime);

        // 使机器人面向召回点
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // 检测是否到达召回点
        if (distance < 0.1f)
        {
            isRecalling = false;
            robotMovement.SetIsMoving(true); // 恢复机器人正常移动
            targetRecallPoint = null;        // 重置目标召回点
        }
    }
}
