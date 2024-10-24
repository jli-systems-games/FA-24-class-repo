using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SratrGame : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{    
    private TextMeshProUGUI textMeshPro;
    private AudioManager audioManager;
    private Color normalColor = Color.white;
    private Color hoverColor = Color.red;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        textMeshPro = GetComponent<TextMeshProUGUI>();

        textMeshPro.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeTextColor(hoverColor);
        audioManager.PlayMedicPickup();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ChangeTextColor(normalColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ChangeTextColor(hoverColor);
        audioManager.PlayWaterPickup();
        
        SceneManager.LoadScene("Home");

    }

    private void ChangeTextColor(Color color)
    {
        textMeshPro.color = color;
    }
}
