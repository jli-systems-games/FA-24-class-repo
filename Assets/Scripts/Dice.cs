using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dice : MonoBehaviour
{
    public int CurrentRoll { get; protected set; }
    public int MinRoll { get; protected set; }
    public int MaxRoll { get; protected set; }

    public void Roll()
    {
        CurrentRoll = Random.Range(MinRoll, MaxRoll + 1);
        Debug.Log($"{this.GetType().Name} scored {CurrentRoll}");
    }

    public int GetRollResult()
    {
        return CurrentRoll;
    }

    public void SetRollRange(int min, int max)
    {
        MinRoll = min;
        MaxRoll = max;
    }
}
