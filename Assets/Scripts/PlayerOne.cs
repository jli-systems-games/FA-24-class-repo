using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerOne : MonoBehaviour { 

    public GameObject playerOne;

    public int health;
    public int attack;
    public int speed;
    public int defense;

    public int maxHealth;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI speedText;

    [SerializeField] public Slider playerSlider;



    // Start is called before the first frame update
    void Start()
    {
        health = 200;
        maxHealth = health;
        RandomStats();
     
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = ("Health: ") + health.ToString();
        attackText.text = ("Attack: ") + attack.ToString();
        defenseText.text = ("Defense: ") + defense.ToString();
        speedText.text = ("Speed: ") + speed.ToString();

      
    }

    public void RandomStats()
    {
        attack = Random.Range(11, 22);
        speed = Random.Range(10, 21);
        defense = Random.Range(5, 10);
    }

    public void UpdatePlayerBar(int health, int maxHealth)
    {
        health = Mathf.Clamp(health, 0, maxHealth);  // Ensure hunger doesn't exceed max value

        playerSlider.value = (float)health / maxHealth; // Normalize the value (0 to 1 range)
    }

}
