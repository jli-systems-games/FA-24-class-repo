using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOne : MonoBehaviour
{

    [SerializeField] private Slider slider;
    public TextMeshProUGUI displayText;


    public float p1_hp;
    public float maxHp;

    public GameObject Player1;

    public List<Vector3> locations = new();


    // Start is called before the first frame update
    void Start()
    {
        Player1.transform.position = locations[0];

        p1_hp = maxHp;
        UpdateBar(p1_hp, maxHp);
    }

    public void StartPositions()
    {
        Player1.transform.position = locations[0];
    }

    public void PlayPositions()
    {
        Player1.transform.position = locations[1];
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

        displayText.text = p1_hp.ToString();
    }
}