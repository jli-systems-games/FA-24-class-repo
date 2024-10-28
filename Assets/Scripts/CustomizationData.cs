using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizationData : MonoBehaviour
{
    public static CustomizationData instance;

    public int selectedKnifeIndex;        // Stores the index of the selected knife
    public Sprite selectedKnifeBase;      // Stores the base sprite for the knife
    public Sprite selectedColorOverlay;   // Stores the selected color overlay
    public Sprite selectedEngravingOverlay; // Stores the selected engraving overlay

    private void Awake()
    {
        // Ensure only one instance of CustomizationData exists across scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Keep this object between scenes
        }
    }

    // Method to set the selected knife parts
    public void SetCustomization(int knifeIndex, Sprite baseKnife, Sprite colorOverlay, Sprite engravingOverlay)
    {
        selectedKnifeIndex = knifeIndex;   // Set the selected knife index
        selectedKnifeBase = baseKnife;
        selectedColorOverlay = colorOverlay;
        selectedEngravingOverlay = engravingOverlay;
    }
}
