using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Match : MonoBehaviour
{
    public Slider matchBar;
    public float matchDuration = 5f;
    private float matchTimer;
    public int totalMatches = 5;
    public Image[] matchIcons;

    public Light[] matchLights;
    public GameObject matchAnimation;

    private bool isMatchLit = false;

    void Start()
    {
        matchBar.maxValue = matchDuration;
        matchBar.value = 0;
        UpdateMatchAmount();

        ToggleFire(false);
    }

    void LightMatch()
    {
        if (isMatchLit) return;

        totalMatches--;
        UpdateMatchAmount();

        matchTimer = matchDuration;
        matchBar.value = matchDuration;
        isMatchLit = true;

        ToggleFire(true);
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

        if (Input.GetKeyDown(KeyCode.E) && totalMatches > 0)
        {
            LightMatch();
        }
    }

    void UpdateMatchAmount()
    {
        for (int i = 0; i < matchIcons.Length; i++)
        {
            if (i < totalMatches)
            {
                matchIcons[i].enabled = true;
            }
            else
            {
                matchIcons[i].enabled = false;
            }
        }
    }

    void ToggleFire(bool state)
    {
        foreach (Light light in matchLights)
        {
            if (light != null)
            {
                light.enabled = state;
            }

            if (matchAnimation != null)
            {
                matchAnimation.SetActive(state);
            }
        }
    }
}
