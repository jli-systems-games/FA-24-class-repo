using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    IEnumerator manager;
    public float timePassed = 15f;


    // Start is called before the first frame update
    void Start()
    {
        manager = determineEvents(timePassed);
        StartCoroutine(manager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator determineEvents(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            randomEvents();
        }
    }

    void randomEvents()
    {
        int numb = Random.Range(0, 3);

        switch (numb)
        {
            case 1:
                Debug.Log("true & false");
                break;
            case 2:
                Debug.Log("Oh no, a stranger!");
                break;
            case 3:
                Debug.Log("wow a new Dimension");
                break;
        }
    }
}
