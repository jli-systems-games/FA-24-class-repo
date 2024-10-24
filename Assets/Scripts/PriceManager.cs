using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PriceManager : MonoBehaviour
{
    public Slider priceSlider;
    public TextMeshProUGUI priceText;

    private float minPrice = 0f;
    private float maxPrice = 10f;
    private float priceIncrement = 0.25f;

    private void Start()
    {
        priceSlider.minValue = minPrice;
        priceSlider.maxValue = maxPrice;
        priceSlider.value = minPrice;

        UpdatePriceText();
        priceSlider.onValueChanged.AddListener(OnPriceSliderChange);
    }

    private void OnPriceSliderChange(float value)
    {
        float roundedValue = Mathf.Round(value / priceIncrement) * priceIncrement;
        priceSlider.value = roundedValue;
        UpdatePriceText();
    }

    private void UpdatePriceText()
    {
        priceText.text = "$" + priceSlider.value.ToString("F2");
    }
}
