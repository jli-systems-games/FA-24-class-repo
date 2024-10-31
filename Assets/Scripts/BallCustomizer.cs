using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BallCustomizer : MonoBehaviour
{
    public GameObject ball;

    [Header("UI Elements")]
    public Slider[] sliders;
    public Button[] buttons;
    public Button startButton;
    public Toggle colorToggle;

    private bool[] isCycling;
    private int[] directions; // 1 for positive, -1 for negative
    private int activeSliderIndex;

    public event Action<int> OnSliderStop;

    private Renderer ballRenderer;
    private bool isCyclingColors = false;
    private float colorCycleSpeed = 1.0f;

    private void Awake()
    {
        activeSliderIndex = 0;

        int sliderCount = sliders.Length;

        isCycling = new bool[sliderCount];
        directions = new int[sliderCount];

        for (int i = 0; i < sliderCount; i++)
        {
            sliders[i].gameObject.SetActive(false);
            buttons[i].gameObject.SetActive(false);

            isCycling[i] = true;
            directions[i] = 1;

            int index = i;
            buttons[i].onClick.AddListener(() => OnSliderStop?.Invoke(index));
        }

        startButton.interactable = false;

        colorToggle.gameObject.SetActive(false);

        if (sliders.Length > 0 && buttons.Length > 0)
        {
            sliders[0].gameObject.SetActive(true);
            buttons[0].gameObject.SetActive(true);
        }

        OnSliderStop += StopSlider;
    }

    void Start()
    {
        ballRenderer = ball.GetComponent<Renderer>();

        ballRenderer.material.SetFloat("_Metallic", 1.0f);
        ballRenderer.material.SetFloat("_Glossiness", 0.6f);

        colorToggle.onValueChanged.AddListener(OnColorToggleChanged);
    }

    void Update()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            if (isCycling[i])
            {
                CycleSliders(sliders[i], ref directions[i]);
            }
        }

        ApplyAttributes();

        if (isCyclingColors)
        {
            CycleColors();
        }
    }

    #region attribute sliders

    private void CycleSliders(Slider slider, ref int direction)
    {
        float fixedTimeToMax = 1.2f;

        float range = slider.maxValue - slider.minValue;
        float speed = range / fixedTimeToMax;

        slider.value += Time.deltaTime * direction * speed;
        if (slider.value >= slider.maxValue)
        {
            direction = -1;
            slider.value = slider.maxValue;
        }
        else if (slider.value <= slider.minValue)
        {
            direction = 1;
            slider.value = slider.minValue;
        }
    }

    private void StopSlider(int index)
    {
        if (index >= 0 && index < isCycling.Length)
        {
            isCycling[index] = false;
            activeSliderIndex = index;

            int nextIndex = index + 1;
            if (nextIndex < sliders.Length && nextIndex < buttons.Length)
            {
                sliders[nextIndex].gameObject.SetActive(true);
                buttons[nextIndex].gameObject.SetActive(true);
            }

            if (index == 3)
            {
                colorToggle.gameObject.SetActive(true);
            }

            if (AllAttributesSelected())
            {
                startButton.interactable = true;
            }
        }
    }

    private bool AllAttributesSelected()
    {
        foreach (bool cycling in isCycling)
        {
            if (cycling)
            {
                return false;
            }
        }
        return true;
    }

    private void OnDestroy()
    {
        OnSliderStop -= StopSlider;
    }

    #endregion

    public void ApplyAttributes()
    {
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        SphereCollider ballCollider = ball.GetComponent<SphereCollider>();

        if (ball != null && sliders[0].gameObject.activeSelf) // Size
        {
            float size = sliders[0].value;
            ball.transform.localScale = new Vector3(size, size, size);
        }

        if (ballRigidbody != null && sliders[1].gameObject.activeSelf) // Mass
        {
            float massValue = sliders[2].value;
            ballRigidbody.mass = Mathf.Clamp(massValue, 0.1f, 10f);
        }

        if (ballCollider != null && sliders[2].gameObject.activeSelf) // Bounciness
        {
            PhysicMaterial material = ballCollider.material;
            if (material != null)
            {
                material.bounciness = sliders[2].value;
                material.bounciness = Mathf.Clamp(material.bounciness, 0f, 10f);
            }
        }

        if (ballRigidbody != null && sliders[3].gameObject.activeSelf) // Drag
        {
            float dragValue = sliders[3].value;
            ballRigidbody.drag = Mathf.Clamp(dragValue, 0f, 0.5f);
        }

        if (ballRenderer != null && sliders[4].gameObject.activeSelf) // Color
        {
            float hue = sliders[4].value;
            Color color = Color.HSVToRGB(hue, 1, 1);
            ballRenderer.material.color = color;
        }
    }

    #region color cycling

    private void OnColorToggleChanged(bool isOn)
    {
        isCyclingColors = isOn;

        if (sliders.Length > 4)
        {
            isCycling[4] = !isOn;
        }

        if (!isOn)
        {
            ApplySliderColor();
        }
    }

    private void CycleColors()
    {
        float hue = Mathf.PingPong(Time.time * colorCycleSpeed, 1);
        Color color = Color.HSVToRGB(hue, 1, 1);
        ballRenderer.material.color = color;
    }

    private void ApplySliderColor()
    {
        if (ballRenderer != null && sliders[4].gameObject.activeSelf)
        {
            float hue = sliders[4].value;
            Color color = Color.HSVToRGB(hue, 1, 1);
            ballRenderer.material.color = color;
        }
    }

    #endregion
}