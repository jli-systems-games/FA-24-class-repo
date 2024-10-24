using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI dayCountText;
    private float timer;
    private int dayCount;
    private bool isTimerRunning;
    public GameObject PreparationScreen;
    public AudioSource DuckSong;

    public InventoryManager inventoryManager;
    public CustomerManager customerManager;    


    private void Start()
    {
        timer = 7 * 3600;
        dayCount = 1;
        UpdateTimerText();
        UpdateDayCountText();
        PreparationScreen.SetActive(true);

    }

    public void StartTimer()
    {
        if (!isTimerRunning)
        {
            isTimerRunning = true;
            InvokeRepeating(nameof(IncrementTimer), 0f, 1f);
            DuckSong.Play();
            inventoryManager.CalculateServings();
            PreparationScreen.SetActive(false);
            customerManager.StartDay();
        }
    }

    private void IncrementTimer()
    {
        timer += 15 * 60;

        if (timer >= 18 * 3600)
        {
            timer = 7 * 3600;
            isTimerRunning = false;
            CancelInvoke(nameof(IncrementTimer)); 
            dayCount++;
            UpdateDayCountText();
            PreparationScreen.SetActive(true);
            DuckSong.Pause();
            customerManager.EndDay();
        }

        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        int hours = (int)(timer / 3600) % 24;
        int minutes = (int)(timer % 3600) / 60;
        timerText.text = $"{hours:D2}:{minutes:D2}";
    }

    private void UpdateDayCountText()
    {
        dayCountText.text = $"Day: {dayCount}";
    }
}
