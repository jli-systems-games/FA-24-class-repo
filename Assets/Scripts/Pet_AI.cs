using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_AI : MonoBehaviour
{
    private GameManager gameManager;

    public int hunger;
    public int strength;
    public int confidence;
    private int stomache;
    public int damage;
    private float criticalChance;
    private int criticalHit;

    public bool asleep;

    public Transform mouseLoc;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        stomache = 10;
        criticalHit = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!asleep)
        {
            stomache = 10 - hunger;
        }

        if(stomache <= 0)
        {
            asleep = true;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    public void calculateDamage()
    {
        criticalChance = Random.Range(0,100);

        if(criticalChance <= confidence)
        {
            criticalHit = 2;
        }

        else
        {
            criticalHit = 1;
        }

        damage = (strength - hunger) * criticalHit;
    }
}
