using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    public float health;
    public GameObject dieEffect;
    public UnitType unitType;
    public bool dontDestroyWhenDie = false;
    public enum UnitType
    { 
     Home,
     Tank,
     Infantry
    }
    public void DecreaseHealth(float damage)
    {
        if (damage < health)
        {
            health -= damage;
        }
        else if (!dontDestroyWhenDie)
        {
            Destroy(gameObject);
        }
        else
            health = -1;
    }
    private void OnDestroy()
    {
        Instantiate(dieEffect, transform.position,Quaternion.identity);
    }
}
