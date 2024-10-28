using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplyCustomization : MonoBehaviour
{
    public SpriteRenderer knifeBaseRenderer;
    public SpriteRenderer colorOverlayRenderer;
    public SpriteRenderer engravingOverlayRenderer;

    private void Start()
    {
        // Apply customization data from CustomizationData
        if (CustomizationData.instance != null)
        {
            knifeBaseRenderer.sprite = CustomizationData.instance.selectedKnifeBase;

            // Apply overlays if they exist
            colorOverlayRenderer.sprite = CustomizationData.instance.selectedColorOverlay;
            engravingOverlayRenderer.sprite = CustomizationData.instance.selectedEngravingOverlay;

            // Enable overlay renderers only if they have a sprite
            colorOverlayRenderer.enabled = colorOverlayRenderer.sprite != null;
            engravingOverlayRenderer.enabled = engravingOverlayRenderer.sprite != null;
        }
    }
}
