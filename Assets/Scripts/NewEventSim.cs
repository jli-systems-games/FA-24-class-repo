using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NewEventSim : MonoBehaviour
{
    public float needHunger = 100, needEnergy = 100, needClean = 100, needEntertainment = 100;
    public bool alive = true;

    public UnityEvent onGetHungry, onGetTired, onGetDirty, onGetBored;
    public Slider hungerSlider, energySlider, cleanSlider, entertainmentSlider;
    public Button fridgeButton, bedButton, tubButton, doorButton, bookButton, kitchenSinkButton, bathroomSinkButton, potButton, closetButton;
    public GameObject Asleep, Awake, Bathing, Dry, Eating, Hungry, WashingKitchen, WashingBathroom, Reading, Cooking;
    public Image sleepingStatus, eatingStatus, bathingStatus, washingStatus, walkingStatus, readingStatus;
    public GameObject cityCanvas, kitchenCanvas, buttons;
    public Image nightBedroom, dayBedroom;
    public Image fridge, pot, tub, door, kitchenSink, bathroomSink, closet, bookshelf, bed;

    private bool isPerformingAction;

    void Start()
    {
        Initialize();
        cityCanvas.SetActive(false);
    }

    void Initialize()
    {
        UpdateSliders();

        sleepingStatus.enabled = false;
        Asleep.SetActive(false);

        eatingStatus.enabled = false;
        Eating.SetActive(false);
        Cooking.SetActive(false);

        bathingStatus.enabled = false;
        Bathing.SetActive(false);

        washingStatus.enabled = false;
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);

        readingStatus.enabled = false;
        Reading.SetActive(false);

        walkingStatus.enabled = false;

        dayBedroom.enabled = true;
        nightBedroom.enabled = false;

        if (alive)
        {
            StartCoroutine(PassiveDrain(1f, "hunger"));
            StartCoroutine(PassiveDrain(1f, "energy"));
            StartCoroutine(PassiveDrain(1f, "clean"));
            StartCoroutine(PassiveDrain(1f, "entertainment"));
        }
        AssignButtonActions();
    }

    void AssignButtonActions()
    {
        fridgeButton.onClick.AddListener(() => PerformAction("eating"));
        bedButton.onClick.AddListener(() => PerformAction("sleeping"));
        tubButton.onClick.AddListener(() => PerformAction("bathing"));
        doorButton.onClick.AddListener(() => PerformAction("walking"));
        bookButton.onClick.AddListener(() => PerformAction("reading"));
        kitchenSinkButton.onClick.AddListener(() => PerformAction ("washingkitchen"));
        bathroomSinkButton.onClick.AddListener(() => PerformAction ("washingbathroom"));
        potButton.onClick.AddListener(() => PerformAction ("cooking"));
    }

    void PerformAction(string action)
    {
        if (isPerformingAction) return;

        DisableButtons();

        isPerformingAction = true;
        switch (action)
        {
            case "eating":
                StartCoroutine(PerformEating());
                break;
            case "sleeping":
                StartCoroutine(PerformSleeping());
                break;
            case "bathing":
                StartCoroutine(PerformBathing());
                break;
            case "walking":
                StartCoroutine(PerformWalking());
                break;
            case "reading":
                StartCoroutine(PerformReading());
                break;
            case "washingkitchen":
                StartCoroutine(PerformWashingKI());
                break;
            case "washingbathroom":
                StartCoroutine(PerformWashingBA());
                break;
            case "cooking":
                StartCoroutine(PerformCooking());
                break;
        }
    }

    void DisableButtons()
    {
        bed.enabled = false;
        closet.enabled = false;
        bookshelf.enabled = false;
        fridge.enabled = false;
        pot.enabled = false;
        tub.enabled = false;
        door.enabled = false;
        kitchenSink.enabled = false;
        bathroomSink.enabled = false;
    }

    #region eating

    IEnumerator PerformEating()
    {
        eatingStatus.enabled = true;
        Eating.SetActive(true);
        Asleep.SetActive(false);
        Reading.SetActive(false);
        Awake.SetActive(false);
        Bathing.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);
        Cooking.SetActive(false);
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            NeedChange(ref needHunger, 3f, onGetHungry);
            NeedChange(ref needClean, -1f, onGetDirty);
            NeedChange(ref needEntertainment, 2, onGetBored);
            NeedChange(ref needEnergy, -2f, onGetTired);
            yield return new WaitForSeconds(1f);
        }

        ResetAction(eatingStatus, Eating);
    }

    #endregion

    #region sleeping

    IEnumerator PerformSleeping()
    {
        sleepingStatus.enabled = true;
        dayBedroom.enabled = false;
        nightBedroom.enabled = true;
        Asleep.SetActive(true);
        Reading.SetActive(false);
        Eating.SetActive(false);
        Awake.SetActive(false);
        Bathing.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);
        Cooking.SetActive(false);
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);

        bed.enabled = false;
        closet.enabled = false;
        bookshelf.enabled = false;

        for (int i = 0; i < 10; i++)
        {
            NeedChange(ref needEnergy, 5f, onGetTired);
            NeedChange(ref needClean, -3f, onGetDirty);
            NeedChange(ref needHunger, -1f, onGetHungry);
            NeedChange(ref needEntertainment, -1f, onGetBored);
            yield return new WaitForSeconds(1f);
        }

        dayBedroom.enabled = true;
        nightBedroom.enabled = false;

        bed.enabled = true;
        closet.enabled = true;
        bookshelf.enabled = true;

        ResetAction(sleepingStatus, Asleep);
    }

    #endregion

    #region bathing

    IEnumerator PerformBathing()
    {
        bathingStatus.enabled = true;
        Bathing.SetActive(true);
        Reading.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);
        Eating.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);
        Cooking.SetActive(false);
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            NeedChange(ref needClean, 5f, onGetDirty);
            NeedChange(ref needEnergy, -3f, onGetTired);
            NeedChange(ref needEntertainment, 2f, onGetBored);
            NeedChange(ref needHunger, -1f, onGetHungry);
            yield return new WaitForSeconds(1f);
        }

        ResetAction(bathingStatus, Bathing);
    }

    #endregion

    #region walking

    IEnumerator PerformWalking()
    {
        walkingStatus.enabled = true;
        Reading.SetActive(false);
        Cooking.SetActive(false);
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);
        Bathing.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);
        Eating.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);

        kitchenCanvas.SetActive(false);
        cityCanvas.SetActive(true);
        buttons.SetActive(false);

        for (int i = 0; i < 7; i++)
        {
            NeedChange(ref needEnergy, 5f, onGetTired);
            NeedChange(ref needEntertainment, 4f, onGetBored);
            NeedChange(ref needHunger, -3f, onGetHungry);
            NeedChange(ref needClean, -5f, onGetDirty);
            yield return new WaitForSeconds(1f);
        }

        cityCanvas.SetActive(false);
        kitchenCanvas.SetActive(true);
        buttons.SetActive(true);

        ResetAction(walkingStatus, Awake);
    }

    #endregion

    #region reading

    IEnumerator PerformReading()
    {
        readingStatus.enabled = true;
        Reading.SetActive(true);
        Bathing.SetActive(false);
        Cooking.SetActive(false);
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);
        Eating.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            NeedChange(ref needEntertainment, 5f, onGetBored);
            NeedChange(ref needHunger, -2f, onGetHungry);
            NeedChange(ref needClean, -1f, onGetDirty);
            NeedChange(ref needEnergy, -3f, onGetTired);
            yield return new WaitForSeconds(1f);
        }

        ResetAction(readingStatus, Reading);
    }

    #endregion

    #region washing

    IEnumerator PerformWashingBA()
    {
        washingStatus.enabled = true;
        WashingBathroom.SetActive(true);
        WashingKitchen.SetActive(false);
        Cooking.SetActive(false);
        Reading.SetActive(false);
        Bathing.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);
        Eating.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            NeedChange(ref needClean, 3f, onGetDirty);
            NeedChange(ref needEnergy, -1f, onGetTired);
            NeedChange(ref needEntertainment, -3f, onGetBored);
            NeedChange(ref needHunger, -1f, onGetHungry);
            yield return new WaitForSeconds(1f);            
        }

        ResetAction(washingStatus, WashingBathroom);
    }

    IEnumerator PerformWashingKI()
    {
        washingStatus.enabled = true;
        WashingKitchen.SetActive(true);
        WashingBathroom.SetActive(false);
        Cooking.SetActive(false);
        Reading.SetActive(false);
        Bathing.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);
        Eating.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);  

        for (int i = 0; i < 3; i++)
        {
            NeedChange(ref needClean, 3f, onGetDirty);
            NeedChange(ref needEnergy, -1f, onGetTired);
            NeedChange(ref needEntertainment, -3f, onGetBored);
            NeedChange(ref needHunger, -1f, onGetHungry);
            yield return new WaitForSeconds(1f);            
        }

        ResetAction(washingStatus, WashingKitchen);      
    }

    #endregion

    #region cooking

    IEnumerator PerformCooking()
    {
        eatingStatus.enabled = true;
        Cooking.SetActive(true);
        WashingBathroom.SetActive(false);
        WashingKitchen.SetActive(false);
        Reading.SetActive(false);
        Bathing.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);
        Eating.SetActive(false);
        Dry.SetActive(false);
        Hungry.SetActive(false);

        for (int i = 0; i < 5; i++)
        {
            NeedChange(ref needHunger, 5f, onGetHungry);
            NeedChange(ref needEnergy, -3f, onGetTired);
            NeedChange(ref needEntertainment, 2f, onGetBored);
            NeedChange(ref needClean, -4f, onGetDirty);
            yield return new WaitForSeconds (1f); 
        }

        ResetAction(eatingStatus, Cooking);       
    }

    #endregion

    void ResetAction(Image status, GameObject actionObject)
    {
        isPerformingAction = false;
        status.enabled = false;
        actionObject.SetActive(false);
        ReassignButtonImage();

        Awake.SetActive(true);
        Dry.SetActive(true);
        Hungry.SetActive(true);
    }

    void ReassignButtonImage()
    {
        bed.enabled = true;
        closet.enabled = true;
        bookshelf.enabled = true;
        fridge.enabled = true;
        pot.enabled = true;
        tub.enabled = true;
        door.enabled = true;
        kitchenSink.enabled = true;
        bathroomSink.enabled = true;
    }


    void NeedChange(ref float need, float change, UnityEvent onLowNeedEvent)
    {
        need = Mathf.Clamp(need + change, 0, 100);
        UpdateSliders();

        if (need < 50 && onLowNeedEvent != null) onLowNeedEvent.Invoke();
        if (need == 0) EndGame();
    }

    #region passive drain

    IEnumerator PassiveDrain(float waitTime, string needType)
    {
        while (alive)
        {
            yield return new WaitForSeconds(waitTime);

            if (isPerformingAction) continue;



            switch (needType)
            {
                case "hunger": NeedChange(ref needHunger, -1f, onGetHungry); break;
                case "energy": NeedChange(ref needEnergy, -1f, onGetTired); break;
                case "clean": NeedChange(ref needClean, -1f, onGetDirty); break;
                case "entertainment": NeedChange(ref needEntertainment, -3f, onGetBored); break;
            }
        }
    }

    #endregion

    void UpdateSliders()
    {
        hungerSlider.value = needHunger;
        energySlider.value = needEnergy;
        cleanSlider.value = needClean;
        entertainmentSlider.value = needEntertainment;

    }

    void EndGame()
    {
        alive = false;
        Debug.Log("Fin died");
        SceneManager.LoadScene(2);
    }
}
