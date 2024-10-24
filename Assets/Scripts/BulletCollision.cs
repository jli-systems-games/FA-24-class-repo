using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    public bool isGrenade = false;
    public float damage;
    public GameObject sparkleEffect;

    public int maxHitTime = 2;
    private int targetTouchedTime = 0;

    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isGrenade)
        { 
            if (sparkleEffect != null)
            {
                Instantiate(sparkleEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);           
        
        }
              
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isGrenade)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                targetTouchedTime++;

                if (sparkleEffect != null)
                {
                    Instantiate(sparkleEffect, transform.position, Quaternion.identity);
                }

                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);

                if (targetTouchedTime >= maxHitTime + 1)
                    Destroy(gameObject);

            }
        }
        else
        {
            if (other.gameObject.CompareTag("Enemy"))
            {              
                EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
                enemyHealth.TakeDamage(damage);
            }


        }
           
    }
}
