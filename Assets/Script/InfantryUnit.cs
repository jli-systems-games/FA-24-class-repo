using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfantryUnit : MonoBehaviour
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
    public Transform bulletAnchor;

    private GameObject[] enemies;
    private GameObject target; // 当前目标
    public JumpAnimation jumpAnimation;
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
            Vector3 direction = (target.transform.position - bulletAnchor.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction) * Quaternion.Euler(0, 0, 90);
            bulletAnchor.rotation = targetRotation;
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
        jumpAnimation.isMoving = true;

        Vector3 direction = (target.transform.position - transform.position).normalized;      

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void StopAndShoot()
    {
        jumpAnimation.isMoving = false;
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

    void Shoot()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletAnchor.position, bulletAnchor.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = bulletAnchor.right * 10f;
            }
        }

    }
    public void ManualRefreshTarget()
    {
        SetColorAndTag();

        FindEnemies();
    }
}
