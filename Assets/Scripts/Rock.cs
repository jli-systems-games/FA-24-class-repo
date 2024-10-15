using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private GameManager gameManager;

    public int HP;
    public Inventory item;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(HP <= 0)
        {
            Destroy(gameObject);
        }
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
    }
}
