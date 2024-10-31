using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Canvas2");
    }

    // Update is called once p//er frame
    void Update()
    {

    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("goaled");
        GameGoals.score++;

        parent.GetComponent<GameGoals>().SpawnGoal();
        //parent.GetComponent<GameGoals>().Collected();

        Destroy(gameObject);

    }
}
