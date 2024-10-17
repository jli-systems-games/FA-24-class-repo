using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public event Action onGetTired;
    //public event Action<SimState> onGetBored;

    // Start is called before the first frame update
    void Start()
    {
        state = SimState.Awake;
        needHunger = 100;
        needEnergy = 100;
        needEntertainment = 100;
        alive = true;


        onGetTired?.Invoke();

        if (alive == true)
        {
            StartCoroutine(PassiveHunger(3f));
            StartCoroutine(PassiveEnergy(2f));
            StartCoroutine(PassiveEntertainment(.5f));
        }
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

    }

    void SimBehavior()
    {

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
            if (state == SimState.Asleep)
            {
                StartCoroutine(PassiveHunger(10f));
            }
            else if (state == SimState.Eating)
            {
                //do something else
            }
            else
                StartCoroutine(PassiveHunger(3f));
        }
    }
    IEnumerator PassiveEnergy(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //needEnergy--;
        NeedChangeEnergy(-1f);

        if (alive == true)
            StartCoroutine(PassiveEnergy(2f));
    }
    IEnumerator PassiveEntertainment(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //needEntertainment--;
        NeedChangeEntertainment(-1f);

        if (alive == true)
            StartCoroutine(PassiveEntertainment(.5f));
    }
    #endregion

    #region Active Changes

    void NeedChangeHunger(float change)
    {
        needHunger += change;

        if (needHunger < 50 && state != SimState.Eating)
        {
            //onGetHungry.Invoke();
            StateChanged(SimState.Hungry);
        }

    }
    void NeedChangeEnergy(float change)
    {
        needEnergy += change;
    }
    void NeedChangeEntertainment(float change)
    {
        needEntertainment += change;
    }
    #endregion

    void StateChanged(SimState newState)
    {
        //stateChange.Invoke(newState);
        switch (newState)
        {
            case SimState.Hungry:
                state = newState;
                onGetHungry.Invoke();
                break;
            default:
                break;
        }
    }
}