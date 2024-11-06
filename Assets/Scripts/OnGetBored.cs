using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGetBored : MonoBehaviour
{
    public Color boredSkyboxColor = Color.red;
    private Color defaultSkyboxColor;
    private Camera mainCamera;
    private NewEventSim sim;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            defaultSkyboxColor = mainCamera.backgroundColor; 
        }

        sim = FindObjectOfType<NewEventSim>();

        if (sim != null)
        {
            sim.onGetBored.AddListener(ChangeSkyboxColor); 
        }
        else
        {
            Debug.LogError("eventsim missing");
        }
    }

    public void ChangeSkyboxColor()
    {
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = boredSkyboxColor; 
        }
    }

    public void ResetSkyboxColor()
    {
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = boredSkyboxColor; 
        }
    }

    void OnDestroy()
    {
        if (sim != null)
        {
            sim.onGetBored.RemoveListener(ChangeSkyboxColor); 
        }
    }
}
