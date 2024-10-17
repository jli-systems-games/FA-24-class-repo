using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum SimState
{
    Awake,
    Asleep,
    Working,
    Eating,
    Hungry
}

public class Event_Sim : MonoBehaviour
{
    public static SimState state;
    public float needHunger, needEnergy, needEntertainment;
    public bool alive;

    public UnityEvent onGetHungry;

    public Slider hungerSlider;
    public Slider energySlider;
    public Slider entertainmentSlider;

    public Button feedButton;
    public Button energyButton;
    public Button entertainmentButton;
    public Button restartButton;
    public float buttonCooldown = 3f;

    public GameObject endPanel;
    public GameObject character;

    public AudioSource audioSource;
    public AudioClip feedSound;
    public AudioClip energySound;
    public AudioClip entertainmentSound;

    // Start is called before the first frame update
    void Start()
    {
        state = SimState.Awake;
        needHunger = 100;
        needEnergy = 100;
        needEntertainment = 100;
        alive = true;

        hungerSlider.maxValue = 100;
        energySlider.maxValue = 100;
        entertainmentSlider.maxValue = 100;

        UpdateSliders();

        if (alive == true)
        {
            StartCoroutine(PassiveHunger(.8f));
            StartCoroutine(PassiveEnergy(.5f));
            StartCoroutine(PassiveEntertainment(.3f));
        }

        feedButton.onClick.AddListener(PlayFeedAction);
        energyButton.onClick.AddListener(PlayEnergyAction);
        entertainmentButton.onClick.AddListener(PlayEntertainmentAction);

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        gameObject.SetActive(false);
        SetButtonsInteractable(false);

        endPanel.SetActive(true);
    }

    void SetButtonsInteractable(bool state)
    {
        feedButton.interactable = state;
        energyButton.interactable = state;
        entertainmentButton.interactable = state;
    }

    #region Update Behavior

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case SimState.Awake:
                break;
            case SimState.Asleep:
                break;
            default:
                break;
        }

        if (hungerSlider.value <= 0 || energySlider.value <= 0 || entertainmentSlider.value <= 0)
        {
            GameOver();
        }
    }

    void SimBehavior()
    {
        state = SimState.Awake;
        needHunger = 100;
        needEnergy = 100;
        needEntertainment = 100;
    }

    #endregion
    #region Passive Drains

    IEnumerator PassiveHunger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //needHunger--;
        NeedChangeHunger(-1f);

        if (alive == true)
        {
            if(state == SimState.Asleep)
            {
                StartCoroutine(PassiveHunger(2.1f));
            }
            else if(state == SimState.Eating)
            {
                // do smth
            }
            else
                StartCoroutine(PassiveHunger(.7f));
        }
    }

    IEnumerator PassiveEnergy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //needEnergy--;
        NeedChangeEnergy(-1f);

        if (alive == true)
            StartCoroutine(PassiveEnergy(.4f));
    }

    IEnumerator PassiveEntertainment(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //needEntertainment--;
        NeedChangeEntertainment(-1f);

        if (alive == true)
            StartCoroutine(PassiveEntertainment(.3f));
    }

    #endregion

    #region Active Changes

    void NeedChangeHunger(float change)
    {
        needHunger += change;
        needHunger = Mathf.Clamp(needHunger, 0, 100);

        if (needHunger < 50 && state != SimState.Eating)
        {
            //onGetHungry.Invoke();
            //maybe music changes or you can't do certain things
            StateChanged(SimState.Hungry);
        }

        UpdateSliders();
    }

    void NeedChangeEnergy(float change)
    {
        needEnergy += change;
        needEnergy = Mathf.Clamp(needEnergy, 0, 100);
        UpdateSliders();
    }

    void NeedChangeEntertainment(float change)
    {
        needEntertainment += change;
        needEntertainment = Mathf.Clamp(needEntertainment, 0, 100);
        UpdateSliders();
    }

    #endregion

    #region Button Methods

    public void Feed()
    {
        NeedChangeHunger(3f);
        StartCoroutine(ButtonCooldown(feedButton));
    }

    public void GiveEnergy()
    {
        NeedChangeEnergy(3f);
        StartCoroutine(ButtonCooldown(energyButton));
    }

    public void Entertain()
    {
        NeedChangeEntertainment(5f);
        StartCoroutine(ButtonCooldown(entertainmentButton));
    }

    IEnumerator ButtonCooldown(Button button)
    {
        button.interactable = false;
        yield return new WaitForSeconds(buttonCooldown);
        button.interactable = true;
    }

    #endregion

    void StateChanged(SimState newState)
    {
        switch (newState)
        {
            case SimState.Hungry:
                state = newState;
                onGetHungry.Invoke();
                //state has changed, react accordingly
                break;
            default:
                break;
        }
    }

    void UpdateSliders()
    {
        hungerSlider.value = needHunger;
        energySlider.value = needEnergy;
        entertainmentSlider.value = needEntertainment;
    }

    void PlayFeedAction()
    {
        PlaySound(feedSound);
        Feed();
    }

    void PlayEnergyAction()
    {
        PlaySound(energySound);
        GiveEnergy();
    }

    void PlayEntertainmentAction()
    {
        PlaySound(entertainmentSound);
        Entertain();
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
