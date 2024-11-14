using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    // THINGS TO DO: fix up pooping mech, add feeding system, add ending screen
    public static GameStates state;

    public List<GameObject> teamOne = new List<GameObject>();
    public List<Food> TeamOneInventory = new List<Food>();
    public static List<GameObject> teamOneSplatters = new List<GameObject>();

    public List<GameObject> teamTwo = new List<GameObject>();
    public List<Food> TeamTwoInventory = new List<Food>();
    public static List<GameObject> teamTwoSplatters = new List<GameObject>();

    public GameObject[] inventoryBoxes;

    public GameObject[] pigeonPrefabs;
    public Transform[] teamSpawners;

    public Color[] colorList;
    public int indexTeamOne;
    public int indexTeamTwo;

    public GameObject[] colorButtons;

    public Pigeon_Stats_Base teamOneBase;
    public Pigeon_Stats_Base teamTwoBase;

    public GameObject grainPrefab;
    public GameObject chocolatePrefab;

    public GameObject[] selectScreenUIObjects;

    public int timer;
    public TextMeshProUGUI timerDisplay;

    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI[] scores;

    public GameObject poopBombText;

    // Start is called before the first frame update
    void Start()
    {
        poopBombText.SetActive(false);

        SetUpSelectScreen();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpSelectScreen()
    {
        state = GameStates.selectScreen;

        timer = 61;
        timerDisplay.gameObject.SetActive(false);
        winnerText.gameObject.SetActive(false);
        scores[0].gameObject.SetActive(false);
        scores[1].gameObject.SetActive(false);

        foreach (GameObject uiObj in selectScreenUIObjects)
        {
            uiObj.SetActive(true);
        }
    }

    public void AddToTeams()
    {
        foreach (GameObject uiObj in selectScreenUIObjects)
        {
            uiObj.SetActive(false);
        }
        timerDisplay.gameObject.SetActive(true);

        state = GameStates.preppingBattle;
        //insert maybe 10 or so, (if time permits, let the player select how many pigeons) into the teamOne[] list w/ Add
        for(int i = 0; i < 11; i++)
        {
            //instantiate here to a local GameObject variable, then add that variable to the list
            GameObject pigeonObj = Instantiate(pigeonPrefabs[0], Random.insideUnitSphere * 10 + teamSpawners[0].position, Random.rotation);
            pigeonObj.GetComponent<PigeonController>().TeamNum = 1;

            teamOne.Add(pigeonObj);
        }
        for (int i = 0; i < 11; i++)
        {
            GameObject pigeonObj = Instantiate(pigeonPrefabs[1], Random.insideUnitSphere * 10 + teamSpawners[1].position, Random.rotation);
            pigeonObj.GetComponent<PigeonController>().TeamNum = 2;
            pigeonObj.GetComponent<PigeonController>().SetPoopingState();


            teamTwo.Add(pigeonObj);
        }

        AddToStomachs();

        foreach(GameObject pigeonObj in teamOne)
        {
            pigeonObj.GetComponent<PigeonController>().SetPoopingState();
        }

        foreach (GameObject pigeonObj in teamTwo)
        {
            pigeonObj.GetComponent<PigeonController>().SetPoopingState();
        }

        state = GameStates.actingBattle;
        Debug.Log("state: " + state);

        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        timer--;
        timerDisplay.text = "Time: " + timer.ToString();

        yield return new WaitForSeconds(1);

        if( timer > 0)
        {
            StartCoroutine(Countdown());
        }

        else
        {
            EndGame();
        }
    }
    
    public void EndGame()
    {
        foreach(GameObject pigeon in teamOne)
        {
            Destroy(pigeon);
        }

        teamOne.Clear();

        foreach(GameObject pigeon in teamTwo)
        {
            Destroy(pigeon);
        }

        teamTwo.Clear();

        poopBombText.SetActive(false);

        winnerText.gameObject.SetActive(true);
        scores[0].gameObject.SetActive(true);
        scores[1].gameObject.SetActive(true);

        scores[0].text = "Team 1: " + teamOneSplatters.Count;
        scores[1].text = "Team 2: " + teamTwoSplatters.Count;
        if(teamOneSplatters.Count > teamTwoSplatters.Count) { winnerText.text = "Team One Wins!!!"; }
        else if(teamTwoSplatters.Count > teamOneSplatters.Count) { winnerText.text = "Team Two Wins!!!"; }
        else { winnerText.text = "It's a Tie!!!!"; }
        teamOneSplatters.Clear();
        teamTwoSplatters.Clear();
    }

    public void AddToStomachs()
    {
        //Debug.Log(TeamOneInventory.Count);
        //Debug.Log("team 1 count: " + teamOne.Count);
        //cycle through each pigeon, add food from the inventory one at a time, return back to the first pigeon if there's more food than pigeons, remove item from the inventory once "fed"
        for(int i = 0; i < teamOne.Count; i++)
        {
            if( i < TeamOneInventory.Count)
            {
                teamOne[i].GetComponent<PigeonController>().stomachLevel++;
                teamOne[i].GetComponent<PigeonController>().stomachItems.Add(TeamOneInventory[i]);
                TeamOneInventory.RemoveAt(i);
            }
        }

        for(int i = 0; i < teamTwo.Count; i++)
        {
            if (i < TeamTwoInventory.Count)
            {
                teamTwo[i].GetComponent<PigeonController>().stomachLevel++;
                teamTwo[i].GetComponent<PigeonController>().stomachItems.Add(TeamTwoInventory[i]);
                TeamTwoInventory.RemoveAt(i);
            }
        }

        if(TeamOneInventory.Count > 0 && TeamTwoInventory.Count > 0)
        {
            AddToStomachs();
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

    public void FeedTeamOne(string food)
    {
        if(food == "grains")
        {
            Vector3 foodPos = new Vector3(Random.Range(-200, 200), Random.Range(-300, 300), 0);
            GameObject grain = Instantiate(grainPrefab, inventoryBoxes[0].transform);
            grain.GetComponent<RectTransform>().localPosition = foodPos;

            TeamOneInventory.Add(Food.grains);
        }

        if(food == "chocolate")
        {
            Vector3 foodPos = new Vector3(Random.Range(-200, 200), Random.Range(-300, 300), 0);
            GameObject chocolate = Instantiate(chocolatePrefab, inventoryBoxes[0].transform);
            chocolate.GetComponent<RectTransform>().localPosition = foodPos;
            
            TeamOneInventory.Add(Food.chocolate);
        }
    }

    public void FeedTeamTwo(string food)
    {
        if (food == "grains")
        {
            Vector3 foodPos = new Vector3(Random.Range(-200, 200), Random.Range(-300, 300), 0);
            GameObject grain = Instantiate(grainPrefab, inventoryBoxes[1].transform);
            grain.GetComponent<RectTransform>().localPosition = foodPos;

            TeamTwoInventory.Add(Food.grains);
        }

        if (food == "chocolate")
        {
            Vector3 foodPos = new Vector3(Random.Range(-200, 200), Random.Range(-300, 300), 0);
            GameObject chocolate = Instantiate(chocolatePrefab, inventoryBoxes[1].transform);
            chocolate.GetComponent<RectTransform>().localPosition = foodPos;
            TeamTwoInventory.Add(Food.chocolate);
        }
    }
}
