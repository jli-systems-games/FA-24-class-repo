using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public Transform player; // 玩家的位置
    public float chaseRange = 10f; // 僵尸开始追逐玩家的距离
    public float attackRange = 2f; // 攻击范围
    public float zombieSpeed = 3.5f; // 僵尸速度

    private NavMeshAgent agent; // 导航网格代理
    private float distanceToPlayer = Mathf.Infinity; // 僵尸和玩家之间的距离

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // 获取NavMeshAgent组件
        agent.speed = zombieSpeed; // 设置僵尸速度
    }

    void Update()
    {
        // 计算与玩家之间的距离
        distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            // 追逐玩家
            agent.SetDestination(player.position);

            if (distanceToPlayer <= attackRange)
            {
                // 攻击逻辑（可在此处添加）
                Debug.Log("Zombie attacks!");
            }
        }
    }

    // 可视化追逐和攻击范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
