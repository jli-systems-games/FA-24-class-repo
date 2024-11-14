using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour
{
    public Transform healthBar;  
    private UnitHealth unitHealth; 
    public Canvas endGameCanvas;  
    private float maxHealth = 100;  
    private bool gameEnded = false; 

    void Start()
    {
        
        unitHealth = GetComponent<UnitHealth>();

        endGameCanvas.enabled = false;
    }

    void Update()
    {
        if (healthBar.gameObject != null)
        {
            if (unitHealth.health >= 0)
            {
                float healthPercent = unitHealth.health / maxHealth;

                healthBar.localScale = new Vector3(healthPercent, healthBar.localScale.y, healthBar.localScale.z);
            }
            //else
            //{
            //        Destroy(healthBar.gameObject);
            //}
        }
        

        if (unitHealth.health <= 0 && !gameEnded)
        {
            gameEnded = true;
            endGameCanvas.enabled = true;  
            Time.timeScale = 0;  
        }
    }
}
