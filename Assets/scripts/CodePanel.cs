using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodePanel : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI codeText;
    string codeTextValue = "";
    public TextMeshProUGUI EmergencyCall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        codeText.text = codeTextValue;
        

        if (codeTextValue.Length >= 4)
        {

            if (codeTextValue == "1234")
            {
                Debug.Log("IsCorrect");
                //isOpened = true;
            }
            else
            {
                Debug.Log("IsFalse");
            }

            codeTextValue = "";
        }
        
    }

    public void AddDigit(string digit)
    {
        codeTextValue += digit;
    }
}
