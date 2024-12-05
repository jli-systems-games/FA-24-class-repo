using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarWinState : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject itemsNeeded;
    public TextMeshProUGUI pressF;
    public float promptDuration = 3f;

    private bool isplayerInRange = false;

    private void Start()
    {
        winPanel.SetActive(false);
        itemsNeeded.SetActive(false);
        pressF.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayerInRange = true;

            if (CollectItems.item1 && CollectItems.item2)
            {
                pressF.gameObject.SetActive(true);
                itemsNeeded.SetActive(false);
            }
            else
            {
                itemsNeeded.SetActive(true);
                pressF.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isplayerInRange = false;

            itemsNeeded.SetActive(false);
            pressF.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isplayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (CollectItems.item1 && CollectItems.item2)
            {
                winPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}
