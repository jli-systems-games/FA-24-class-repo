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

    private bool[] isCycling;
    private int[] directions; // 1 for positive, -1 for negative
    private int activeSliderIndex;

    public event Action<int> OnSliderStop;

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

        if (sliders.Length > 0 && buttons.Length > 0)
        {
            sliders[0].gameObject.SetActive(true);
            buttons[0].gameObject.SetActive(true);
        }

        OnSliderStop += StopSlider;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
    }

    private void CycleSliders(Slider slider, ref int direction)
    {
        slider.value += Time.deltaTime * direction;
        if (slider.value >= slider.maxValue)
        {
            direction = -1;
        }
        else if (slider.value <= slider.minValue)
        {
            direction = 1;
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
        }
    }


    private void OnDestroy()
    {
        OnSliderStop -= StopSlider;
    }

    public void ApplyAttributes()
    {
        Renderer ballRenderer = ball.GetComponent<Renderer>();
        Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();
        SphereCollider ballCollider = ball.GetComponent<SphereCollider>();

        if (ballRenderer != null && sliders[0].gameObject.activeSelf) // Color
        {
            float hue = sliders[0].value;
            Color color = Color.HSVToRGB(hue, 1, 1);
            ballRenderer.material.color = color;
        }

        if (ball != null && sliders[1].gameObject.activeSelf) // Size
        {
            float size = sliders[1].value;
            ball.transform.localScale = new Vector3(size, size, size);
        }

        if (ballRigidbody != null && sliders[2].gameObject.activeSelf) // Gravity
        {
            float gravityScale = sliders[2].value;
            ballRigidbody.useGravity = true;
            Physics.gravity = new Vector3(0, -gravityScale, 0);
        }

        if (ballCollider != null && sliders[3].gameObject.activeSelf) // Bounciness
        {
            PhysicMaterial material = ballCollider.material;
            if (material != null)
            {
                material.bounciness = sliders[3].value;
                material.bounciness = Mathf.Clamp(material.bounciness, 0f, 1f);
            }
        }
    }
}
