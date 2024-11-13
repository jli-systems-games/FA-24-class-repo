using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleBase : MonoBehaviour
{
    public ObstaclesStats _stats;
    public string id;

    Image healthBar;

    int currentHealth;
    void Start()
    {
        currentHealth = _stats.Health;
        healthBar = gameObject.GetComponentsInChildren<Image>().FirstOrDefault(c => c.name == "health");
        EventManager.gotHit += DeductHealth;

    }

    void DeductHealth(string request, int d)
    {
        if (request == id)
        {
            currentHealth -= d;
            healthBar.fillAmount = (float)currentHealth / (float)_stats.Health;
            
           
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                return;
            }

        }
    }
}
