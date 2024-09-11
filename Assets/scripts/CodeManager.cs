using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeManager : MonoBehaviour
{
    
    public TextMeshProUGUI RandomCode; 
    private string randomCode;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        //_gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        //Debug.Log("CodeManagerStart");
        GenerateAndDisplayRandomCode();
    }

    
    private void GenerateAndDisplayRandomCode()
    {
        
        int code = Random.Range(1000, 10000); 

        
        randomCode = code.ToString("D4");

        
        if (RandomCode != null)
        {
            RandomCode.text = randomCode;
        }
        else
        {
            Debug.LogError("RandomCode TextMeshProUGUI is not assigned.");
        }
    }

   
    public string GetRandomCode()
    {
        return randomCode;

    }


}
