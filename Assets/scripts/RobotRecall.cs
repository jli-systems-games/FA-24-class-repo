using UnityEngine;

public class RobotRecall : MonoBehaviour
{
    public Transform chargingStationPoint;   // ���վλ��
    public Transform repairStationPoint;     // ά��վλ��
    public float recallSpeed = 3.0f;         // �ٻ�ʱ���ƶ��ٶ�

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
            // ֹͣ�����������ƶ�
            robotMovement.SetIsMoving(false);

            // ���ٻص��ƶ�
            MoveTowardsRecallPoint();
        }
    }

    // �޸� Recall �����������ٻص���Ϊ����
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

        // ʹ�� Rigidbody �ƶ�������
        rb.MovePosition(transform.position + direction * recallSpeed * Time.deltaTime);

        // ʹ�����������ٻص�
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
        }

        // ����Ƿ񵽴��ٻص�
        if (distance < 0.1f)
        {
            isRecalling = false;
            robotMovement.SetIsMoving(true); // �ָ������������ƶ�
            targetRecallPoint = null;        // ����Ŀ���ٻص�
        }
    }
}
