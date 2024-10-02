using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnClick : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager != null)
        {
            if (gameManager.IsGamePaused())
            {
                DisableAllBoxColliders();
                this.enabled = false;
            }
            else
            {
                EnableAllBoxColliders();
                this.enabled = true;
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
        }
    }

    private void DisableAllBoxColliders()
    {
        BoxCollider[] colliders = FindObjectsOfType<BoxCollider>(); // finds all box colliders
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }

    private void EnableAllBoxColliders()
    {
        BoxCollider[] colliders = FindObjectsOfType<BoxCollider>();
        foreach (var collider in colliders)
        {
            collider.enabled = true;
        }
    }
}
