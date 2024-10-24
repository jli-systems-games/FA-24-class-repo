using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class BackButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Canvas homeCanvas, myCanvas;
    public CinemachineVirtualCamera homeCamera, myCamera; 
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
        ChangeTextColor(normalColor);
        audioManager.PlayWaterPickup();

        myCanvas.gameObject.SetActive(false);  
        homeCanvas.enabled = true;             

        myCamera.Priority = 10;
        homeCamera.Priority = 20; 
    }

    private void ChangeTextColor(Color color)
    {
        textMeshPro.color = color;
    }
}
