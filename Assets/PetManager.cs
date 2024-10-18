using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PetManager : MonoBehaviour
{
    public static PetManager Instance;

    [Header("Pet Sliders")]
    public Slider stat1;
    public Slider stat2;
    public Slider stat3;

    [Header("Low Stat")]
    public Image ramenPet;
    public Sprite normalSprite;
    public Sprite stat1Sprite, stat2Sprite, stat3Sprite, deadSprite;
    public TextMeshProUGUI warningText;

    [Header("Game Canvas")]
    public GameObject mainCanvas;
    public GameObject minigame1Canvas, minigame2Canvas, minigame3Canvas;

    [Header("Game Over UI")]
    public GameObject gameOver;
    public GameObject statButtons;

    private float stat1Value, stat2Value, stat3Value;
    private bool stat1Low = false;
    private bool stat2Low = false;
    private bool stat3Low = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1;
        StartCoroutine(DrainStat(stat1, 2.5f, 1f));
        StartCoroutine(DrainStat(stat2, 1.2f, 1f));
        StartCoroutine(DrainStat(stat3, 0.8f, 1f));

        ShowMainCanvas();
        gameOver.SetActive(false);
    }

    IEnumerator DrainStat(Slider statBar, float interval, float amount)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            if (mainCanvas.activeSelf && statBar.value > 0)
                statBar.value -= amount;

            if (statBar.value <= 5 && !isLowStatTriggered(statBar))
                LowStatWarning(statBar);

            else if (statBar.value > 5 && isLowStatTriggered(statBar))
                ResetSprite(statBar);

            if (statBar.value <= 0)
            {
                GameOver();
                yield break;
            }
        }
    }

    public void ReplenishStat(Slider statBar)
    {
        statBar.value = statBar.maxValue;
    }

    private void SaveStatValues()
    {
        stat1Value = stat1.value;
        stat2Value = stat2.value;
        stat3Value = stat3.value;
    }

    #region Show Canvas

    public void ShowMainCanvas()
    {
        SaveStatValues();

        mainCanvas.SetActive(true);
        minigame1Canvas.SetActive(false);
        minigame2Canvas.SetActive(false);
        minigame3Canvas.SetActive(false);
    }

    public void ShowMiniGame1()
    {
        //mainCanvas.SetActive(false);
        //minigame1Canvas.SetActive(true);
        ReplenishStat(stat1);
        warningText.text = "There's no minigame yet. :(";
    }

    public void ShowMiniGame2()
    {
        mainCanvas.SetActive(false);
        minigame2Canvas.SetActive(true);
        //ReplenishStat(stat2);
        //warningText.text = "There's no minigame yet. :(";
    }

    public void ShowMiniGame3()
    {
        mainCanvas.SetActive(false);
        minigame3Canvas.SetActive(true);
    }

    #endregion

    #region Complete Minigame

    public void CompleteMiniGame1()
    {
        ReplenishStat(stat1);
        ShowMainCanvas();
    }

    public void CompleteMiniGame2()
    {
        ReplenishStat(stat2);
        ShowMainCanvas();
    }

    public void CompleteMiniGame3()
    {
        ReplenishStat(stat3);
        ShowMainCanvas();
    }

    #endregion

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
        setLowStat(statBar, true);

        if (statBar == stat1)
        {
            ramenPet.sprite = stat1Sprite;
            warningText.text = "Your pet is too cold!\nWarm it up.";
        }
        else if (statBar == stat2)
        {
            ramenPet.sprite = stat2Sprite;
            warningText.text = "Your pet is too oily!\nAdd vinegar.";
        }
        else if (statBar == stat3)
        {
            ramenPet.sprite = stat3Sprite;
            warningText.text = "Your pet is too stale!\nAdd veggies.";
        }
    }

    void ResetSprite(Slider statBar)
    {
        ramenPet.sprite = normalSprite;
        warningText.text = "";
        setLowStat(statBar, false);
    }

    #endregion

    public void ResetMiniGame(GameObject canvas)
    {
        foreach (Transform child in canvas.transform)
        {

        }
        canvas.SetActive(false);
    }


    void GameOver()
    {
        statButtons.SetActive(false);
        gameOver.SetActive(true);
        ramenPet.sprite = deadSprite;
        warningText.text = "Your ramen pet died. :(";
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}