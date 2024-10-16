using UnityEngine;
using System.Collections;

public class RobotMovement : MonoBehaviour
{
    public float moveSpeed = 3.0f; // �����˵��ƶ��ٶ�
    public float backwardSpeed = 1.0f; // ���˵��ٶ�
    public float rotationSpeed = 100.0f; // ��ת���ٶ�
    public float backwardDistance = 0.15f; // ���˾���
    public float rotationAngle = 30f; // ��ת�ĽǶ�
    public float waitTime = 0.5f; // ͣ��ʱ��
    private Vector3 moveDirection; // ��ǰ�ƶ�����
    private Rigidbody rb;
    private bool isMoving = true; // ��ǻ������Ƿ�����ƶ�
    private RobotStatus robotStatus; // ����RobotStatus�ű�

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        robotStatus = GetComponent<RobotStatus>(); // ��ȡRobotStatus�ű�
        ChangeDirection(); // ��ʼ���ƶ�����
    }

    void Update()
    {
        if (!isMoving || robotStatus == null || !robotStatus.IsAlive()) return; // ��������������������ƶ���ֹͣ�ƶ�

        // ʹ��Rigidbody��MovePosition���ƶ�������
        rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isMoving || robotStatus == null || !robotStatus.IsAlive()) return; // �����������������������ײ

        // ������ǽ��ʱ��ִ�к��˺���ת�߼�
        if (collision.gameObject.CompareTag("Wall") && isMoving)
        {
            isMoving = false;
            StartCoroutine(HandleCollision()); // ����������ײ��Э��
        }
    }

    private IEnumerator HandleCollision()
    {
        yield return new WaitForSeconds(waitTime); // 1. ͣ��һ��

        // 2. ֱ�ߺ���
        float movedDistance = 0;
        Vector3 backwardDirection = -moveDirection;
        while (movedDistance < backwardDistance)
        {
            float step = backwardSpeed * Time.deltaTime;
            rb.MovePosition(transform.position + backwardDirection * step);
            movedDistance += step;
            yield return null;
        }

        yield return new WaitForSeconds(waitTime); // 3. ͣ��һ��

        // 4. ƽ����ת��Χ�ƻ����˵�����
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, initialRotation.eulerAngles.y + rotationAngle, 0);
        float rotationProgress = 0;
        while (rotationProgress < 1)
        {
            rotationProgress += rotationSpeed * Time.deltaTime / rotationAngle;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, rotationProgress);
            yield return null;
        }

        yield return new WaitForSeconds(waitTime); // 5. ͣ��һ��

        ChangeDirection(); // 6. �����ƶ�����

        isMoving = true; // �ָ��������ƶ�
    }

    private void ChangeDirection()
    {
        moveDirection = transform.forward; // ���ݻ����˵���ת�����µ��ƶ�����
    }

    public bool IsMoving()
    {
        return isMoving;
    }

}
