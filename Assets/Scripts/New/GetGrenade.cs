using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGrenade : MonoBehaviour
{
    public GameObject ui;
    public NewGunManager newGunManager;
    private bool inTrigger = false;
    void Start()
    {
        ui.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            newGunManager.AddGrenade();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ui.SetActive(true);
            inTrigger = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
            ui.SetActive(false);
        }
    }
}
