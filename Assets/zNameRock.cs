using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class zNameRock : MonoBehaviour
{

    public InputField usernameInput;
    public static string rockName;

    // Start is called before the first frame update
    void Start()
    {
        usernameInput.text = PlayerPrefs.GetString("saveNom");
    }

    public void SaveUsername(string newName)
    {
        rockName = newName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
