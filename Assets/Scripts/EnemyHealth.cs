using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public bool isInvencible;
    private bool isDying = false;

    public Material damageMaterial;  // 受伤时的材质
    private Material[] normalMaterials;  // 存储敌人各部分的原始材质
    public Renderer[] enemyRenderers;  // 敌人身上所有需要更改材质的 Renderer 组件
    private NewGunManager gameManager;
    private EnemyController enemyController;

    private Collider unitCollider;

    private void Start()
    {
        enemyController = GetComponentInParent<EnemyController>();
        unitCollider = GetComponent<Collider>();
        gameManager = FindObjectOfType<NewGunManager>();

        // 存储敌人正常状态的材质
        normalMaterials = new Material[enemyRenderers.Length];
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            normalMaterials[i] = enemyRenderers[i].material;
        }

        // 根据是否无敌设置初始血量
        if (isInvencible)
        {
            health = 99999;
        }
        else
        {
            float randomMultiplier = Random.Range(0.5f, 1.5f);
            health *= randomMultiplier;
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvencible)
        {
            health = 99999;
        }

        if (health <= damage && !isDying)
        {
            Die();
        }
        else if (health > damage)
        {
            health -= damage;
            StartCoroutine(ShowDamageEffect());

            if (enemyController != null)
            {
                StartCoroutine(enemyController.StopMove());
            }
        }
    }

    IEnumerator ShowDamageEffect()
    {
        foreach (Renderer renderer in enemyRenderers)
        {
            renderer.material = damageMaterial;
        }

        // 等待0.1秒后还原正常材质
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material = normalMaterials[i];
        }
    }

    void Die()
    {
        isDying = true;
        gameManager.AddMobKill();
        unitCollider.enabled = false;
        enemyController.Die();
    }

    public void GameEndKill()
    {
        isDying = true;
        unitCollider.enabled = false;
        enemyController.Die();
    }
}
