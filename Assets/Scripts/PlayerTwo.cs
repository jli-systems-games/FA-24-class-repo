using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerTwo : MonoBehaviour
{
    public GameObject playerTwo;

    public int health;
    public int attack;
    public int speed;
    public int defense;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI defenseText;
    public TextMeshProUGUI speedText;

    [SerializeField] public Slider CPUSlider;

    public int maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        health = 200;
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
        attack = Random.Range(14, 26);
        speed = Random.Range(10, 21);
        defense = Random.Range(5, 10);
    }


    public void UpdateCPUBar(int health, int maxHealth)
    {
        health = Mathf.Clamp(health, 0, maxHealth);  // Ensure hunger doesn't exceed max value

        CPUSlider.value = (float)health / maxHealth; // Normalize the value (0 to 1 range)
    }

    internal void UpdateCPUBar(object health, object maxHealth)
    {
        throw new System.NotImplementedException();
    }
}
