using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour
{
    private GameManager gameManager;

    public int HP;
    public Inventory item;
    private Slider thisHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        thisHealthSlider = GetComponentInChildren<Slider>();
        thisHealthSlider.maxValue = HP;
        thisHealthSlider.value = HP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        if (damage >= 0)
        {
            HP = HP - damage;
        }
        else
        {
            HP = HP - 1;
        }

        thisHealthSlider.value = HP;

        if (HP <= 0)
        {
            gameManager.addToInventory(item);
            Destroy(gameObject);
        }
    }
}
