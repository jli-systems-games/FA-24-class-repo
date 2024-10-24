using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LemonadeStandName : MonoBehaviour
{
    public TMP_InputField lemonadeStandNameInput; 

    public void SaveName()
    {
        string standName = lemonadeStandNameInput.text;
        PlayerPrefs.SetString("LemonadeStandName", standName);
        PlayerPrefs.Save();
    }
}
