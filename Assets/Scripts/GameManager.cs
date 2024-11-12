using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public enum Food
{
    chocolate,
    grains
}

public enum GameStates
{
    selectScreen,
    preppingBattle,
    actingBattle,
    scoring
}
public class GameManager : MonoBehaviour
{
    public static GameStates state;

    public List<GameObject> teamOne = new List<GameObject>();
    public List<GameObject> teamTwo = new List<GameObject>();

    public GameObject[] pigeonPrefabs;
    public Transform[] teamSpawners;

    public Color[] colorList;
    public int indexTeamOne;
    public int indexTeamTwo;

    public GameObject[] colorButtons;

    public Pigeon_Stats_Base teamOneBase;
    public Pigeon_Stats_Base teamTwoBase;

    // Start is called before the first frame update
    void Start()
    {
        GameStates state = GameStates.selectScreen;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpSelectScreen()
    {
        colorButtons[0].SetActive(true);
        colorButtons[1].SetActive(true);
    }

    public void AddToTeams()
    {
        //insert maybe 10 or so, (if time permits, let the player select how many pigeons) into the teamOne[] list w/ Add
        for(int i = 0; i < 11; i++)
        {
            //instantiate here to a local GameObject variable, then add that variable to the list
            GameObject pigeonObj = Instantiate(pigeonPrefabs[0], Random.insideUnitSphere * 10 + teamSpawners[0].position, Quaternion.identity);
            teamOne.Add(pigeonObj);
        }
        for (int i = 0; i < 11; i++)
        {
            GameObject pigeonObj = Instantiate(pigeonPrefabs[1], Random.insideUnitSphere * 10 + teamSpawners[1].position, Quaternion.identity);
            teamTwo.Add(pigeonObj);
        }
    }

    public void CycleColors(int index)
    {
        if(index == 0)
        {
            if (indexTeamOne < colorList.Length-1) { indexTeamOne++; }
            else { indexTeamOne = 0; }
            colorButtons[index].GetComponent<UnityEngine.UI.Image>().color = colorList[indexTeamOne];
            teamOneBase.splatter.GetComponent<SpriteRenderer>().color = colorList[indexTeamOne];
        }
        if (index == 1)
        {
            if (indexTeamTwo < colorList.Length-1) { indexTeamTwo++; }
            else { indexTeamTwo = 0; }
            colorButtons[index].GetComponent<UnityEngine.UI.Image>().color = colorList[indexTeamTwo];
            teamTwoBase.splatter.GetComponent<SpriteRenderer>().color = colorList[indexTeamTwo];
        }
    }

    public void FeedPigeon()
    {
        //add a function that loops through each pigeon, turns a camera that focuses on that pigeon on, and allows you to feed that specific pigeon
    }
}
