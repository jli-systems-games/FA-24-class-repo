using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDice : Dice
{
    private const int DefaultMaxRoll = 6;

    private void Awake()
    {
        MaxRoll = DefaultMaxRoll;
    }

    public void AdjustMaxRoll(int amount)
    {
        MaxRoll = Mathf.Max(DefaultMaxRoll, MaxRoll + amount);
    }

    public int GetMaxRoll()
    {
        return MaxRoll;
    }
}