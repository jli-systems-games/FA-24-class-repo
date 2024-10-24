using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPage1;
    public GameObject tutorialPage2;
    public GameObject tutorialPage3;

    private int currentPage;

    void Start()
    {
        currentPage = 1;
        UpdateTutorialPages();
    }

    public void Next()
    {
        if (currentPage < 3)
        {
            currentPage++;
            UpdateTutorialPages();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            UpdateTutorialPages();
        }
    }

    private void UpdateTutorialPages()
    {
        tutorialPage1.SetActive(currentPage == 1);
        tutorialPage2.SetActive(currentPage == 2);
        tutorialPage3.SetActive(currentPage == 3);
    }

    public void Begin()
    {
        SceneManager.LoadScene(2);
    }
}
