using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{

    private static int skillPoints;

    [SerializeField] private TMP_InputField InputField;
    [SerializeField] private TMP_Dropdown traitOneSelector;
    [SerializeField] private TMP_Dropdown traitTwoSelector;

    [SerializeField] private TextMeshProUGUI skillPtDisplay;

    [SerializeField] private TextMeshProUGUI intDisplay;
    [SerializeField] private TextMeshProUGUI chaDisplay;
    [SerializeField] private TextMeshProUGUI strDisplay;
    [SerializeField] private TextMeshProUGUI dexDisplay;

    // Start is called before the first frame update
    void Start()
    {
        skillPoints = 5;

        ChangeName();
        ChangeTraitOne();
        ChangeTraitTwo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeName()
    {
        GameManager.playerName = InputField.text;

        Debug.Log("name: " + GameManager.playerName);
    }

    public void ChangeTraitOne()
    {
        if (traitOneSelector.value == 0)
        {
            GameManager.trait1 = PersonalityTrait.Cocky;
        }
        else if (traitOneSelector.value == 1)
        {
            GameManager.trait1 = PersonalityTrait.Friendly;
        }

        Debug.Log("trait 1: " + GameManager.trait1);
    }

    public void ChangeTraitTwo()
    {
        if (traitTwoSelector.value == 0)
        {
            GameManager.trait2 = PersonalityTrait.Cautious;
        }
        else if (traitTwoSelector.value == 1)
        {
            GameManager.trait2 = PersonalityTrait.Impulsive;
        }

        Debug.Log("trait 2: " + GameManager.trait2);
    }

    public void AddSkillPoint(int stat)
    {
        if (skillPoints > 0)
        {
            if (stat == 0)
            {
                GameManager.intelligence++;
                intDisplay.text = "Intelligence: " + GameManager.intelligence.ToString();
            }
            else if(stat == 1)
            {
                GameManager.cha++;
                chaDisplay.text = "Charisma: " + GameManager.cha.ToString();
            }
            else if(stat == 2)
            {
                GameManager.str++;
                strDisplay.text = "Strength: " + GameManager.str.ToString();
            }
            else if(stat == 3)
            {
                GameManager.dex++;
                dexDisplay.text = "Dexterity: " + GameManager.dex.ToString();
            }
            skillPoints--;
            skillPtDisplay.text = "Skill Points: " + skillPoints.ToString();
        }
    }

    public void RemoveSkillPoint(int stat)
    {
        if(stat == 0)
        {
            GameManager.intelligence--;
            intDisplay.text = "Intelligence: " + GameManager.intelligence.ToString();
        }
        else if(stat == 1)
        {
            GameManager.cha--;
            chaDisplay.text = "Charisma: " + GameManager.cha.ToString();
        }
        else if (stat == 2)
        {
            GameManager.str--;
            strDisplay.text = "Strength: " + GameManager.str.ToString();
        }
        else if( stat == 3)
        {
            GameManager.dex--;
            dexDisplay.text = "Dexterity: " + GameManager.dex.ToString();
        }

        skillPoints++;
        skillPtDisplay.text = "Skill Points: " + skillPoints.ToString();
    }
}
