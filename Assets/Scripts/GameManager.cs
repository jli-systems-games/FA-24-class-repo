using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PersonalityTrait
{
    Cocky,
    Impulsive,
    Cautious,
    Friendly
}
public class GameManager : MonoBehaviour
{
    public static string playerName;

    public static PersonalityTrait trait1;
    public static PersonalityTrait trait2;

    public static int intelligence;
    public static int charisma;
    public static int strength;
    public static int dexterity;

    [SerializeField] private InputField InputField;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeName()
    {
        playerName = InputField.text;
    }
}
