using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class PetManager : MonoBehaviour
{
    public Slider stat1, stat2, stat3;

    public GameObject gameOver;
    public GameObject statButtons;

    public Image ramenPet;
    public Sprite normalSprite;
    public Sprite stat1Sprite, stat2Sprite, stat3Sprite;
    public TMP_Text text;

    private bool stat1Low = false;
    private bool stat2Low = false;
    private bool stat3Low = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DrainStat(stat1, 2.5f, 1f));
        StartCoroutine(DrainStat(stat2, 1f, 1f));
        StartCoroutine(DrainStat(stat3, 0.8f, 1f));

        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DrainStat(Slider statBar,float interval, float amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Decrease the stat value if above 0
            if (statBar.value > 0)
                statBar.value -= amount;
            Debug.Log($"{statBar.name} {statBar.value}");

            if (statBar.value <= 5 && !isLowStatTriggered(statBar))
            {
                LowStatWarning(statBar);
            }
            else if (statBar.value > 5 && isLowStatTriggered(statBar))
            {
                ResetSprite(statBar);
            }

            // Check if the stat is depleted (value <= 0)
            if (statBar.value <= 0)
            {
                Debug.Log($"{statBar.name} empty");
                GameOver();
                yield break;
            }
        }
    }

    public void ReplenishStat(Slider statBar, float amount)
    {
        statBar.value = Mathf.Clamp(statBar.value + amount, 0, statBar.maxValue);
    }

    void GameOver()
    {
        statButtons.SetActive(false);
        gameOver.SetActive(true);
        Time.timeScale = 0;
    }

    #region Low Stat

    private bool isLowStatTriggered(Slider statBar)
    {
        if (statBar == stat1) return stat1Low;
        if (statBar == stat2) return stat2Low;
        if (statBar == stat3) return stat3Low;
        return false;
    }

    private void setLowStat(Slider statBar, bool value)
    {
        if (statBar == stat1) stat1Low = value;
        if (statBar == stat2) stat2Low = value;
        if (statBar == stat3) stat3Low = value;
    }

    private void LowStatWarning(Slider statBar)
    {
        setLowStat(statBar, true); // Mark warning as triggered

        if (statBar == stat1)
        {
            Debug.Log("too cold");
            ramenPet.sprite = stat1Sprite;
        }
        else if (statBar == stat2)
        {
            Debug.Log("too oily");
            ramenPet.sprite = stat2Sprite;
        }
        else if (statBar == stat3)
        {
            Debug.Log("freshness low");
            ramenPet.sprite = stat3Sprite;
        }
    }

    void ResetSprite(Slider statBar)
    {
        ramenPet.sprite = normalSprite;
        setLowStat(statBar, false);
    }

    #endregion

    void RestartGame()
    {

    }
}
