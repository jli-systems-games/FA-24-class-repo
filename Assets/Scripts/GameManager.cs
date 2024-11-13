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
    // THINGS TO DO: fix up pooping mech, add feeding system, add death functionality, fix movement, add ending screen
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
        state = GameStates.selectScreen;
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
        state = GameStates.preppingBattle;
        //insert maybe 10 or so, (if time permits, let the player select how many pigeons) into the teamOne[] list w/ Add
        for(int i = 0; i < 11; i++)
        {
            //instantiate here to a local GameObject variable, then add that variable to the list
            GameObject pigeonObj = Instantiate(pigeonPrefabs[0], Random.insideUnitSphere * 10 + teamSpawners[0].position, Random.rotation);

            teamOne.Add(pigeonObj);
        }
        for (int i = 0; i < 11; i++)
        {
            GameObject pigeonObj = Instantiate(pigeonPrefabs[1], Random.insideUnitSphere * 10 + teamSpawners[1].position, Random.rotation);

            teamTwo.Add(pigeonObj);
        }

        state = GameStates.actingBattle;
        Debug.Log("state: " + state);
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

    public void FeedPigeon(int team)
    {
        //add a function that loops through each pigeon, turns a camera that focuses on that pigeon on, and allows you to feed that specific pigeon
    }
}
