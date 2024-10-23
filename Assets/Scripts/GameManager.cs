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
    public static int cha;
    public static int str;
    public static int dex;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        intelligence = 1;
        cha = 1;
        str = 1;
        dex = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
