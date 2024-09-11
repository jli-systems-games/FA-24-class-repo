using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodePanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI codeText; 
    private string codeTextValue = ""; 
    public TextMeshProUGUI EmergencyCall;

    public GameObject win;

    private CodeManager codeManager;

    private GameManager _gameManager;


    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //Debug.Log("CodePanelStart");
        win.SetActive(false);

       
        codeManager = FindObjectOfType<CodeManager>();

        
        if (codeManager == null)
        {
            Debug.LogError("CodeManager not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        codeText.text = codeTextValue; 

        
        if (codeTextValue.Length >= 4)
        {
            
            if (codeManager != null && codeTextValue == codeManager.GetRandomCode())
            {
                Debug.Log("IsCorrect");

                _gameManager.currentGameIndex++;
                StartCoroutine(ShowWin());
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
        if (codeTextValue.Length < 4)
        {
            codeTextValue += digit;
        }
    }

    IEnumerator ShowWin()
    {
        win.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _gameManager.ChangeState(GameState.Transition);
    }
}
