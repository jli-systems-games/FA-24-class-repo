using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGetDirty : MonoBehaviour
{
    public Color dirtySkyboxColor = Color.red;
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
            sim.onGetDirty.AddListener(ChangeSkyboxColor); 
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
            mainCamera.backgroundColor = dirtySkyboxColor; 
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
            sim.onGetDirty.RemoveListener(ChangeSkyboxColor); 
        }
    }
}
