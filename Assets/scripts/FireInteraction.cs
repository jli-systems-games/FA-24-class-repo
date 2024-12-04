using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireInteraction : MonoBehaviour
{
    public GameObject fireEffect;
    public InfiniteMatches matchesScript;
    public AudioSource fireSound;

    public GameObject firePrompt;
    public GameObject matchUnlit;

    private bool isFireLit = false;
    private bool isInRange = false;

    private Collider fireCollider;

    private void Start()
    {
        if (firePrompt != null)
        {
            firePrompt.SetActive(false);
            matchUnlit.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;

            // shows ui prompt when player in range, but only if the match is lit
            if (firePrompt != null && !matchUnlit.activeInHierarchy)
            {
                firePrompt.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;

            if (firePrompt != null)
            {
                firePrompt.SetActive(false);
            }

            if (matchUnlit != null)
            {
                matchUnlit.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.R))
        {
            // match unlit
            if (matchesScript != null && !matchesScript.IsMatchLit())
            {
                UnlitPrompt();
            }
            else
            {
                LightFire();
            }
        }
    }

    void UnlitPrompt()
    {
        if (matchUnlit != null)
        {
            matchUnlit.SetActive(true);
            firePrompt.SetActive(false);
        }
    }

    void LightFire()
    {
        if (matchesScript != null && matchesScript.IsMatchLit() && !isFireLit)
        {
            if (fireEffect != null)
            {
                fireEffect.SetActive(true);
            }

            if (fireSound != null)
            {
                fireSound.Play();
            }

            isFireLit = true;
            firePrompt.SetActive(false);

            if (matchUnlit != null)
            {
                matchUnlit.SetActive(false);
            }

            // disable collider to prevent interactions
            fireCollider = GetComponent<Collider>();
            if (fireCollider != null)
            {
                fireCollider.enabled = false;
            }
        }
    }
}