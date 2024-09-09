using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchingManager : MonoBehaviour
{
    public GameObject[] Ids;
    public EventManagers manage;

    int index;
    private void OnEnable()
    {
        index = Random.Range(0, Ids.Length + 1);
        Ids[index].SetActive(true);

        if (!manage.firstpass)
        {

        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
