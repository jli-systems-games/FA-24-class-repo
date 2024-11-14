using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        //EventManager.gotHit += DeductHealth;
        

    }

    public void DeductHealth( int d)
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
