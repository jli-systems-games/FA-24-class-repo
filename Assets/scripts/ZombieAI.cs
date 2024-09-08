using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player; // ��ҵ�λ��
    public float chaseRange = 10f; // ��ʬ��ʼ׷����ҵľ���
    public float attackRange = 2f; // ������Χ
    public float zombieSpeed = 3.5f; // ��ʬ�ٶ�

    private NavMeshAgent agent; // �����������
    private float distanceToPlayer = Mathf.Infinity; // ��ʬ�����֮��ľ���

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // ��ȡNavMeshAgent���
        agent.speed = zombieSpeed; // ���ý�ʬ�ٶ�
    }

    void Update()
    {
        // ���������֮��ľ���
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            // ׷�����
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {
                // �����߼������ڴ˴���ӣ�
                Debug.Log("Zombie attacks!");
            }
        }
    }

    // ���ӻ�׷��͹�����Χ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
