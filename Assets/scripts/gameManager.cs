using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public beybladeMovement stamina1;
    public beybladeMovement2 stamina2;

    public bool winnerDeclared;

    public TMP_Text winnerText;

    public float blueStamina;
    public float pinkStamina;

    public GameObject restartButton;

    void Update()
    {
        GameObject beyblade1 = GameObject.FindWithTag("Beyblade1");
        GameObject beyblade2 = GameObject.FindWithTag("Beyblade2");

        stamina1 = beyblade1.GetComponent<beybladeMovement>();
        stamina2 = beyblade2.GetComponent<beybladeMovement2>();

        blueStamina = stamina1.stamina;
        pinkStamina = stamina2.stamina;

        if (blueStamina < 0 && winnerDeclared == false)
        {
            winnerDeclared = true;
            winnerText.text = "pink wins";
            restartButton.gameObject.SetActive(true);
        }
        if (pinkStamina < 0 && winnerDeclared == false)
        {
            winnerDeclared = true;
            winnerText.text = "blue wins";
            restartButton.gameObject.SetActive(true);
        }
        
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
