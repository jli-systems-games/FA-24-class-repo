using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject[] canvases;
    private int currentIndex = 0;

    public Button leftArrowButton;
    public Button rightArrowButton;

    void Start()
    {
        leftArrowButton.onClick.AddListener(OnLeftArrowClick);
        rightArrowButton.onClick.AddListener(OnRightArrowClick);

        UpdateCanvas();
    }

    void OnLeftArrowClick()
    {
        currentIndex = (currentIndex - 1 + canvases.Length) % canvases.Length;
        UpdateCanvas();
    }

    void OnRightArrowClick()
    {
        currentIndex = (currentIndex + 1) % canvases.Length;
        UpdateCanvas();
    }

    void UpdateCanvas()
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == currentIndex);
        }
    }
}
