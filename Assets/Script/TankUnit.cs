using UnityEngine;


public class TankUnit : MonoBehaviour
{
    public enum Side
    {
        Red,
        Blue,
    }

    public Side side;
    public SpriteRenderer[] spriteRenderers;
    public GameObject bulletPrefab; 
    public float moveSpeed = 5.0f;
    public float attackRange = 10f; 
    public float fireRate = 1f; 
    private float fireCooldown = 0f; 
    public Transform turretTransform;
    public Transform bulletAnchor;
    public Transform secondBulletAnchor;

    private GameObject[] enemies; 
    private GameObject target; // 当前目标

    void Start()
    {
        SetColorAndTag();

        FindEnemies();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (enemies.Length == 0) return;

        //UpdateTarget();

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= attackRange)
            {
                StopAndShoot();
            }
            else
            {
                MoveTowardsTarget();
            }
        }

        if (target != null)
        {
            Vector3 direction = (target.transform.position - turretTransform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);
            turretTransform.rotation = targetRotation;
        }

    }

    void SetColorAndTag()
    {
        switch (side)
        {
            case Side.Red:
                foreach (SpriteRenderer spriteRender in spriteRenderers)
                {
                    spriteRender.color = Color.red;
                }
                gameObject.tag = "RedUnit";
                gameObject.layer = 8;
                break;

            case Side.Blue:
                foreach (SpriteRenderer spriteRender in spriteRenderers)
                {
                    spriteRender.color = Color.cyan;
                }
                gameObject.tag = "BlueUnit";
                gameObject.layer = 9;
                break;
        }
    }

    void FindEnemies()
    {
        //Debug.Log("重置索敌");
        enemies = new GameObject[0];
        string enemyTag = side == Side.Blue ? "RedUnit" : "BlueUnit"; 
        enemies = GameObject.FindGameObjectsWithTag(enemyTag); // 查找所有敌人
    }

    void UpdateTarget()
    {
        FindEnemies(); 
        if (enemies.Length == 0)
        {
            target = null;
            return; 
        }

        SelectClosestEnemy();
    }


    void SelectClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) continue; 

            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy;
            //Debug.Log("当前目标敌人: " + closestEnemy.name);
        }
        else
        {
            target = null; 
        }
    }
    void MoveTowardsTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;

        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
        }

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void StopAndShoot()
    {
        if (fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = fireRate; 
        }
        else
        {
            fireCooldown -= Time.deltaTime; 
        }
    }

    private bool isAlternate = false;
    void Shoot()
    {
        if (bulletPrefab != null)
        {
            if (secondBulletAnchor != null)
            {
                // 交替发射
                if (isAlternate)
                {
                    // 创建随机角度偏移
                    float randomAngle = Random.Range(-5f, 5f); // 随机角度范围，单位是度
                    Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle); // Z轴上旋转随机角度

                    GameObject bullet = Instantiate(bulletPrefab, secondBulletAnchor.position, secondBulletAnchor.rotation * randomRotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = (secondBulletAnchor.right + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f)).normalized * 10f;
                    }
                }
                else
                {
                    // 创建随机角度偏移
                    float randomAngle = Random.Range(-5f, 5f); // 随机角度范围
                    Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle); // Z轴上旋转随机角度

                    GameObject bullet = Instantiate(bulletPrefab, bulletAnchor.position, bulletAnchor.rotation * randomRotation);
                    Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                    if (rb != null)
                    {
                        rb.velocity = (bulletAnchor.right + new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0f)).normalized * 10f;
                    }
                }
                isAlternate = !isAlternate;
            }

            else
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletAnchor.position, bulletAnchor.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = bulletAnchor.right * 10f;
                }
            }
        }
    }
    public void ManualRefreshTarget()
    {
        SetColorAndTag();

        FindEnemies();
    }
}
