using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum SimState
{
    Awake,
    Asleep,
    Tired,
    Eating,
    Hungry,
    Bathing,
    Dirty
}

public class Event_Sim : MonoBehaviour
{
    public static SimState state;
    public float needHunger, needEnergy, needClean;
    public bool alive;

    public UnityEvent onGetHungry, onGetTired, onGetDirty;

    public Slider hungerSlider, energySlider, cleanSlider;

    public Button fridgeButton;
    public Button bedButton;
    public Button tubButton;

    public GameObject Asleep;
    public GameObject Awake;
    public GameObject Bathing;
    public GameObject Dry;
    public GameObject Eating;
    public GameObject Hungry;

    public Image sleepingStatus;
    public Image eatingStatus;
    public Image bathingStatus;

    private bool isPerformingAction;

    void Start()
    {
        state = SimState.Awake;
        needHunger = 100;
        needEnergy = 100;
        needClean = 100;
        alive = true;

        sleepingStatus.enabled = false;
        eatingStatus.enabled = false;
        bathingStatus.enabled = false;

        hungerSlider.maxValue = 100;
        energySlider.maxValue = 100;
        cleanSlider.maxValue = 100;

        UpdateSliders();

        if (fridgeButton != null)
        {
            fridgeButton.onClick.AddListener(OnFridgeClick);
        }

        if (bedButton != null)
        {
            bedButton.onClick.AddListener(OnBedClick);
        }

        if (tubButton != null)
        {
            tubButton.onClick.AddListener(OnTubClick);
        }

        if (alive == true)
        {
            StartCoroutine(PassiveHunger(2f));
            StartCoroutine(PassiveEnergy(2f));
            StartCoroutine(PassiveClean(2f));
        }
    }

    #region Active Increase
    public void OnFridgeClick()
    {
        if (!isPerformingAction)
        {   
            Debug.Log("Eating");
            NeedChangeHunger(10f);  
            state = SimState.Eating; 
            StartCoroutine(ResetStateAfterEating(5f));
            eatingStatus.enabled = true;
        }
    }

    IEnumerator ResetStateAfterEating(float waitTime)
    {
        isPerformingAction = true;
        Eating.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Eating.SetActive(false);
        eatingStatus.enabled = false;
        state = SimState.Awake;
        Debug.Log("Done eating");
        isPerformingAction = false;
        StartCoroutine(PassiveHunger(2f));

    }

    public void OnBedClick()
    {
        if (!isPerformingAction)
        {
            Debug.Log("Sleeping");
            NeedChangeEnergy(10f);  
            state = SimState.Asleep; 
            StartCoroutine(ResetStateAfterSleeping(5f)); 
            sleepingStatus.enabled = true;
        }
    }

    IEnumerator ResetStateAfterSleeping(float waitTime)
    {
        isPerformingAction = true;
        Asleep.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Asleep.SetActive(false);
        sleepingStatus.enabled = false;
        state = SimState.Awake;
        Debug.Log("Done sleeping");
        isPerformingAction = false;
        StartCoroutine(PassiveEnergy(2f));

    }

    public void OnTubClick()
    {   
        if (!isPerformingAction)
        {
            Debug.Log("Bathing");
            NeedChangeClean(10f);  
            state = SimState.Bathing; 
            StartCoroutine(ResetStateAfterBathing(5f)); 
            bathingStatus.enabled = true;
        }
    }

    IEnumerator ResetStateAfterBathing(float waitTime)
    {
        isPerformingAction = true;
        Bathing.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        Bathing.SetActive(false);
        bathingStatus.enabled = false;
        state = SimState.Awake; 
        Debug.Log("Done bathing");
        isPerformingAction = false;
        StartCoroutine(PassiveClean(2f));

    }
    #endregion

    #region Update Behavior / Update

    void Update()
    {
        UpdateSliders();

        fridgeButton.interactable = alive;
        bedButton.interactable = alive;
        tubButton.interactable = alive;

        switch (state)
        {
            case SimState.Awake:
                Asleep.SetActive(false);
                Awake.SetActive(true);
                Bathing.SetActive(false);
                Dry.SetActive(true);
                Hungry.SetActive(true);
                Eating.SetActive(false);
                fridgeButton.interactable = true;
                bedButton.interactable = true;
                tubButton.interactable = true;
                break;
            case SimState.Asleep:
                Asleep.SetActive(true);
                Awake.SetActive(false);
                Dry.SetActive(false);
                Hungry.SetActive(false);
                fridgeButton.interactable = false;
                bedButton.interactable = false;
                tubButton.interactable = false;
                break;
            case SimState.Eating:
                Eating.SetActive(true);
                Hungry.SetActive(false);
                Dry.SetActive(false);
                Awake.SetActive(false);
                fridgeButton.interactable = false;
                bedButton.interactable = false;
                tubButton.interactable = false;
                break;
            case SimState.Bathing:
                Bathing.SetActive(true);
                Dry.SetActive(false);
                Awake.SetActive(false);
                Hungry.SetActive(false);
                fridgeButton.interactable = false;
                bedButton.interactable = false;
                tubButton.interactable = false;
                break;
            default:
                Asleep.SetActive(false);
                Awake.SetActive(true);
                Dry.SetActive(true);
                Bathing.SetActive(false);
                Hungry.SetActive(true);
                Eating.SetActive(false);
                fridgeButton.interactable = true;
                bedButton.interactable = true;
                tubButton.interactable = true;
                break;
        }
    }

    void UpdateSliders()
    {
        hungerSlider.value = Mathf.Clamp(needHunger, 0, 100); 
        energySlider.value = Mathf.Clamp(needEnergy, 0, 100); 
        cleanSlider.value = Mathf.Clamp(needClean, 0, 100);
    }

    #endregion

    #region Passive Drains
    IEnumerator PassiveHunger(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NeedChangeHunger(-5f);
        if (alive == true)
        {
            if (state != SimState.Eating)
            {
                StartCoroutine(PassiveHunger(2f));
            }
        }
    }

    IEnumerator PassiveEnergy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NeedChangeEnergy(-5f);
        if (alive == true)
        {
            if (state != SimState.Asleep)
            {
                StartCoroutine(PassiveEnergy(2f));
            }
        }
    }

    IEnumerator PassiveClean(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NeedChangeClean(-5f);
        if (alive == true)
        {
            if (state != SimState.Bathing)
            {
                StartCoroutine(PassiveClean(2f));
            }
        }
    }
    #endregion

    #region Active Changes
    void NeedChangeHunger(float change)
    {
        needHunger = Mathf.Clamp(needHunger + change, 0, 100);
        if (needHunger < 50 && state != SimState.Eating)
        {
            StateChanged(SimState.Hungry);
        }
        else if (needHunger == 0)
        {
            EndGame();
        }
    }
    void NeedChangeEnergy(float change)
    {
        needEnergy = Mathf.Clamp(needEnergy + change, 0, 100);
        if (needEnergy < 50 && state != SimState.Asleep)
        {
            StateChanged(SimState.Tired);
        }
        else if (needEnergy == 0)
        {
            EndGame();
        }
    }
    void NeedChangeClean(float change)
    {
        needClean = Mathf.Clamp(needClean + change, 0, 100);
        if (needClean < 50 && state != SimState.Bathing)
        {
            StateChanged(SimState.Dirty);
        }
        else if (needClean == 0)
        {
            EndGame();
        }
    }
    #endregion

    void StateChanged(SimState newState)
    {
        state = newState;
        switch (newState)
        {
            case SimState.Hungry:
                onGetHungry.Invoke();
                break;
            case SimState.Tired:
                onGetTired.Invoke();
                break;
            case SimState.Dirty:
                onGetDirty.Invoke();
                break;
        }
    }

    void EndGame()
    {
        alive = false;
        Debug.Log("Fin died");
        StopAllCoroutines();
        SceneManager.LoadScene(2);
    }
}