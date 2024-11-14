using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOne : MonoBehaviour
{

    [SerializeField] private Slider slider;

    public float p1_hp;
    public float maxHp;

    public GameObject Player1;

    // Start is called before the first frame update
    void Start()
    {
        p1_hp = maxHp;
        UpdateBar(p1_hp, maxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (p1_hp <= 0)
        {
            Player1.SetActive(false);
        }
    }

    public void UpdateBar(float p1_hp, float maxHp)
    {
        p1_hp = Mathf.Clamp(p1_hp, 0f, maxHp);  // Ensure hunger doesn't exceed max value

        slider.value = p1_hp / maxHp; // Normalize the value (0 to 1 range)
    }

    //public void Feed()
    //{
    //    float food = Random.Range(5f, 10f);  // Random value between 15 and 40
    //    hunger += food;
    //    hunger = Mathf.Clamp(hunger, 0f, maxHunger);  // Ensure hunger doesn't exceed max value
    //    UpdateBar(hunger, maxHunger);  // Update the slider
    //    Debug.Log($"Hunger increased by: {food}. New hunger value: {hunger}");
    //}
}
