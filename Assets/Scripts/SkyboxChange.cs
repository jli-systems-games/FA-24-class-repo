using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxChange : MonoBehaviour
{
    public Material daySkybox;
    public Material nightSkybox;
    public Light directionalLight;
    public Color dayLightColor = Color.white;
    public Color nightLightColor = Color.gray;
    public float dayIntensity = 1f;
    public float nightIntensity = 0.3f;

    private bool isDay = true;

    // Start is called before the first frame update
    void Start()
    {
        ToggleLights(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (isDay)
            {
                SetNight();
            }
            else
            {
                SetDay();
            }

            isDay = !isDay;
        }
    }

    void SetDay()
    {
        RenderSettings.skybox = daySkybox;
        directionalLight.color = dayLightColor;
        directionalLight.intensity = dayIntensity;
        DynamicGI.UpdateEnvironment();
        ToggleLights(false);
    }

    // Switch to nighttime settings
    void SetNight()
    {
        RenderSettings.skybox = nightSkybox;
        directionalLight.color = nightLightColor;
        directionalLight.intensity = nightIntensity;
        DynamicGI.UpdateEnvironment();
        ToggleLights(true);
    }

    void ToggleLights(bool turnOn)
    {
        GameObject[] lightObjects = GameObject.FindGameObjectsWithTag("light");
        foreach (GameObject obj in lightObjects)
        {
            Light light = obj.GetComponent<Light>();
            if (light != null)
            {
                light.enabled = turnOn;
            }
        }
    }
}