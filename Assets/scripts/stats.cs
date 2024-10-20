using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stats : MonoBehaviour
{
    public Slider iceSlider;
    public Slider saltSlider;
    public Slider waterSlider;

    public SpriteRenderer dirtyWater;
    public SpriteRenderer mossbrown;
    
    void Start()
    {
        iceSlider.value =  Random.Range(40f, 60f);
        saltSlider.value =  Random.Range(40f, 60f);
        waterSlider.value =  0f;

        StartCoroutine(iceStatTiming());
        StartCoroutine(saltStatTiming());
        StartCoroutine(waterStatTiming());
    }

    private IEnumerator iceStatTiming()
    {
        yield return new WaitForSeconds(3);
        iceSlider.value += 1f;
        StartCoroutine(iceStatTiming());
    }

    private IEnumerator saltStatTiming()
    {
        yield return new WaitForSeconds(5);
        saltSlider.value += 1f;
        StartCoroutine(saltStatTiming());
    }

    private IEnumerator waterStatTiming()
    {
        yield return new WaitForSeconds(5);
        waterSlider.value += 1f;
        StartCoroutine(waterStatTiming());
    }

    public void AddIce()
    {
        iceSlider.value -= 10;
    }

    public void AddSalt()
    {
        saltSlider.value -= 10;
    }

    public void cleanWater()
    {
        waterSlider.value -= 10;
    }

    void Update()
    {
        dirtyWater.color = new Color (1f, 1f, 1f, (waterSlider.value)/100f);
        if (saltSlider.value > iceSlider.value)
        {
            mossbrown.color = new Color (1f, 1f, 1f, (saltSlider.value)/100f);
        }
        if (iceSlider.value > saltSlider.value)
        {
            mossbrown.color = new Color (1f, 1f, 1f, (iceSlider.value)/100f);
        }
    }
}
