using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGetTired : MonoBehaviour
{
    public Color tiredSkyboxColor = Color.red;
    private Color defaultSkyboxColor;
    private Camera mainCamera;
    private Event_Sim sim;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            defaultSkyboxColor = mainCamera.backgroundColor; 
        }

        sim = FindObjectOfType<Event_Sim>();

        if (sim != null)
        {
            sim.onGetTired.AddListener(ChangeSkyboxColor); 
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
            mainCamera.backgroundColor = tiredSkyboxColor; 
        }
    }

    public void ResetSkyboxColor()
    {
        if (mainCamera != null)
        {
            mainCamera.backgroundColor = defaultSkyboxColor; 
        }
    }

    void OnDestroy()
    {
        if (sim != null)
        {
            sim.onGetTired.RemoveListener(ChangeSkyboxColor); 
        }
    }
}
