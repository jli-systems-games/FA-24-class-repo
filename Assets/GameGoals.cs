using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGoals : MonoBehaviour
{

    public int score = 0;

    public GameObject goalPrefab;

    public List<Transform> goalLocations = new();

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        SpawnGoal();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SpawnGoal()
    {
        GameObject goal;

        goal = Instantiate(goalPrefab, goalLocations[Random.Range(0, goalLocations.Count)].position, Quaternion.identity);
    }


}
