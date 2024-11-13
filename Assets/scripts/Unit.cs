using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public UnitStats stats;
    public int currentHealth;
    private float attackCooldown;

    public GameObject healthBarPrefab;
    private Slider healthBarSlider;

    private void Start()
    {
        attackCooldown = 1f / stats.attackSpeed;

        // initialize health
        currentHealth = stats.health;

        // instantiates health bar
        GameObject healthBar = Instantiate(healthBarPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity, transform);
        healthBarSlider = healthBar.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = stats.health;
        healthBarSlider.value = currentHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Despawn();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }
    }

    private void Despawn()
    {
        Destroy(gameObject);
        // can trigger other effects
    }

    public void Attack(Unit target)
    {
        if (target != null)
        {
            target.TakeDamage(stats.attackPower);
        }
    }
}
