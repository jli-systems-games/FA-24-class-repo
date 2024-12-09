using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectItems : MonoBehaviour
{
    public static bool item1 = false;
    public static bool item2 = false;

    public TextMeshProUGUI itemTracker;
    public TextMeshProUGUI pressF;
    public AudioSource collectSound;

    private bool playerInRange = false;

    private void Start()
    {
        UpdateItemTracker();
        pressF.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            if (!item1 && !item2)
            {
                item1 = true;
                UpdateItemTracker();
                pressF.gameObject.SetActive(false);
                StartCoroutine(PlaySound());
            }
            else if (!item1 && item2)
            {
                item1 = true;
                UpdateItemTracker();
                pressF.gameObject.SetActive(false);
                StartCoroutine(PlaySound());
            }
            else if (!item2 && item1)
            {
                item2 = true;
                UpdateItemTracker();
                pressF.gameObject.SetActive(false);
                StartCoroutine(PlaySound());
            }
        }
    }

    void UpdateItemTracker()
    {
        itemTracker.text = $"Car keys: {(item1 ? "Collected" : "Not Collected")}\n" +
                               $"Medkit: {(item2 ? "Collected" : "Not Collected")}";
    }

    IEnumerator PlaySound()
    {
        collectSound.Play();
        yield return new WaitForSeconds(collectSound.clip.length);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressF.gameObject.SetActive(true);
            playerInRange = true; // allows collection
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pressF.gameObject.SetActive(false);
            playerInRange = false; // no collection
        }
    }
}