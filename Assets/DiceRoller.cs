using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public int playerRoll;
    public int minPlayerRoll = 1;
    public int maxPlayerRoll = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollPlayer()
    {
        playerRoll = Random.Range(1, maxPlayerRoll + 1);
        Debug.Log("player score = " + playerRoll);
    }

    public int ResultPlayer()
    {
        return playerRoll;
    }

    public void IncreaseMaxPlayerRoll(int amount)
    {
        maxPlayerRoll = maxPlayerRoll + amount;
    }

    public void DecreaseMaxPlayerRoll(int amount)
    {
        maxPlayerRoll = Mathf.Max(8, maxPlayerRoll - amount);
    }

    public int GetMaxPlayerRoll()
    {
        return maxPlayerRoll;
    }
}
