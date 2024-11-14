using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damageToHome = 1f;
    public float damageToEnemy = 1f;
    public float damageToEnemyTank = 1f;
    public float dieTime = 1f;
    public GameObject dieEffect;
    private UnitHealth unitHealth;
    void Start()
    {
        Destroy(gameObject, dieTime);
    }   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedUnit") || collision.gameObject.CompareTag("BlueUnit"))
        {
            unitHealth = collision.gameObject.GetComponent<UnitHealth>();
            switch (unitHealth.unitType)
            {
                case UnitHealth.UnitType.Home:
                    unitHealth.DecreaseHealth(damageToHome);
                    break;
                case UnitHealth.UnitType.Tank:
                    unitHealth.DecreaseHealth(damageToEnemyTank);
                    break;
                case UnitHealth.UnitType.Infantry:
                    unitHealth.DecreaseHealth(damageToEnemy);
                    break;
            }           
        }
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        Instantiate(dieEffect,transform.position,Quaternion.identity);
    }

}
