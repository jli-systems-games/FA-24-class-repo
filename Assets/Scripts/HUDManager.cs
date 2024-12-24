using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // HUD elements
    public Image hudImage; 
    public Sprite[] hudStages; 

    public void UpdateHUD(int stage)
    {
        if (hudStages != null && hudStages.Length >= stage)
        {
            hudImage.sprite = hudStages[stage - 1];
        }
    }
}
