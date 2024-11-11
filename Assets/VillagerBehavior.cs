using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillagerBehavior : MonoBehaviour
{
    public VillagerStats _stats;
    public string id;

    TextMeshPro _healthUI;
    int currentHealth;

    void Start()
    {
        _stats.ids.Add(id);
        _healthUI = GetComponentInChildren<TextMeshPro>();
        currentHealth = _stats.Health;
        _healthUI.text = currentHealth.ToString();
        EventManager.gotHit += DeductHealth;
    }

    void DeductHealth(string requestid)
    {
        if(requestid == id)
        {   
            this.currentHealth--;
            this._healthUI.text = currentHealth.ToString();
            if(this.currentHealth == 0)
                    {
                        gameObject.SetActive(false);
                        return;
                    }
                   
        }
        

    }

    private void OnDisable()
    {
        EventManager.newTarget();
        _stats.ids.Remove(id);
    }
}
