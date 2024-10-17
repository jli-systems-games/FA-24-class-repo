using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rock : MonoBehaviour
{
    private GameManager gameManager;

    public int HP;
    public Inventory item;

    public Slider[] healthSliders;

    public Slider thisHealthSlider;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        thisHealthSlider.maxValue = HP;
        thisHealthSlider.value = HP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for(int i = 0; i < healthSliders.Length; i++)
        {
            healthSliders[i].gameObject.SetActive(false);
        }

        thisHealthSlider.gameObject.SetActive(true);
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
            thisHealthSlider.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
