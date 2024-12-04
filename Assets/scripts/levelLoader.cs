using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class levelLoader : MonoBehaviour
{

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
    public Button level12Button;
    public Button level13Button;
    public Button level14Button;
    public Button level15Button;
    public Button level16Button;
    public Button level17Button;
    public Button level18Button;
    public Button level19Button;
    public Button level20Button;

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
        level12Button.interactable = false;
        level13Button.interactable = false;
        level14Button.interactable = false;
        level15Button.interactable = false;
        level16Button.interactable = false;
        level17Button.interactable = false;
        level18Button.interactable = false;
        level19Button.interactable = false;
        level20Button.interactable = false;
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

        if (key.level12Key == true)
        {
            level12Button.interactable = true;
        }

        if (key.level13Key == true)
        {
            level13Button.interactable = true;
        }

        if (key.level14Key == true)
        {
            level14Button.interactable = true;
        }

        if (key.level15Key == true)
        {
            level15Button.interactable = true;
        }

        if (key.level16Key == true)
        {
            level16Button.interactable = true;
        }

        if (key.level17Key == true)
        {
            level17Button.interactable = true;
        }

        if (key.level18Key == true)
        {
            level18Button.interactable = true;
        }

        if (key.level19Key == true)
        {
            level19Button.interactable = true;
        }

        if (key.level20Key == true)
        {
            level20Button.interactable = true;
        }
    }

    public void loadLevel(string levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
}
