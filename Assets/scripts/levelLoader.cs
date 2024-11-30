using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelLoader : MonoBehaviour
{

    public bool level1Access;
    public bool level2Access;
    public bool level3Access;
    public bool level4Access;
    public bool level5Access;
    public bool level6Access;
    public bool level7Access;
    public bool level8Access;
    public bool level9Access;
    public bool level10Access;
    public bool level11Access;

    public Button level1Button;
    public Button level2Button;
    public Button level3Button;
    public Button level4Button;
    public Button level5Button;
    public Button level6Button;
    public Button level7Button;
    public Button level8Button;
    public Button level9Button;
    public Button level10Button;
    public Button level11Button;

    public gameManager key;

    void Start()
    {
        level2Button.interactable = false;
        level3Button.interactable = false;
        level4Button.interactable = false;
        level5Button.interactable = false;
        level6Button.interactable = false;
        level7Button.interactable = false;
        level8Button.interactable = false;
        level9Button.interactable = false;
        level10Button.interactable = false;
        level11Button.interactable = false;
    }

    void Update()
    {
        key = GameObject.FindWithTag("GameManager").GetComponent<gameManager>();

        if (key.level2Key == true)
        {
            level2Button.interactable = true;
        }

        if (key.level3Key == true)
        {
            level3Button.interactable = true;
        }

        if (key.level4Key == true)
        {
            level4Button.interactable = true;
        }

        if (key.level5Key == true)
        {
            level5Button.interactable = true;
        }

        if (key.level6Key == true)
        {
            level6Button.interactable = true;
        }

        if (key.level7Key == true)
        {
            level7Button.interactable = true;
        }

        if (key.level8Key == true)
        {
            level8Button.interactable = true;
        }

        if (key.level9Key == true)
        {
            level9Button.interactable = true;
        }

        if (key.level10Key == true)
        {
            level10Button.interactable = true;
        }

        if (key.level11Key == true)
        {
            level11Button.interactable = true;
        }
    }

    public void loadLevel(string levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
