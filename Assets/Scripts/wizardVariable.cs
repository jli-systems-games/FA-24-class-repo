using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class wizardVariable : MonoBehaviour
{

    public GameObject wizard;
    public int attack;
    public int health;
    public int dodge; 
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;

    // Start is called before the first frame update
    void Start()
    {
        attackText.text = ("attack: ") + attack.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = ("health: ") + health.ToString();
    }
}
