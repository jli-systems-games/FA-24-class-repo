using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfiniteMatches : MonoBehaviour
{
    public Slider matchBar;
    public float matchDuration = 5f;
    private float matchTimer;

    public Light[] matchLights;
    public GameObject matchAnimation;
    public AudioSource matchSound;

    private bool isMatchLit = false;

    void Start()
    {
        matchBar.maxValue = matchDuration;
        matchBar.value = 0;

        ToggleFire(false);
    }

    void LightMatch()
    { 
        matchTimer = matchDuration;
        matchBar.value = matchDuration;
        matchBar.maxValue = matchDuration;

        isMatchLit = true;

        ToggleFire(true);

        if (matchSound != null)
        {
            matchSound.Play();
        }
    }

    void MatchUnlit()
    {
        isMatchLit = false;
        matchBar.value = 0;

        ToggleFire(false);
    }

    void Update()
    {
        if (isMatchLit)
        {
            matchTimer -= Time.deltaTime;
            matchBar.value = matchTimer;

            if (matchTimer <= 0)
            {
                MatchUnlit();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && !isMatchLit)
        {
            LightMatch();
        }
    }

    void ToggleFire(bool state)
    {
        foreach (Light light in matchLights)
        {
                light.enabled = state;
        }

        if (matchAnimation != null)
        {
            matchAnimation.SetActive(state);
        }
    }

    public bool IsMatchLit()
    {
        return isMatchLit;
    }
}
